using System;

namespace Nucumber.Parsing
{
    [Flags]
    public enum GherkinLineType
    {
        Unknown = 1,
        None = 2,
        FeatureHeader = 4,
        ScenarioHeader = 8,
        Given = 16,
        When = 32,
        Then = 64,
        But = 128,
        And = 256
    }
}