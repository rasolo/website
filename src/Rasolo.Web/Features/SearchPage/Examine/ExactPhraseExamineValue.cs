using Examine.Search;

namespace Rasolo.Web.Features.SearchPage.Examine
{
	/// <summary>
	/// This class helps searching for multiple words.
	/// </summary>
	public class ExactPhraseExamineValue : IExamineValue
	{
		/// <param name="phrase">The entire search phrase the user has entered.</param>
		public ExactPhraseExamineValue(string phrase)
		{
			Examineness = Examineness.Explicit;
			Value = $"\"{phrase}\"";
			Level = 1;
		}

		public Examineness Examineness { get; }
		public float Level { get; }
		public string Value { get; }
	}
}