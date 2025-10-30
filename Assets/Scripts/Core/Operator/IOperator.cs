// IOperator.cs
public interface IOperator
{
    char Symbol { get; }                 // The operator character
    double Evaluate(double a, double b); // Logic for evaluation
    int Precedence { get; }              // Helps with DMAS priority
}
