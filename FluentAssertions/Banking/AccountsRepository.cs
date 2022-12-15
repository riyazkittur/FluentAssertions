namespace Banking
{
    public class AccountsRepository
    {
        private readonly List<BankAcount> _bankAcounts;

        public AccountsRepository()
        {
            _bankAcounts = new List<BankAcount>()
            {
                new BankAcount()
                {
                    AccountNumber = 1,
                    Name = "Luke Skywalker",
                    Balance = 1000,
                    IsOverDraftAllowed = true,
                    WithdrawLimit = BankAccountLimit.CurrentAccountWithdrawLimit,
                    OverDraftLimit = BankAccountLimit.OverDraftLimit,
                    AccountType = AccountType.Current
                },
                new BankAcount()
                {
                    AccountNumber = 2,
                    Name = "Darth Vader",
                    Balance = 1000,
                    IsOverDraftAllowed = false,
                    WithdrawLimit = BankAccountLimit.ZeroBalanceAccountWithdrawLimit,
                    AccountType = AccountType.ZeroBalance
                },
                new BankAcount(){
                    AccountNumber = 3,
                    Name = "Han Solo",
                    Balance = 20,
                    IsOverDraftAllowed= false,
                    WithdrawLimit = BankAccountLimit.SavingsAccountWithdrawLimit,
                    AccountType = AccountType.Savings
                }
            };

        }

        public BankAcount GetBankAcount(int accountNumber)
        {
            return _bankAcounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        }

        public BankAcount CreateAccount(CreateAccountDTO newAccount)
        {
            var newAccountNumber = _bankAcounts.Max(a => a.AccountNumber)+1;
            Enum.TryParse<AccountType>(newAccount.AccountType, out var accountType);

            var createdAccount = accountType switch
            {
                AccountType.Current => new BankAcount()
                {
                    AccountNumber=newAccountNumber,
                    Name = newAccount.Name,
                    AccountType=AccountType.Current,
                    Balance = 0,
                    IsOverDraftAllowed = true,
                    OverDraftLimit = BankAccountLimit.OverDraftLimit,
                    WithdrawLimit= BankAccountLimit.CurrentAccountWithdrawLimit
                },
                AccountType.Savings => new BankAcount()
                {
                    AccountNumber=newAccountNumber,
                    Name = newAccount.Name,
                    AccountType = AccountType.Savings,
                    Balance = 0,
                    IsOverDraftAllowed = false,
                    WithdrawLimit= BankAccountLimit.SavingsAccountWithdrawLimit
                },
                _ => new BankAcount()
                {
                    AccountNumber=newAccountNumber,
                    Name = newAccount.Name,
                    AccountType = AccountType.ZeroBalance,
                    Balance = 0,
                    IsOverDraftAllowed = false,
                    WithdrawLimit= BankAccountLimit.ZeroBalanceAccountWithdrawLimit
                },
            };
            _bankAcounts.Add(createdAccount);
            return createdAccount;
        }

        public void UpdateBalance(int accountNumber, int amount)
        {
            var account = _bankAcounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            var index = _bankAcounts.FindIndex(account => account.AccountNumber == accountNumber);
            if (index == -1)
            {
                throw new ArgumentException($"Account Number {accountNumber} does not exist");
            }
            var update = _bankAcounts[index] with
            {
                Balance = amount,
            };
            _bankAcounts.RemoveAt(index);
            _bankAcounts.Insert(index, update);
        }

        public BankAcount GetBankAccountSummary(int accountNumber)
        {
            return _bankAcounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        }


    }
}
