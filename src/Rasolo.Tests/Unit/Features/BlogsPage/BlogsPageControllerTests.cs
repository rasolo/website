using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.BlogsPage;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Tests.Unit.Shared.Compositions.BaseContentPage;
using Shouldly;

namespace Rasolo.Tests.Unit.Features.BlogsPage
{
	internal class BlogsPageControllerTests : BaseContentPageControllerTests<Core.Features.BlogsPage.BlogsPage>
	{
	}
}
