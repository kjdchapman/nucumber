using System;
using System.Collections.Generic;
using System.Linq;

namespace Nucumber.Parsing
{
    public class GherkinParser
    {
        public Gherkin Parse(string input)
        {
            input = input.Replace("\t", string.Empty);
            var lines = input.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).Select(ca => ca.ToString()).ToList();

            var linesAndTypes = new Dictionary<string, GherkinLineType>();

            foreach (var line in lines)
            {
                var lineType = Gherkin.GetLineType(line);
                linesAndTypes.Add(line, lineType);
            }

            var gherkin = new Gherkin();
            var previousType = GherkinLineType.None;
            
            foreach (var lineAndType in linesAndTypes)
            {
                var currentLine = lineAndType.Key;
                var currentType = lineAndType.Value;

                if (!(currentType == previousType || currentType == GherkinLineType.None))
                {
                    gherkin.StartNewElement(currentType);
                }

                gherkin.WriteToCurrentElement(currentLine);
                
                previousType = lineAndType.Value;
            }

            return gherkin;
        }
    }
}