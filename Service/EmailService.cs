using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net;
using System.Net.Mail;
using System.Web;
using WorkTimeAPI.Model;
public class EmailService
{
    private readonly PasswordHasher<User> passwordHasher = new();

    public async Task SendEmailAsync(EmailRequest request)
    {
        string Password = Guid.NewGuid().ToString("d").Substring(1, 8);

        User userModel = new User()
        {
            Email = request.To,
            Login = request.To,
            Password = Password

        };

        userModel.Password = passwordHasher.HashPassword(userModel, Password);

        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("arekwsb123@gmail.com", "kmau xdlv khcj rddt"),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress("arekwsb123@gmail.com"),
            Subject = "Reset has³a WorkTime APP",
            Body = $"Twoje nowe has³o to {Password}", // Include the generated password in the email body
            IsBodyHtml = true,
        };
        mailMessage.To.Add(request.To);

        await smtpClient.SendMailAsync(mailMessage);
    }
}
