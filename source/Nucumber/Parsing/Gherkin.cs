using System;
using System.Collections.Generic;

namespace Nucumber.Parsing
{
    public class Gherkin
    {
        public const string FeatureMarker = "Feature:";
        public const string ScenarioMarker = "Scenario:";
        public const string GivenMarker = "Given";
        public const string WhenMarker = "When";
        public const string ThenMarker = "Then";
        public const string ButMarker = "But";

        public string Title { get; private set; }
        public string Blurb { get; private set; }

        private GherkinScenario _currentScenario;
        private GherkinLineType _currentParsingLineType;

        private readonly List<GherkinScenario> _scenarios;

        public IEnumerable<GherkinScenario> Scenarios
        {
            get { return _scenarios; }
        }

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
            _scenarios = new List<GherkinScenario>();
            _currentParsingLineType = GherkinLineType.None;
        }

        public void StartNewElement(GherkinLineType newState)
        {
            var valid = false;

            switch (this._currentParsingLineType)
            {
                case GherkinLineType.None:
                    valid = newState == GherkinLineType.FeatureHeader;
                    break;
                case GherkinLineType.FeatureHeader:
                    valid = (newState == GherkinLineType.ScenarioHeader) || (newState == GherkinLineType.None);
                    break;
                case GherkinLineType.ScenarioHeader:
                    valid = (newState == GherkinLineType.Given) || (newState == GherkinLineType.When) ||
                            (newState == GherkinLineType.Then);
                    break;
                case GherkinLineType.Given:
                    valid = (newState == GherkinLineType.When) || (newState == GherkinLineType.Then) ||
                            (newState == GherkinLineType.But) || (newState == GherkinLineType.None);
                    break;
                case GherkinLineType.When:
                    valid = (newState == GherkinLineType.Then) || (newState == GherkinLineType.But) ||
                            (newState == GherkinLineType.None);
                    break;
                case GherkinLineType.Then:
                    valid = (newState == GherkinLineType.But) || (newState == GherkinLineType.None) ||
                            (newState == GherkinLineType.ScenarioHeader);
                    break;
                default:
                    throw new Exception("Current state of Gherkin object unexpected.");
            }

            if (!valid) {throw new InvalidOperationException
                (String.Format("Cannot create {0} element after {1} element.", newState, _currentParsingLineType));}

            if (!newState.HasFlag(GherkinLineType.None | GherkinLineType.But)) { this._currentParsingLineType = newState; }
        }

        public void WriteToCurrentElement(string currentLine)
        {
            switch (this._currentParsingLineType)
            {
                case GherkinLineType.FeatureHeader:
                    WriteToFeaturePreamble(currentLine);
                    break;
                case GherkinLineType.ScenarioHeader:
                    StartNewScenario();
                    _currentScenario.WriteDescription(currentLine);
                    break;
                case GherkinLineType.Given:
                    _currentScenario.WriteGiven(currentLine);
                    break;
                case GherkinLineType.When:
                    _currentScenario.WriteWhen(currentLine);
                    break;
                case GherkinLineType.Then:
                    _currentScenario.WriteThen(currentLine);
                    break;
                case GherkinLineType.But:
                    _currentScenario.WriteAndOrBut(currentLine);
                    break;
                case GherkinLineType.And:
                    _currentScenario.WriteAndOrBut(currentLine);
                    break;
                default:
                    throw new InvalidOperationException("No element has been initialised on this Gherkin.");
            }
        }

        private void StartNewScenario()
        {
            if (_currentScenario == null) {_currentScenario = new GherkinScenario();}

            if (_currentScenario.IsComplete)
            {
                _currentScenario = new GherkinScenario();
            }
            else
            {
                _scenarios.Add(_currentScenario);
                _currentScenario.MarkAsComplete();
            }
        }

        private void WriteToFeaturePreamble(string currentLine)
        {
            if (currentLine.StartsWith(FeatureMarker))
            {
                Title = currentLine.TrimStart(FeatureMarker.ToCharArray()).Trim(' ');
            }
            else
            {
                AddToBlurb(currentLine);
            }
        }

        private void AddToBlurb(string currentLine)
        {
            // does nothing
        }
    }
};