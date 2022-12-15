namespace Banking
{
    public class BankingService : IBankingService
    {
        private readonly AccountsRepository _accountsRepository;
        public BankingService()
        {
            _accountsRepository = new AccountsRepository();
        }
        public int CheckBalance(int accountNumber)
        {
            //Assumes account exists in the repository
            var account = _accountsRepository.GetBankAccountSummary(accountNumber);
            return account.Balance;
        }

        public bool DepositAmount(int accountNumber, int amount)
        {
            if (amount <= 0)
            {
                return false;
            }
            var account = _accountsRepository.GetBankAccountSummary(accountNumber);
            if (account == null)
            {
                return false;
            }
            _accountsRepository.UpdateBalance(accountNumber, amount);
            return true;
        }

        public BankAccountDTO GetBankAccountSummary(int accountNumber)
        {
            var account = _accountsRepository.GetBankAccountSummary(accountNumber);
            if (account == null)
            {
                return null;
            }
            return new BankAccountDTO()
            {
                AccountNumber = account.AccountNumber,
                AccountType = account.AccountType,
                BalanceAmount = account.Balance
            };
        }

        public bool WithdrawAmount(int accountNumber, int amountToWithdraw)
        {
            // cannot withdraw negative amount
            if (amountToWithdraw <= 0)
            {
                return false;
            }
            var account = _accountsRepository.GetBankAccountSummary(accountNumber);

            // cannot withdraw beyond the withdraw limit 
            // cannot withdraw amount more than available balance 
            if (amountToWithdraw > GetWithDrawLimit(account.AccountType) || !IsEnoughBalanceAvailable(account, amountToWithdraw))
            {
                return false;
            }
            _accountsRepository.UpdateBalance(accountNumber, amountToWithdraw);
            return true;
        }
        public BankAccountDTO CreateAccount(CreateAccountDTO newAccount)
        {
            if (!Enum.TryParse<AccountType>(newAccount.AccountType, out var accountType))
            {
                throw new ArgumentException($"Account Type {newAccount.AccountType} not supported");
            }
            if (string.IsNullOrEmpty(newAccount.Name))
            {
                throw new ArgumentException($"Account holder Name cannot be blank");
            }
            var account = _accountsRepository.CreateAccount(newAccount);
            return new BankAccountDTO()
            {
                AccountNumber = account.AccountNumber,
                AccountType = account.AccountType,
                BalanceAmount = account.Balance
            };
        }

        private static int GetWithDrawLimit(AccountType accountType)
        {
            return accountType switch
            {
                AccountType.Current => BankAccountLimit.CurrentAccountWithdrawLimit,
                AccountType.Savings => BankAccountLimit.SavingsAccountWithdrawLimit,
                _ => BankAccountLimit.ZeroBalanceAccountWithdrawLimit,
            };
        }

        private static bool IsEnoughBalanceAvailable(BankAcount account, int amountToWithdraw)
        {
            var balance = account.AccountType switch
            {
                // current account comes with overdraft limit 
                AccountType.Current => account.IsOverDraftAllowed ? (account.Balance + account.OverDraftLimit) : account.Balance,
                AccountType.Savings => account.Balance,
                _ => account.Balance,
            };

            return balance > amountToWithdraw;
        }
    }
}
