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
            var lines = input.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).Select(ca => ca.ToString());

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

            // if (!titleLine.StartsWith("FeatureMarker:")) {throw new Exception("First line of the scenario must start with 'FeatureMarker:'");}

            return titleLine.TrimStart(FeatureMarker.ToCharArray());
        }

        private IEnumerable<GherkinScenario> GetScenarios(IEnumerable<string> lines)
        {
            var scenarios = new List<GherkinScenario>();
            var scenarioBuilder = new List<string>();
            
            foreach (var element in lines)
            {
                if (scenarioBuilder.Count.Equals(0))
                {
                    if (!element.StartsWith(ScenarioMarker)) continue;

                    scenarioBuilder.Add(element);
                }
                else
                {
                    if (element.StartsWith(ScenarioMarker))
                    {
                        scenarios.Add(CreateScenarioFrom(scenarioBuilder));
                        scenarios.Clear();
                    }

                    scenarioBuilder.Add(element);
                }
            }

            return scenarios;
        }

        private GherkinScenario CreateScenarioFrom(List<string> scenarioBuilder)
        {
            throw new NotImplementedException();
        }

        //private GherkinScenario CreateScenarioFrom(List<string> scenarioBuilder)
        //{
        //    var stepBuilder = new List<String>();

        //    foreach (var line in scenarioBuilder)
        //    {
        //        if (stepBuilder.Count.Equals(0))
        //        {
        //            if (line.StartsWith(ScenarioMarker)) { stepBuilder.Add(line); }
        //        }
        //        else
        //        {
        //            if (line.StartsWith(GivenMarker) || line.StartsWith(WhenMarker) || line.StartsWith(ThenMarker))
        //            {
                        
        //            }
        //        }
        //    }

        //    return new GherkinScenario();
        //}
    }
}