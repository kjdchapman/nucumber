using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Nucumber.Parsing;

namespace Nucumber.tests
{
    [TestFixture]
    public class GherkinStateTransitions
    {
        [Test]
        public void Only_a_feature_header_is_valid_to_start_with()
        {
            var gherkin = new Gherkin();
            TestStateChange(gherkin, GherkinLineType.FeatureHeader, true);
        }
        
        [Test]
        public void Everything_but_a_feature_header_is_invalid_to_start_with()
        {
            var gherkin = new Gherkin();
            TestStateChange(gherkin, LineTypesExcept(GherkinLineType.FeatureHeader), false);
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

        private List<GherkinLineType> LineTypesExcept(GherkinLineType typeToExclude)
        {
            return LineTypesExcept(new List<GherkinLineType> {typeToExclude});
        }

        private List<GherkinLineType> LineTypesExcept(IEnumerable<GherkinLineType> typesToExclude)
        {
            var allLineTypes = Enum.GetValues(typeof(GherkinLineType)).Cast<GherkinLineType>();
            return allLineTypes.Except(typesToExclude).ToList();
        }


        private void TestStateChange(Gherkin gherkin, GherkinLineType typesUnderTest, bool expectedValid)
        {
            TestStateChange(gherkin, new List<GherkinLineType>{typesUnderTest}, expectedValid);
        }

        private void TestStateChange(Gherkin gherkin, List<GherkinLineType> typesUnderTest, bool expectedValid)
        {
            foreach (var type in typesUnderTest)
            {
                try
                {
                    gherkin.StartNewElement(type);
                    Assert.That(expectedValid, Is.EqualTo(true), 
                        String.Format("No exceptions thrown for state transition to {0} - expected an invalid state transition.", type));
                }
                catch (Exception ex)
                {
                    Assert.That(expectedValid, Is.EqualTo(false),
                                string.Format("Exception thrown on state transition to {0} - {1}", type, ex.StackTrace));
                }
            }
        }
    }
}
