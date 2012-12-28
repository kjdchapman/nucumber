using System.Collections.Generic;

namespace Nucumber.Parsing
{
    public struct Gherkin
    {
        public string Title { get; set; }
        public IEnumerable<GherkinScenario> Scenarios { get; set; }
    }
}