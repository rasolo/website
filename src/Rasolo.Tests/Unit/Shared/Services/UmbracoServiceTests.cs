using Moq;
using Rasolo.Core.Features.Shared.Services;
using System.Web;
using Umbraco.Web;

namespace Rasolo.Tests.Unit.Shared.Services
{
	public class UmbracoServiceTests
	{
		private readonly UmbracoService _sut;

		public UmbracoServiceTests()
		{
			var umbracoContextFactoryMock = new Mock<IUmbracoContextFactory>();
			umbracoContextFactoryMock.Setup(x => x.EnsureUmbracoContext(It.IsAny<HttpContextBase>()))
				.Returns(It.IsAny<UmbracoContextReference>());
			this._sut = new UmbracoService(umbracoContextFactoryMock.Object);
		}
	}
}
