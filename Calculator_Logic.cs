using System.Linq.Expressions;
using System.Text;

namespace Midterm_Project_Calculator;

//Discerns what token types can be identified, and returned in my program
public enum TokenType
{
    Number,
    Operation,
    RightParenthesis,
    LeftParenthesis,
}

//Used to get tokens from an expression input
public class Token
{
    public TokenType Type { get; } // the type of the token (Number, operator, RightParenthesis)
    public string Value { get; } //The actual value of the token (3, +, ))

    public Token(TokenType type, string value)
    {
        Type = type;
        Value = value;
    }
    public override string ToString() => $"{Type}: {Value}";
}

//The tokenizer class loops through the expression like an array, parses what's important and returns until it can't
public class Tokenizer
{
    private readonly string _Expression;

    public Tokenizer(string Expression)
    {
        _Expression = Expression;
    }

    public List<Token> Tokenize()
    {
        var tokens = new List<Token>(); //stores the final results from the foreach loop, and the if statements
        var number = new StringBuilder(); //makes multi-digit numbers i.e 12 or 13

        foreach (char character in _Expression)
        {
            if (char.IsWhiteSpace(character)) { continue; } //bypasses spaces
            
            if (char.IsDigit(character) || character == '.') //finds numbers and decimals
            {
                number.Append(character);
                continue; 
            }
            
            //Tokenizes numbers
            if (number.Length > 0)
            {
                tokens.Add(new Token(TokenType.Number, number.ToString()));
                number.Clear();
            }
            
            //Tokenizes operations, and parenthesis
            if ("+-*/^".Contains(character))
            {
                tokens.Add(new Token(TokenType.Operation, character.ToString()));
            }
            else if (character == '(')
            {
                tokens.Add(new Token(TokenType.LeftParenthesis, character.ToString()));
            }
            else if (character == ')')
            {
                tokens.Add(new Token(TokenType.RightParenthesis, character.ToString()));
            }
            else
            {
                throw new FormatException($"Invalid character '{character}'.");
            }
        }
          //Code to ensure that any remaining numbers are added  
          if (number.Length > 0)
          {
              tokens.Add(new Token(TokenType.Number, number.ToString()));
          }
          return tokens;
    }

    public class Calculator
    {
        //Determines the priority status of each operator
        private static int priority(string operation)
        {
            if (operation == "+" || operation == "-")
            {
                return 3;
            } else if (operation == "*" || operation == "/")
            {
                return 2;
            } else if (operation == "^")
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        
        //Evaluates each function, and returns a value
        public static double Evaluator(string expression)
        {
            var Tokenizer = new Tokenizer(expression);
            var tokens = Tokenizer.Tokenize();
            var PostFix = InFixToPostFix(tokens);
            return EvaluatePostFix(PostFix);
        }
        
        //Reorders the tokens to make expression math easier for a machine (RPN)
        private static List<Token> InFixToPostFix(List<Token> tokens)
        {
            var output = new List<Token>();
            var operators = new Stack<Token>();

            foreach (var token in tokens)
            {
                switch (token.Type)
                {
                    //If the token is a number, add it to the output list
                    case TokenType.Number:
                        output.Add(token);
                        break;
                    
                    //If the token is an operation, add to output list 
                    case TokenType.Operation:
                        while (operators.Count > 0 &&
                               operators.Peek().Type == TokenType.Operation && 
                               (priority(operators.Peek().Value) < priority(token.Value) || 
                                   (priority(operators.Peek().Value) == priority(token.Value) && token.Value != "^")))
                        {
                            output.Add(operators.Pop());
                        }
                        operators.Push(token);
                        break;
                    
                    //If the token is the left parenthesis
                    case TokenType.LeftParenthesis:
                        operators.Push(token);
                        break;
                    //If the token is a right parenthesis
                    case TokenType.RightParenthesis:
                        while (operators.Count > 0 && operators.Peek().Type != TokenType.LeftParenthesis)
                        {
                            output.Add(operators.Pop());
                        }
                        if (operators.Count == 0)
                        {
                            throw new Exception("Unidentified parenthesis");
                        }
                        operators.Pop(); //Removes the misplaced parenthesis
                        break;
                }
            }
            
            //When the operators stack has a higher count than zero, this checks is there are any parenthesis unaccounted for
            while (operators.Count > 0)
            {
                if (operators.Peek().Type == TokenType.RightParenthesis ||
                    operators.Peek().Type == TokenType.LeftParenthesis)
                {
                    throw new Exception("Unidentified parenthesis");
                }
                output.Add(operators.Pop());
            }
            return output;
        }
        
        private static double EvaluatePostFix(List<Token> postfixTokens)
        {
            var stack = new Stack<double>();

            foreach (var token in postfixTokens)
            {
                if (token.Type == TokenType.Number)
                {
                    stack.Push(double.Parse(token.Value));
                }
                else if (token.Type == TokenType.Operation)
                {
                    double right = stack.Pop();
                    double left = stack.Pop();
                    double result;
                    
                    //Checks which operator the token is, and performs the math to complete it
                    switch (token.Value)
                    {
                        case("^"):
                            result = Math.Pow(left, right);
                            break;
                        case("+"):
                            result = left + right;
                            break;
                        case("-"):
                            result = left - right;
                            break;
                        case("*"):
                            result = left * right;
                            break;
                        case("/"):
                            if (right == 0)
                            {
                                throw new Exception("Cannot divide by 0");
                            }
                            result = left / right;
                            break;
                        default:
                            throw new Exception("unsupported operation");
                    }
                    stack.Push(result);
                }
            }

            if (stack.Count == 0)
            {
                throw new Exception("unidentified");
            }
            return stack.Pop();
        }
    }
}