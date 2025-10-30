// Addition.cs
public class Addition : IOperator
{
    public char Symbol => '+';
    public int Precedence => 1; // lower than * and /
    public double Evaluate(double a, double b) => a + b;
}