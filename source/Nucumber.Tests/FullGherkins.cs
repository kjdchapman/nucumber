using NUnit.Framework;
using Nucumber.Parsing;

namespace Nucumber.tests
{
    class FullGherkins
    {
        [Test]
        public void Passing_through_states_of_a_basic_feature_file_throws_no_exception()
        {
            var gherkin = new Gherkin();
            gherkin.StartNewElement(GherkinLineType.FeatureHeader);     // Feature: Example feature
            gherkin.StartNewElement(GherkinLineType.None);              // As a role
            gherkin.StartNewElement(GherkinLineType.None);              // I want to do something
            gherkin.StartNewElement(GherkinLineType.None);              // So that I get some benefit
            gherkin.StartNewElement(GherkinLineType.ScenarioHeader);    // Scenario: Example 1
            gherkin.StartNewElement(GherkinLineType.Given);             // Given some precondition
            gherkin.StartNewElement(GherkinLineType.When);              // When I perform some action
            gherkin.StartNewElement(GherkinLineType.Then);              // Then I expect some result
        }
        
    }
}
