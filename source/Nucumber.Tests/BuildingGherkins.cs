using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Nucumber.Parsing;

namespace Nucumber.tests
{
    [TestFixture]
    public class BuildingGherkins
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
        
        [Test]
        public void Only_a_feature_header_is_valid_to_start_with()
        {
            // TODO: pull up to class
            var allGherkinTypes = new List<GherkinLineType>();

            var validTypes = new List<GherkinLineType>{GherkinLineType.FeatureHeader};
            var invalidTypes = allGherkinTypes.Except(validTypes);

            // TODO: replace with Assert.AreInvalid(invalidTypes);
            foreach (var type in invalidTypes)
            {
                try
                {
                    var gherkin = new Gherkin();
                    gherkin.StartNewElement(type);
                }
                catch (Exception ex)
                {
                    Assert.Fail("Transitioning from state of 'none' to state of `{0}' caused an Exception: {1}", type, ex.StackTrace);
                }
            }
        }

        [Test]
        public void Only_scenario_header_and_typeless_line_are_valid_after_a_feature_header()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void Only_a_Given_When_or_Then_is_valid_after_a_scenario_header()
        {
            // TODO: fill in invalid types
            
            var gherkin = new Gherkin();
            gherkin.StartNewElement(GherkinLineType.FeatureHeader);
            gherkin.StartNewElement(GherkinLineType.ScenarioHeader);
            
            Assert.Fail("Test writing not yet complete.");
        }

        [Test]
        public void Only_a_When_or_Then_or_And_or_But_or_typeless_line_is_valid_after_a_Given()
        {
            // TODO: fill in invalid types

            var gherkin = new Gherkin();
            gherkin.StartNewElement(GherkinLineType.FeatureHeader);
            gherkin.StartNewElement(GherkinLineType.ScenarioHeader);
            gherkin.StartNewElement(GherkinLineType.Given);

            Assert.Fail("Test writing not yet complete.");
        }


        [Test]
        public void Only_a_Then_or_But_or_typeless_line_is_valid_after_a_When()
        {
            // TODO: fill in invalid types

            var gherkin = new Gherkin();
            gherkin.StartNewElement(GherkinLineType.FeatureHeader);
            gherkin.StartNewElement(GherkinLineType.ScenarioHeader);
            gherkin.StartNewElement(GherkinLineType.Given);
            gherkin.StartNewElement(GherkinLineType.When);

            Assert.Fail("Test writing not yet complete.");
        }

        [Test]
        public void Only_a_But_or_scenario_header_or_typeless_line_is_valid_after_a_Then()
        {
            // TODO: fill in invalid types

            Assert.Fail("Test writing not yet complete");
        }
    }
}
