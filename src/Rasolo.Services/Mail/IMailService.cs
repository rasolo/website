using System.Threading.Tasks;

namespace Rasolo.Services.Mail
{
	public interface IMailService
	{
		 Task SendMail(string textContent, string htmlContent);
	}
}
