
namespace LoanManagementSystem.Domain.Enums
{
    public enum CheckStatus
    {
        Pending = 1,   // در انتظار
        Paid = 2,      // پرداخت شده
        Bounced = 3,   // برگشت خورده
        Cancelled = 4, // لغو شده
        Cleared = 5   // پاک شده (چک تایید شده یا مبلغ از حساب پرداخت شده)
    }
}
