using System;
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
        private static FeatureStore _instance;
        private static string _featuresPath;

        private FeatureStore(string featuresPath)
        {
            _features = new List<Gherkin>();

            var gherkinParser = new GherkinParser();

            foreach (var featureFileName in Directory.GetFiles(featuresPath))
            {
                var rawText = File.ReadAllText(featureFileName);

                _features.Add(gherkinParser.Parse(rawText));
            }
        }

        public static void SetFeaturePath(string path)
        {
            _featuresPath = path;
        }

        public static FeatureStore GetInstance()
        {
            if (String.IsNullOrEmpty(_featuresPath)) {throw new InvalidOperationException("Cannot get a valid instance until a path to features has been set.");}
            return _instance ?? (_instance = new FeatureStore(_featuresPath));
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