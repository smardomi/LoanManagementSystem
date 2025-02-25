using LoanManagementSystem.Domain.Common;

namespace LoanManagementSystem.Domain.Exceptions
{
    public class DueDateShouldNotBeLessThanIssueDateException : DomainException
    {
        public override string Message => "تاریخ سررسید نباید از تاریخ صدور کوچکتر باشد.";
    }
}
