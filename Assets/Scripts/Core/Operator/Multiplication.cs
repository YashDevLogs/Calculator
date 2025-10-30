// Multiplication.cs
public class Multiplication : IOperator
{
    public char Symbol => '*';
    public int Precedence => 2;
    public double Evaluate(double a, double b) => a * b;
}
