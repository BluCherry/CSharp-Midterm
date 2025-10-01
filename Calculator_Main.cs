namespace Midterm_Project_Calculator;

class Calculator_Main
{
    public static void Main()
    {
        Console.Write($"Enter an Expression: ");
        string Expression = Console.ReadLine();
        try
        {
            double result = Tokenizer.Calculator.Evaluator(Expression);
            Console.WriteLine($"Result: {result}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        } 
    }
}