using System;
using System.Collections.Generic;

namespace Nucumber.Parsing
{
    public class Gherkin
    {
        private const string FeatureMarker = "Feature:";
        private const string ScenarioMarker = "Scenario:";
        private const string GivenMarker = "Given";
        private const string WhenMarker = "When";
        private const string ThenMarker = "Then";
        private const string ButMarker = "But";

        public string Title { get; private set; }
        public string Blurb { get; private set; }
        public IEnumerable<GherkinScenario> Scenarios { get; private set; }

        protected GherkinLineType State { get; set; }

        public static GherkinLineType GetLineType(string line)
        {
            var type = GherkinLineType.None;

            if (line.StartsWith(FeatureMarker)) { type = GherkinLineType.FeatureHeader; }
            if (line.StartsWith(ScenarioMarker)) { type = GherkinLineType.ScenarioHeader; }

            if (line.StartsWith(GivenMarker)) { type = GherkinLineType.Given; }
            if (line.StartsWith(WhenMarker)) { type = GherkinLineType.When; }
            if (line.StartsWith(ThenMarker)) { type = GherkinLineType.Then; }
            if (line.StartsWith(ButMarker)) { type = GherkinLineType.But; }

            return type;
        }

        public Gherkin()
        {
            State = GherkinLineType.None;
        }

        public void StartNewElement(GherkinLineType newState)
        {
            var valid = false;

            switch (newState)
            {
                case GherkinLineType.FeatureHeader:
                    valid = true;
                    break;
                case GherkinLineType.ScenarioHeader:
                    break;
                case GherkinLineType.Given:
                    break;
                case GherkinLineType.When:
                    break;
                case GherkinLineType.Then:
                    break;
                case GherkinLineType.But:
                    break;
                case GherkinLineType.None:
                    break;
            }

            if (!valid) {throw new InvalidOperationException
                (String.Format("Cannot create {0} element after {1} element.", newState, State));}

            if (!newState.HasFlag(GherkinLineType.None | GherkinLineType.But)) { this.State = newState; }
        }

        public void WriteToCurrentElement(string currentLine)
        {
            throw new NotImplementedException();
        }
    }
};