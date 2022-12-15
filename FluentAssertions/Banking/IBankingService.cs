namespace Banking
{
    interface IBankingService
    {
        bool DepositAmount(int accountNumber, int amount);
        bool WithdrawAmount(int accountNumber, int amount);
        int CheckBalance(int accountNumber);
        BankAccountDTO GetBankAccountSummary(int accountNumber);
        BankAccountDTO CreateAccount(CreateAccountDTO newAccount);

    }
}
