using BL.Interfaces;

namespace BL.Services
{
    public class CalculatorService: ICalculatorService
    {
        public CalculatorService() { }

        private static readonly Dictionary<string, Func<double, double, double>> operations = new Dictionary<string, Func<double, double, double>>
    {
        { "add", (a, b) => a + b },
        { "subtract", (a, b) => a - b },
        { "multiply", (a, b) => a * b },
        { "divide", (a, b) => b == 0 ? throw new ArgumentException("Cannot divide by zero.") : a / b }
    };

        public double Calculate(double num1, double num2, string operation)
        {
            if (operations.TryGetValue(operation.ToLower(), out var operationFunc))
            {
                return operationFunc(num1, num2);
            }
            throw new ArgumentException("Invalid operation.");//TODO
        }

    }
}
