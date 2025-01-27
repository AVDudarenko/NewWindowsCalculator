/*
* Represents the state of the calculator, including error state and the last entered expression.
*/
public class CalculatorState
{
    public bool IsErrorState { get; set; }
    public string LastExpression { get; set; }
}