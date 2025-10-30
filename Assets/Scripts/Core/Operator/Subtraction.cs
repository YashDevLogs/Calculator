// Subtraction.cs
public class Subtraction : IOperator
{
    public char Symbol => '-';
    public int Precedence => 1;
    public double Evaluate(double a, double b) => a - b;
}
