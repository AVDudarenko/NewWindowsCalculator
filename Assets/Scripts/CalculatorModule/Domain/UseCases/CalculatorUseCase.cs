using System.Text.RegularExpressions;

namespace Domain.UseCases
{
    public class CalculatorUseCase
    {
        public bool ValidateExpression(string expression)
        {
            return Regex.IsMatch(expression, @"^[0-9]+\+[0-9]+$");
        }
       
        public int Calculate(string expression)
        {
            // Делим строку по знаку '+'
            var parts = expression.Split('+');
            int a = int.Parse(parts[0]);
            int b = int.Parse(parts[1]);
            return a + b;
        }
    }
}