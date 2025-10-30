using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CalculatorUI : MonoBehaviour
{
    [SerializeField] private TMP_Text displayText;

    private CalculatorEngine calculator;
    private string expression = "";
    private bool isNewEntry = false;
    private bool justEvaluated = false; // To make sure even after result is displayed, you can perform further actions on result

    void Awake()
    {
        calculator = new CalculatorEngine(new IOperator[]
        {
            new Addition(),
            new Subtraction(),
            new Multiplication(),
            new Division()
        });
    }

    //  Handles number and operator clicks
    public void OnButtonClick(string input)
    {
        // if we just evaluated and user presses an operator → continue chain
        if (justEvaluated && "+-*/".Contains(input))
        {
            expression += input; // append operator to existing result
            justEvaluated = false;
        }
        // if we just evaluated and user presses a number → start fresh
        else if (justEvaluated && char.IsDigit(input[0]))
        {
            expression = input;
            justEvaluated = false;
        }
        else
        {
            if (isNewEntry)
            {
                expression = "";
                isNewEntry = false;
            }

            expression += input;
        }

        displayText.text = expression;
    }

    //  Equals button
    public void OnEquals()
    {
        try
        {
            double result = calculator.Evaluate(expression);
            displayText.text = result.ToString();
            expression = result.ToString();

            isNewEntry = false;
            justEvaluated = true; // 🔹 marks that we’re in “post-result” mode
        }
        catch
        {
            displayText.text = "Error";
            expression = "";
            justEvaluated = false;
        }
    }

    //  Handles "C" (Clear All)
    public void OnClear()
    {
        expression = "";
        displayText.text = "0";
    }

    // Handles "CE" (Clear Entry)
    public void OnClearEntry()
    {
        if (string.IsNullOrEmpty(expression))
        {
            displayText.text = "0";
            return;
        }

        int lastOp = Mathf.Max(expression.LastIndexOf('+'),
                               expression.LastIndexOf('-'),
                               expression.LastIndexOf('*'),
                               expression.LastIndexOf('/'));

        if (lastOp == -1)
            expression = "";
        else
            expression = expression.Substring(0, lastOp + 1);

        displayText.text = expression.Length > 0 ? expression : "0";
    }

    //  Handles "⌫" (Backspace)
    public void OnBackspace()
    {
        if (expression.Length > 0)
        {
            expression = expression.Substring(0, expression.Length - 1);
            displayText.text = expression.Length > 0 ? expression : "0";
        }
    }

    //  Handles "." (Decimal Point)
    public void OnDecimal()
    {
        if (expression.Length == 0 || "+-*/".Contains(expression[^1].ToString()))
            expression += "0."; // start a new decimal number
        else
            expression += ".";

        displayText.text = expression;
    }

    //  Handles "+/-" (Toggle Sign)
    public void OnToggleSign()
    {
        if (string.IsNullOrEmpty(expression)) return;

        // find the last operator to isolate the current number
        int lastOp = Mathf.Max(expression.LastIndexOf('+'),
                               expression.LastIndexOf('-'),
                               expression.LastIndexOf('*'),
                               expression.LastIndexOf('/'));

        string currentNumber = lastOp == -1 ? expression : expression.Substring(lastOp + 1);
        string before = lastOp == -1 ? "" : expression.Substring(0, lastOp + 1);

        if (currentNumber.StartsWith("-"))
            currentNumber = currentNumber.Substring(1);
        else
            currentNumber = "-" + currentNumber;

        expression = before + currentNumber;
        displayText.text = expression;
    }
}
