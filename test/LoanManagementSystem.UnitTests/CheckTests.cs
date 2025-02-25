using LoanManagementSystem.Domain.Entities.Financial;
using LoanManagementSystem.Domain.Exceptions;
using Microsoft.VisualBasic;
using Shouldly;

namespace LoanManagementSystem.UnitTests
{
    public class CheckTests
    {
        private readonly DateOnly _issueDate;
        private readonly DateOnly _dueDate;
        private const string BankName = "بانک ملت";
        private const string CheckNumber = "45454545";
        private const string Payee = "خرید لوازم خانه";

        public CheckTests()
        {
            _issueDate = DateOnly.FromDateTime(DateTime.Now);
            _dueDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(1));
        }

        private Check CreateCheck(long amount, DateOnly issueDate, DateOnly dueDate, string bankName, string checkNumber, string payee)
        {
            return new Check(amount, issueDate, dueDate, bankName, checkNumber, payee);
        }

        [Fact]
        public void Check_ShouldBeCreated_WithValidData()
        {
            var check = CreateCheck(25000000, _issueDate, _dueDate, BankName, CheckNumber, Payee);

            check.Amount.ShouldBe(25000000);
            check.IssueDate.ShouldBe(_issueDate);
            check.DueDate.ShouldBe(_dueDate);
            check.BankName.ShouldBe(BankName);
            check.CheckNumber.ShouldBe(CheckNumber);
            check.Payee.ShouldBe(Payee);           
        }

        [Fact]
        public void Check_ShouldThrowException_When_DueDate_LessThan_IssueDate()
        {           
            Action action = () => CreateCheck(25000000, _issueDate, _dueDate, BankName, CheckNumber, Payee); 

            action.ShouldThrow<DueDateShouldNotBeLessThanIssueDateException>();
        }

        [Fact]
        public void Check_ShouldSetStatus_As_Pendding_When_Created()
        {
            var check = CreateCheck(25000000, _issueDate, _dueDate, BankName, CheckNumber, Payee);

            check.Status.ShouldBe(Domain.Enums.CheckStatus.Pending);
        }
    }
}
