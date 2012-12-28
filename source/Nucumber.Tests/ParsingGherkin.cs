using System.Linq;
using System.Text;
using NUnit.Framework;
using Nucumber.Parsing;

namespace Nucumber.tests
{
	[TestFixture]
	class ParsingGherkin
	{
	    private string _inputFeature;
	    private GherkinParser _parser;

	    [SetUp]
		public void Given_we_are_parsing_this_feature_file()
		{
			var featureBuilder = new StringBuilder();
			featureBuilder.AppendLine("Feature: Example feature");
			featureBuilder.AppendLine();
			featureBuilder.AppendLine("As a something, etc etc");
			featureBuilder.AppendLine();
			featureBuilder.AppendLine("Scenario: Example 1");
			featureBuilder.AppendLine();
			featureBuilder.AppendLine("Given some precondition");
			featureBuilder.AppendLine("And some second precondition");
			featureBuilder.AppendLine("When I perform some action");
			featureBuilder.AppendLine("Then I will see some result");
			featureBuilder.AppendLine("But not some other result");

			_inputFeature = featureBuilder.ToString();
	        _parser = new GherkinParser();
		}

		[Test]
        public void Parsing_this_gherkin_returns_a_result()
		{
            var result = _parser.Parse(_inputFeature);
			Assert.That(result, Is.Not.Null);
		}

		[Test]
        public void Parsing_this_gherkin_returns_a_result_with_a_nonempty_feature_identifier()
		{
            var result = _parser.Parse(_inputFeature);
            Assert.That(result.Title, Is.Not.Empty);
		}

        [Test]
        public void Parsing_this_gherkin_returns_a_feature_with_the_correct_title()
        {
            var result = _parser.Parse(_inputFeature);
            Assert.That(result.Title, Is.EqualTo("Example feature"));
        }

		[Test]
        public void Parsing_this_gherkin_returns_a_result_with_a_nonempty_scenarios_collection()
		{
            var result = _parser.Parse(_inputFeature);
			Assert.That(result.Scenarios, Is.Not.Empty);
		}

        [Test]
        public void Parsing_this_gherkin_returns_a_feature_with_one_scenario()
        {
            var result = _parser.Parse(_inputFeature);
            Assert.That(result.Scenarios.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Parsing_this_gherkin_returns_a_feature_where_the_first_scenario_has_the_correct_description()
        {
            var result = _parser.Parse(_inputFeature);
            Assert.That(result.Scenarios.First().Description, Is.EqualTo("Example 1"));
        }
	}
}

