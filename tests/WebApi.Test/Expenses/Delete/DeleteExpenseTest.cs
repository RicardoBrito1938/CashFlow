using System.Net;
using System.Text.Json;
using CashFlow.Exception;
using Shouldly;

namespace WebApi.Test.Expenses.Delete;


public class DeleteExpenseTest : CashFlowClassFixture
    {
        private const string Method = "api/Expenses/";
        private readonly string _token;
        private readonly long _expenseId;

        public DeleteExpenseTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _token = factory.User_Team_Member.GetToken();
            _expenseId = factory.Expense.GetExpenseId();
        }

        [Fact]
        public async Task Success()
        {
            var result = await DoDelete(requestUri: Method + _expenseId, token: _token);
            result.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }
        
        [Fact]
        public async Task Error_Expense_Not_Found()
        {
            var result = await DoDelete(requestUri: Method + 999999, token: _token, "pt-BR");
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
        
    }
