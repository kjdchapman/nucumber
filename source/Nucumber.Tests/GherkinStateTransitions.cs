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
        public void To_start_a_feature_file_a_feature_header_is_valid()
        {
            foreach (var type in _allLineTypes.Where(type => type == GherkinLineType.FeatureHeader))
            {
                var gherkin = new Gherkin();

                TestStartingNewElement(gherkin, type, expectedValid: true);
            }
        }
        
        [Test]
        public void To_start_a_feature_file_everything_except_a_feature_header_is_not_valid()
        {
            foreach (var type in _allLineTypes.Where(type => type != GherkinLineType.FeatureHeader))
            {
                var gherkin = new Gherkin();

                TestStartingNewElement(gherkin, type, expectedValid: false);
            }
        }

        [Test]
        public void After_a_feature_header_a_scenario_header_or_typeless_line_is_valid()
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
        public void After_a_feature_header_everything_except_a_scenario_header_or_typeless_line_is_not_valid()
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
        public void After_a_scenario_header_Given_When_and_Then_are_valid()
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
        public void After_a_scenario_header_everything_except_Given_When_and_Then_are_not_valid()
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
        public void After_a_Given_element_When_or_Then_or_But_or_a_typeless_line_are_valid()
        {
            var typeBitmask = GherkinLineType.When | GherkinLineType.Then | GherkinLineType.But | GherkinLineType.None;
            var typesToTest = _allLineTypes.Where(type => typeBitmask.HasFlag(type));

            foreach (var type in typesToTest)
            {
                var gherkin = new Gherkin();
                gherkin.StartNewElement(GherkinLineType.FeatureHeader);
                gherkin.StartNewElement(GherkinLineType.ScenarioHeader);
                gherkin.StartNewElement(GherkinLineType.Given);

                TestStartingNewElement(gherkin, type, expectedValid: true);
            }
        }

        [Test]
        public void After_a_Given_element_everything_except_When_or_Then_or_But_or_a_typeless_line_are_not_valid()
        {
            var typeBitmask = GherkinLineType.When | GherkinLineType.Then | GherkinLineType.But | GherkinLineType.None;
            var typesToTest = _allLineTypes.Where(type => !typeBitmask.HasFlag(type));

            foreach (var type in typesToTest)
            {
                var gherkin = new Gherkin();
                gherkin.StartNewElement(GherkinLineType.FeatureHeader);
                gherkin.StartNewElement(GherkinLineType.ScenarioHeader);
                gherkin.StartNewElement(GherkinLineType.Given);

                TestStartingNewElement(gherkin, type, expectedValid: false);
            }
        }


        [Test]
        public void After_a_When_element_Then_or_But_or_a_typeless_line_are_valid()
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
        public void After_a_When_element_everything_exception_Then_or_But_or_a_typeless_line_are_not_valid()
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
        public void After_a_Then_element_But_or_a_scenario_header_or_a_typeless_line_are_valid()
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
        public void After_a_Then_element_everything_except_But_or_a_scenario_header_or_a_typeless_line_are_not_valid()
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
                    String.Format("No exceptions thrown for state transition to {0} - expected the state transition to be not valid.", type));
            }
            catch (Exception ex)
            {
                Assert.That(expectedValid, Is.EqualTo(false),
                    string.Format("Exception thrown on state transition to {0} - {1}", type, ex.StackTrace));
            }
            
        }
    }
}
