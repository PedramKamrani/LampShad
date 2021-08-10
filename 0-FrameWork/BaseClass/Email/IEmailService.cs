namespace _0_FrameWork.BaseClass.Email
{
    public interface IEmailService
    {
        void SendEmail(string title, string messageBody, string destination);
    }
}