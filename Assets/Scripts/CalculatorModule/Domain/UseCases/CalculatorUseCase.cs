using System.Text.RegularExpressions;

namespace Domain.UseCases
{
    /*
    * Represents the use case for performing basic calculator operations, such as validating and calculating expressions.
    */
    public class CalculatorUseCase
    {
        public bool ValidateExpression(string expression)
        {
            return Regex.IsMatch(expression, @"^[0-9]+\+[0-9]+$");
        }
       
        public int Calculate(string expression)
        {
            // Split the expression by the '+' sign
            var parts = expression.Split('+');
            int a = int.Parse(parts[0]);
            int b = int.Parse(parts[1]);
            return a + b;
        }
    }
}