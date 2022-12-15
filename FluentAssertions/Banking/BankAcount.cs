namespace Banking
{
    public record BankAcount
    {
        public int AccountNumber { get; init; }
        public string Name { get; init; }
        public AccountType AccountType { get; init; }
        public int Balance { get; init; }
        public bool IsOverDraftAllowed { get; init; }
        public int OverDraftLimit { get; init; }
        public int WithdrawLimit { get; init; }

    }

    public enum AccountType
    {
        Savings,
        Current,
        ZeroBalance
    }

    public class BankAccountDTO
    {
        public int AccountNumber { get; set; }
        public AccountType AccountType { get; set; }
        public int BalanceAmount { get; set; }
    }

    public class CreateAccountDTO
    {
        public string AccountType { get; set; }
        public string Name { get; set; }

    }
}