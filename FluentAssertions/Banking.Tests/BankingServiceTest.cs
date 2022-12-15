using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using System.Collections.Generic;

namespace Banking.Tests
{
    public class BankingServiceTests
    {
        private BankingService _bankingService;
        [SetUp]
        public void Setup()
        {
            _bankingService = new BankingService();
        }

        [Test]
        public void GetBankAccountSummary_shall_return_details_when_accountNumber_exists()
        {

            var accountNumber = 1;
            var expectedAccount = new
            {
                AccountNumber = accountNumber,
                AccountType = AccountType.Current,
                BalanceAmount = 1000
            };
            var existingAccount = _bankingService.GetBankAccountSummary(accountNumber);
            existingAccount.Should().NotBeNull();
            existingAccount.AccountNumber.Should().Be(accountNumber);
            existingAccount.Should().BeEquivalentTo(expectedAccount);

            var unknownAccountNumber = 20;
            var unknownAccount = _bankingService.GetBankAccountSummary(unknownAccountNumber);
            unknownAccount.Should().BeNull();

        }

        [Test]
        public void CheckBalance_shall_return_balanceAmount()
        {
            var accountNumber = 1;
            var expectedBalance = 1000;
            var balance = _bankingService.CheckBalance(accountNumber);
            balance.Should().BePositive();
            balance.Should().Be(expectedBalance);

        }


    }
}