﻿using NUnit.Framework;
using Rasolo.Core.Features.Shared.Mappings;
using Rasolo.Core.Features.StartPage;
using Rasolo.Tests.Shared;

namespace Rasolo.Tests
{
	public class StartPageControllerTests : BaseContentPageControllerTests<StartPage>
	{
		public override void SetUp()
		{
			base.SetUp();
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			this._sut = new StartPageController(umbracoMapper);
		}
	}
}