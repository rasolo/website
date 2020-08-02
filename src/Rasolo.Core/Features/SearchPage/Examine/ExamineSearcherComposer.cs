using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Rasolo.Core.Features.SearchPage.Examine
{
	public class ExamineSearcherComposer : IUserComposer
	{
		public void Compose(Composition composition)
		{
			composition.Register<IExamineSearcher, ExamineSearcher>();
		}
	}
}
