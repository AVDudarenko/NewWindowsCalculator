public class CalculationRecord
{
    public string Expression { get; }
    public string Result { get; }

    public CalculationRecord(string expression, string result)
    {
        Expression = expression;
        Result = result;
    }
}