# Unit Tests with Fluent Assertions

FluentAssertions in unit testing helps to make the unit tests more readable, provides flexibility to add extensions and adds fun to writing unit tests  !!

For e.g.

````
var accountBalance = -1 ;
Assert.That(accountBalance , Is.GreaterThan(0));

  Message: 
  Expected: greater than 0
  But was:  -1
````
This message from Assertion tells us only about the condition failed. We will replace the same with FluentAssertion and see how it makes it more readable

## Setup

Just add NuGet package “FluentAssertions” to your test project and include below ```using``` statement to yout test file

```
using FluentAssertions;

```

Now we can see how the assertion works using FluentAssertions

````
var accountBalance = -1;
accountBalance.Should().BePositive();

Message: 
Expected accountBalance to be positive, but found -1.
````
Here the message is very clear that accountBalance value is supposed to positive but found actual value as -1 . 

Here is one more example 

```
var companyName = "Kongsberg Digital";
companyName.Should().Be("Kongsberg Maritime");

Message: 
Expected companyName to be "Kongsberg Maritime" with a length of 18, but "Kongsberg Digital" has a length of 17, differs near "Dig" (index 10).


```

Is n't it more readable without adding more details in assertion !!

## Assertions

### Numeric Assertions
We can use the numeric assertions 

```
int intBalance = 10;
intBalance.Should().BeGreaterThan(0);
intBalance.Should().BeLessThan(11);
intBalance.Should().BeGreaterThanOrEqualTo(0);
intBalance.Should().Be(10);

We can use range as below 

float floatBalance = 11.3456f;
floatBalance.Should().BeInRange(1,12);

```

### Object Assertions

```
            var accountNumber = 1;
            var expectedAccount = new
            {
                AccountNumber = accountNumber,
                AccountType = AccountType.Current,
                BalanceAmount = 1000
            };

            //returns DTO object 
            var existingAccountDTO = _bankingService.GetBankAccountSummary(accountNumber);

            existingAccountDTO.Should().Be(expectedAccount); // this will fail as the type does not match

            However, if we just want to match based on property names and their values, we can use 

            existingAccount.Should().BeEquivalentTo(expectedAccount);

            Read more in Object graph comparison 
```

### Collections and Dictionary

```
int[] Numbers = new[] { 1, 2, 3, 4 };
            Numbers.Should()
                .NotBeEmpty()
                .And.HaveCount(4)
                .And.StartWith(1)
                .And.ContainInOrder(new[] { 1, 2, 3, 4 });

Did you observe chaining !! 
```

````
Dictionary<int, string> students = new Dictionary<int, string>
            {
                {1, "Luke Skywalker" },
                {2, "Darth Vader" },
                {3, "Obi-Won" }
            };

Dictionary<int, string> expectedStudents = new Dictionary<int, string>
            {
                {1, "Luke Skywalker" },
                {2, "Darth Vader" },
                {3, "Obi-Won" }
            };
            students.Should().ContainKey(1);
            students.Should().ContainKeys(1,2,3);
            students.Should().ContainValue("Darth Vader");
            students.Should().BeEquivalentTo(expectedStudents);

```

### Assertion Scopes

Have you faced sceanrios where test contains multiple assert statements and we just to know all failed assertions rather than just the first one?

We can do it in FluentAssertions using Assertion Scope

```
 using (new AssertionScope())
        {
                var number = 100;
                number.Should().BePositive();
                number.Should().BeInRange(1, 10);
                number.Should().Be(5);

        }

Test runs all assertions and gives test log as below  

Message: 
Expected number to be between 1 and 10, but found 100.
Expected number to be 5, but found 100 (difference of 95).

```
