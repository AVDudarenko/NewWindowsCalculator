/*
* Represents a record of a calculation, containing the expression and its result.
*/
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