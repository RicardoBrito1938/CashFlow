﻿using Bogus;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using Tag = CashFlow.Domain.Enums.Tag;

namespace CommonTestUtils.Entities;

public class ExpenseBuilder
{
    public static List<Expense> Collection(User user, uint count = 2)
    {
        var expenses = new List<Expense>();
        for (uint i = 0; i < count; i++)
        {
           var expense = Build(user);
           expense.Id = expenses.Count + 1;
            expenses.Add(expense);
        }
        return expenses;
    }

    public static Expense Build(User user)
    {
        return new Faker<Expense>()
            .RuleFor(u => u.Id, _ => 1)
            .RuleFor(u => u.Title, faker => faker.Commerce.ProductName())
            .RuleFor(u => u.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(u => u.Date, faker => faker.Date.Past())
            .RuleFor(u => u.Amount, faker => faker.Random.Decimal(1, 1000))
            .RuleFor(r => r.PaymentType, faker => faker.PickRandom<PaymentType>())
            .RuleFor(u => u.UserId, _ => user.Id)
            .RuleFor(rule => rule.Tags, faker => faker.Make(1, () => new CashFlow.Domain.Entities.Tag()
            {
                Id = 1,
                Value = faker.PickRandom<CashFlow.Domain.Enums.Tag>(),
                ExpenseId = 1
            }));
    }
}