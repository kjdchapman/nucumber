using System.Collections.Generic;

namespace Nucumber
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

    public struct Gherkin
    {
        public string Title { get; set; }

        public IEnumerable<GherkinScenario> Scenarios { get; set; }
    }

    public struct GherkinScenario
    {
        public string Description { get; set; }
    }
}