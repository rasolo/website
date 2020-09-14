using System;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Configuration;
using System.Threading.Tasks;
using Umbraco.Core.Composing;

namespace Rasolo.Services.Mail
{
	public class MailService : IMailService
	{
		public async Task SendMail(string textContent, string htmlContent)
		{
			try
			{
				var apiKey = ConfigurationManager.AppSettings["SendGrid.ApiKey"];
				var client = new SendGridClient(apiKey);
				var from = new EmailAddress("rasmusolofssons@gmail.com", "Rasolo.net");
				var subject = "A new comment has been submitted.";
				var to = new EmailAddress(Constants.Project.Email, "Rasmus Olofsson");
				var msg = MailHelper.CreateSingleEmail(from, to, subject, textContent, htmlContent);

				var response = await client.SendEmailAsync(msg);
				await response.Body.ReadAsStringAsync();
			}
			catch (Exception e)
			{
				Current.Logger.Error(typeof(MailService), e.Message);
			}
		}
	}
}
