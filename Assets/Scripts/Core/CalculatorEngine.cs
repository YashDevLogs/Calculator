// CalculatorEngine.cs
using System.Collections.Generic;
using System.Linq;
using System;

public class CalculatorEngine
{
    private readonly Dictionary<char, IOperator> _operators;

    public CalculatorEngine(IEnumerable<IOperator> operators)
    {
        _operators = operators.ToDictionary(o => o.Symbol, o => o);
    }

    public double Evaluate(string expression)
    {
        List<double> numbers = new List<double>();
        List<IOperator> ops = new List<IOperator>();
        string current = "";

        foreach (char c in expression)
        {
            if (_operators.ContainsKey(c))
            {
                numbers.Add(double.Parse(current));
                current = "";
                IOperator op = _operators[c];

                // Handle precedence (DMAS)
                while (ops.Count > 0 && ops.Last().Precedence >= op.Precedence)
                {
                    ApplyLastOperator(numbers, ops);
                }

                ops.Add(op);
            }
            else
            {
                current += c;
            }
        }

        numbers.Add(double.Parse(current));

        while (ops.Count > 0)
        {
            ApplyLastOperator(numbers, ops);
        }

        return numbers[0];
    }

    private void ApplyLastOperator(List<double> numbers, List<IOperator> ops)
    {
        double b = numbers.Last(); numbers.RemoveAt(numbers.Count - 1);
        double a = numbers.Last(); numbers.RemoveAt(numbers.Count - 1);

        IOperator op = ops.Last(); ops.RemoveAt(ops.Count - 1);
        numbers.Add(op.Evaluate(a, b));
    }
}
