namespace Nucumber.Parsing
{
    public enum GherkinLineType
    {
        Unknown = 0,
        None = 1,
        FeatureHeader = 2,
        ScenarioHeader = 3,
        Given = 4,
        When = 5,
        Then = 6,
        But = 7,
        And = 8
    }
}