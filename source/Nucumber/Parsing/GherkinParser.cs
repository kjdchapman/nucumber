using System;
using System.Collections.Generic;
using System.Linq;

namespace Nucumber.Parsing
{
    public class GherkinParser
    {
        private const string FeatureMarker = "FeatureMarker: ";
        private const string ScenarioMarker = "Scenario: ";
        private const string GivenMarker = "Given ";
        private const string WhenMarker = "When ";
        private const string ThenMarker = "Then ";

        public Gherkin Parse(string input)
        {
            var lines = input.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).Select(ca => ca.ToString()).ToList();

            return new Gherkin
            {
                Title = GetTitle(lines),
                // Blurb = GetBlurb(lines),
                Scenarios = GetScenarios(lines)
            };
        }

        private string GetTitle(IEnumerable<string> lines)
        {
            var titleLine = lines.First();
            return titleLine.TrimStart(FeatureMarker.ToCharArray());
        }

        private IEnumerable<GherkinScenario> GetScenarios(List<string> lines)
        {
            var scenarios = new List<GherkinScenario>();
            var scenarioBuilder = new List<string>();

            var originalCount = lines.Count();
            var counter = 0;

            foreach (var element in lines)
            {
                counter++;

                if (scenarioBuilder.Count.Equals(0))
                {
                    if (!element.StartsWith(ScenarioMarker)) continue;

                    scenarioBuilder.Add(element);
                }
                else
                {
                    if (element.StartsWith(ScenarioMarker) || counter == originalCount)
                    {
                        scenarios.Add(CreateScenarioFrom(scenarioBuilder));
                        scenarioBuilder.Clear();
                    }

                    scenarioBuilder.Add(element);
                }
            }

            return scenarios;
        }

        private GherkinScenario CreateScenarioFrom(List<string> scenarioBuilder)
        {
            var description = string.Empty;
            var givens = new List<string>();
            var whens = new List<string>();
            var thens = new List<string>();

            var stage = ScenarioBuildingStage.NoneComplete;

            foreach (var line in scenarioBuilder)
            {
                if (line.StartsWith(ScenarioMarker))
                {
                    description = line.TrimStart(ScenarioMarker.ToCharArray());
                    stage = ScenarioBuildingStage.TitleComplete;
                }
                else
                {
                    if (line.StartsWith(GivenMarker) || stage < ScenarioBuildingStage.GivensComplete)
                    {
                        givens.Add(line);
                        stage = ScenarioBuildingStage.TitleComplete;
                    }
                    else if (line.StartsWith(WhenMarker) || stage < ScenarioBuildingStage.WhensComplete)
                    {
                        whens.Add(line);
                        stage = ScenarioBuildingStage.GivensComplete;
                    }
                    else if (line.StartsWith(ThenMarker) || stage < ScenarioBuildingStage.ThensComplete)
                    {
                        thens.Add(line);
                        stage = ScenarioBuildingStage.WhensComplete;
                    }
                }
            }

            return new GherkinScenario
            {
                Description = description,
                Givens = givens,
                Whens = whens,
                Thens = thens
            };
        }
    }

    public enum ScenarioBuildingStage
    {
        NoneComplete = 0,
        TitleComplete = 1,
        GivensComplete = 2,
        WhensComplete = 3,
        ThensComplete = 4
    }
}