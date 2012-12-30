using System.Collections;
using System.Collections.Generic;
using System.IO;
using Nucumber.Parsing;

namespace Nucumber
{
    // NOTE: As we build search/filter functionality into this class, we want to separate it from the "editing files on disk" functionality.
    public class FeatureStore : IEnumerable<Gherkin>
    {
        private List<Gherkin> _features;
        
        public FeatureStore(string featuresPath)
        {
            _features = new List<Gherkin>();

            var gherkinParser = new GherkinParser();

            foreach (var featureFileName in Directory.GetFiles(featuresPath))
            {
                var rawText = File.ReadAllText(featureFileName);

                _features.Add(gherkinParser.Parse(rawText));
            }
        }

        public IEnumerator<Gherkin> GetEnumerator()
        {
            return _features.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}