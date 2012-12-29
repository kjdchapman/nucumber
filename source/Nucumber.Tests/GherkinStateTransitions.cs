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
        private IEnumerable<GherkinLineType> _allLineTypes;

        [SetUp]
        public void SetUp()
        {
            _allLineTypes = Enum.GetValues(typeof(GherkinLineType)).Cast<GherkinLineType>();
        }

        [Test]
        public void A_feature_header_is_valid_to_start_with()
        {
            foreach (var type in _allLineTypes.Where(type => type == GherkinLineType.FeatureHeader))
            {
                var gherkin = new Gherkin();

                TestStartingNewElement(gherkin, type, expectedValid: true);
            }
        }
        
        [Test]
        public void Everything_except_a_feature_header_is_not_valid_to_start_with()
        {
            foreach (var type in _allLineTypes.Where(type => type != GherkinLineType.FeatureHeader))
            {
                var gherkin = new Gherkin();

                TestStartingNewElement(gherkin, type, expectedValid: false);
            }
        }

        [Test]
        public void Scenario_header_and_typeless_line_are_valid_after_a_feature_header()
        {
            var typeBitmask = GherkinLineType.ScenarioHeader | GherkinLineType.None;
            var typesToTest = _allLineTypes.Where(type => typeBitmask.HasFlag(type));

            foreach (var type in typesToTest)
            {
                var gherkin = new Gherkin();
                gherkin.StartNewElement(GherkinLineType.FeatureHeader);

                TestStartingNewElement(gherkin, type, expectedValid: true);
            }
        }

        [Test]
        public void Everything_except_scenario_header_and_typeless_line_are_not_valid_after_a_feature_header()
        {
            var typeBitmask = GherkinLineType.ScenarioHeader | GherkinLineType.None;
            var typesToTest = _allLineTypes.Where(type => !typeBitmask.HasFlag(type));

            foreach (var type in typesToTest)
            {
                var gherkin = new Gherkin();
                gherkin.StartNewElement(GherkinLineType.FeatureHeader);

                TestStartingNewElement(gherkin, type, expectedValid: false);
            }
        }

        [Test]
        public void Given_When_and_Then_are_valid_after_a_scenario_header()
        {
            var typeBitmask = GherkinLineType.Given | GherkinLineType.When | GherkinLineType.Then;
            var typesToTest = _allLineTypes.Where(type => typeBitmask.HasFlag(type));

            foreach (var type in typesToTest)
            {
                var gherkin = new Gherkin();
                gherkin.StartNewElement(GherkinLineType.FeatureHeader);
                gherkin.StartNewElement(GherkinLineType.ScenarioHeader);
    
                TestStartingNewElement(gherkin, type, expectedValid: true);
            }
        }

        [Test]
        public void Everything_except_Given_When_and_Then_are_not_valid_after_a_scenario_header()
        {
            var typeBitmask = GherkinLineType.Given | GherkinLineType.When | GherkinLineType.Then;
            var typesToTest = _allLineTypes.Where(type => !typeBitmask.HasFlag(type));

            foreach (var type in typesToTest)
            {
                var gherkin = new Gherkin();
                gherkin.StartNewElement(GherkinLineType.FeatureHeader);
                gherkin.StartNewElement(GherkinLineType.ScenarioHeader);

                TestStartingNewElement(gherkin, type, expectedValid: true);
            }
        }


        [Test]
        public void Then_But_and_typeless_line_are_valid_after_a_When()
        {
            var typeBitmask = GherkinLineType.Then | GherkinLineType.But | GherkinLineType.None;
            var typesToTest = _allLineTypes.Where(type => typeBitmask.HasFlag(type));

            foreach (var type in typesToTest)
            {
                var gherkin = new Gherkin();
                gherkin.StartNewElement(GherkinLineType.FeatureHeader);
                gherkin.StartNewElement(GherkinLineType.ScenarioHeader);
                gherkin.StartNewElement(GherkinLineType.Given);
                gherkin.StartNewElement(GherkinLineType.When);

                TestStartingNewElement(gherkin, type, expectedValid: true);
            }
        }

        [Test]
        public void Everything_except_Then_But_And_typeless_line_are_not_valid_after_a_scenario_header()
        {
            var typeBitmask = GherkinLineType.Then | GherkinLineType.But | GherkinLineType.None;
            var typesToTest = _allLineTypes.Where(type => !typeBitmask.HasFlag(type));

            foreach (var type in typesToTest)
            {
                var gherkin = new Gherkin();
                gherkin.StartNewElement(GherkinLineType.FeatureHeader);
                gherkin.StartNewElement(GherkinLineType.ScenarioHeader);
                gherkin.StartNewElement(GherkinLineType.Given);
                gherkin.StartNewElement(GherkinLineType.When);

                TestStartingNewElement(gherkin, type, expectedValid: false);
            }
        }

        [Test]
        public void But_and_scenario_header_and_typeless_line_are_valid_after_a_Then()
        {
            var typeBitmask = GherkinLineType.But | GherkinLineType.ScenarioHeader | GherkinLineType.None;
            var typesToTest = _allLineTypes.Where(type => typeBitmask.HasFlag(type));

            foreach (var type in typesToTest)
            {
                var gherkin = new Gherkin();
                gherkin.StartNewElement(GherkinLineType.FeatureHeader);
                gherkin.StartNewElement(GherkinLineType.ScenarioHeader);
                gherkin.StartNewElement(GherkinLineType.Given);
                gherkin.StartNewElement(GherkinLineType.When);
                gherkin.StartNewElement(GherkinLineType.Then);

                TestStartingNewElement(gherkin, type, expectedValid: true);
            }
        }

        [Test]
        public void Everything_except_But_and_scenario_header_and_typeless_line_are_not_valid_after_a_Then()
        {
            var typeBitmask = GherkinLineType.But | GherkinLineType.ScenarioHeader | GherkinLineType.None;
            var typesToTest = _allLineTypes.Where(type => !typeBitmask.HasFlag(type));

            foreach (var type in typesToTest)
            {
                var gherkin = new Gherkin();
                gherkin.StartNewElement(GherkinLineType.FeatureHeader);
                gherkin.StartNewElement(GherkinLineType.ScenarioHeader);
                gherkin.StartNewElement(GherkinLineType.Given);
                gherkin.StartNewElement(GherkinLineType.When);
                gherkin.StartNewElement(GherkinLineType.Then);

                TestStartingNewElement(gherkin, type, expectedValid: false);
            }
        }

        private void TestStartingNewElement(Gherkin gherkin, GherkinLineType type, bool expectedValid)
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
