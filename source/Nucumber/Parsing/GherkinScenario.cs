using System;
using System.Collections.Generic;

namespace Nucumber.Parsing
{
    public class GherkinScenario
    {
        private GherkinLineType _currentElement;
        private readonly List<string> _givens;
        private readonly List<string> _whens;
        private readonly List<string> _thens;

        public IEnumerable<string> Givens { get { return _givens; }}
        public IEnumerable<string> Whens { get { return _whens; }}
        public IEnumerable<string> Thens { get { return _thens; }}

        public bool IsComplete { get; private set; }
        public string Description { get; private set; }

        public GherkinScenario() 
        {
            _givens = new List<string>();
            _whens = new List<string>();
            _thens = new List<string>();
        }

        public void MarkAsComplete()
        {
            IsComplete = true;
        }
        
        public void WriteDescription(string newLine)
        {
            Description = newLine.TrimStart(Gherkin.ScenarioMarker.ToCharArray()).Trim(' ');
        }

        public void WriteGiven(string newLine)
        {
            _givens.Add(newLine);
            _currentElement = GherkinLineType.Given;
        }

        public void WriteWhen(string newLine)
        {
            _whens.Add(newLine);
            _currentElement = GherkinLineType.When;
        }

        public void WriteThen(string newLine)
        {
            _thens.Add(newLine);
            _currentElement = GherkinLineType.Then;
        }

        public void WriteAndOrBut(string newLine)
        {
            switch (_currentElement)
            {
                case GherkinLineType.Given:
                    WriteGiven(newLine);
                    break;
                case GherkinLineType.When:
                    WriteWhen(newLine);
                    break;
                case GherkinLineType.Then:
                    WriteThen(newLine);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}