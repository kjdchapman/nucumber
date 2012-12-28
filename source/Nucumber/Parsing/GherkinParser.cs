using System.Collections.Generic;

namespace Nucumber.Parsing
{
    public class GherkinParser
    {
        public Gherkin Parse(string input)
        {
            return new Gherkin
            {
                Title = "anything", 
                Scenarios = new List<GherkinScenario>{new GherkinScenario()}
            };
        }
    }
}