using LoanManagementSystem.Domain.Common;
using LoanManagementSystem.Domain.Enums;
using LoanManagementSystem.Domain.Exceptions;

namespace LoanManagementSystem.Domain.Entities.Financial
{
    public class Check : BaseEntity
    {
        public Check(long amount, DateOnly issueDate, DateOnly dueDate, string bankName, string checkNumber, string payee)
        {
            Amount = amount;
            IssueDate = issueDate;            
            BankName = bankName;
            Status = CheckStatus.Pending;
            CheckNumber = checkNumber;
            Payee = payee;

            SetDueDate(dueDate);
        }

        public long Amount { get; private set; }
        public DateOnly IssueDate { get; private set; }
        public DateOnly DueDate { get; private set; }
        public string Payee { get; private set; }
        public CheckStatus Status { get; private set; }
        public string BankName { get; private set; }
        public string CheckNumber { get; private set; }
        public string? Note { get; private set; }

        public void SetDueDate(DateOnly dueDate)
        {
            if (dueDate < IssueDate) throw new DueDateShouldNotBeLessThanIssueDateException();

            DueDate = dueDate;
        }

        public void SetNote(string note)
        {
            Note = note;
        }
        
    }

}
