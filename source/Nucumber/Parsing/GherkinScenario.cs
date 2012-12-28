using System.Collections.Generic;

namespace Nucumber.Parsing
{
    public struct GherkinScenario
    {
        public string Description { get; set; }

        public List<string> Givens { get; set; }
        public List<string> Whens { get; set; }
        public List<string> Thens { get; set; }
    }
}