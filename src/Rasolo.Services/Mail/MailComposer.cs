using Rasolo.Services.Mail;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Rasolo.Services.Authentication
{
	public class MailComposer : IUserComposer
	{
		public void Compose(Composition composition)
		{
			composition.Register<IMailService, MailService>();
		}
	}
}