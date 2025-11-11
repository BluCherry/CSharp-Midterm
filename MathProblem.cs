lt ﻿public enum Difficulty
{
    Easy,
    Medium,
    Hard,
    VeryHard,
    Impossible
}

public class MathProblem
{
    public string Question { get; }
    public int Answer { get; }

    public MathProblem(string question, int answer)
    {
        Question = question;
        Answer = answer;
    }

    public override string ToString()
    {
        return Question;
    }
    
    public static MathProblem EasyProblems()
    {
        MathProblem[] easyProblem = new MathProblem[]
        {
            new MathProblem("2 + 2", 4),
            new MathProblem("5 + 2", 7),
            new MathProblem("3 + 2", 5),
            new MathProblem("7 - 2", 5),
            new MathProblem("5 - 4", 1),
            new MathProblem("2 - 1", 1)
        };
        Random easyRandom = new Random();
        int index = easyRandom.Next(easyProblem.Length);
        return easyProblem[index];
    }
    
    public static MathProblem MediumProblems()
    {
        MathProblem[] mediumProblem = new MathProblem[]
        {
            new MathProblem("28 + 17", 45),
            new MathProblem("30 + 20", 50),
            new MathProblem("47 - 17", 30),
            new MathProblem("27 - 18", 9),
            new MathProblem("6 x 2", 12),
            new MathProblem("3 x 3", 9),
            new MathProblem("0 / 5", 0),
            new MathProblem("3 / 3", 1)
        };
        Random mediumRandom = new Random();
        int index = mediumRandom.Next(mediumProblem.Length);
        return mediumProblem[index];
    }

    public static MathProblem HardProblems()
    {
        MathProblem[] hardProblem = new MathProblem[]
        {
            new MathProblem("28 + 72", 100),
            new MathProblem("90 + 18", 108),
            new MathProblem("74 + 52", 126),
            new MathProblem("120 - 60", 60),
            new MathProblem("82 - 75", 7),
            new MathProblem("167 - 88", 79),
            new MathProblem("12 x 12", 144),
            new MathProblem("13 x 3", 39),
            new MathProblem("11 x 4", 44),
            new MathProblem("44 / 11", 4),
            new MathProblem("72 / 9", 8),
            new MathProblem("39 / 3", 13),
            new MathProblem("(1 + 2) x 3", 9),
            new MathProblem("(15 - 5) x 2", 20),
            new MathProblem("2 x (7 - 2)", 10),
        };
        Random hardRandom = new Random();
        int index = hardRandom.Next(hardProblem.Length);
        return hardProblem[index];
    }

    public static MathProblem VeryHardProblems()
    {
        MathProblem[] VeryHardProblem = new MathProblem[]
        {
            new MathProblem("(50 - 25) / 5^2", 1),
            new MathProblem("(5 + 5)^2 - 28", 72),
            new MathProblem("2 x (90 - 87)^2", 18),
            new MathProblem("3^3", 27),
            new MathProblem("(7 x 7) / 7^2", 1),
            new MathProblem("2^2 x (4 + 7)", 44),
            new MathProblem("5^3", 125),
            new MathProblem("(12 - 6)^3 - 4", 212),
            new MathProblem("(63 / 7)^2 - 1", 80)
        };
        Random veryHardRandom = new Random();
        int index = veryHardRandom.Next(VeryHardProblem.Length);
        return VeryHardProblem[index];
    }

    public static MathProblem ImpossibleProblems()
    {
        MathProblem[] ImpossibleProblem = new MathProblem[]
        {
            new MathProblem("(99 x 2)^3 - 7762391", 1),
            new MathProblem("(5^5 / 5) - 25^2", 0),
            new MathProblem("(-60 x -54)^2 / 200", 52488),
            new MathProblem("2", 2),
            new MathProblem("(15 * 80)^3 + 20 - 90 (67 - 43)^2", -486),
            new MathProblem("6^6", 46656),
        };
        Random impossibleRandom = new Random();
        int index = impossibleRandom.Next(ImpossibleProblem.Length);
        return ImpossibleProblem[index];
    }

    public static MathProblem GetRandomExpression(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                return EasyProblems();
            case Difficulty.Medium:
                return MediumProblems();
            case Difficulty.Hard:
                return HardProblems();
            case Difficulty.VeryHard:
                return VeryHardProblems();
            case Difficulty.Impossible:
                return ImpossibleProblems();
            default:
                return EasyProblems();
        }
    }
    
    public static void Main()
    { 
        Difficulty difficulty = Difficulty.Easy;
        int correctCount = 0;
        int impossibleCount = 0;
        Boolean gameComplete = false;

        while (!gameComplete)
        {
            MathProblem problem = GetRandomExpression(difficulty);
            Console.WriteLine($"[{difficulty} {problem}]");
            Console.Write("Enter your answer here: ");
            string userAnswer = Console.ReadLine();

            try
            {
                int userNumber =  int.Parse(userAnswer);
                if (userNumber == problem.Answer)
                {
                    Console.WriteLine($"Correct!\n");
                    correctCount++;
                    
                    if (difficulty == Difficulty.Impossible)
                    {
                        impossibleCount++;
                        if (impossibleCount >= 3)
                        {
                            Console.WriteLine("You completed 3 Impossible problems! You won!");
                            gameComplete = true;
                            continue; // Exit the loop
                        }
                    }
                    
                    if (correctCount >= 3 && difficulty != Difficulty.Impossible) 
                    { 
                        correctCount = 0; 
                        difficulty = NextDifficulty(difficulty);

                        if (difficulty == Difficulty.Impossible)
                        {
                            if (correctCount == 3)
                            {
                                Console.WriteLine("You won!");
                                break;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Incorrect.\n");
                }
            }
            catch (FormatException)
            {
                throw new Exception("Invalid input! Make sure to enter a number!");
            }
            catch (OverflowException)
            {
                throw new Exception("Insufficient answer.\n");
            }


        }
    }
    
    public static Difficulty NextDifficulty(Difficulty current)
    {
        switch (current)
        {
            case Difficulty.Easy:
                return Difficulty.Medium;
            case Difficulty.Medium:
                return Difficulty.Hard;
            case Difficulty.Hard:
                return Difficulty.VeryHard;
            case Difficulty.VeryHard:
                return Difficulty.Impossible;
            case Difficulty.Impossible:
                default:
                return Difficulty.Impossible;
        }
    }
        
}

/* Early Working Example
 * MathProblem easyProblems = EasyProblems();
   Console.WriteLine(easyProblems.ToString());
   
   Console.Write("Enter your answer here: ");
   string userinput = Console.ReadLine();

   try
   {
       int userNumber = int.Parse(userinput);
       if (userNumber == easyProblems.Answer)
       {
           Console.WriteLine("Correct!");
       }
       else
       {
           Console.WriteLine("Incorrect!");
       }
   }
   
   catch (FormatException)
   {
       throw new Exception("Invalid input, please enter a valid integer!");
   }
   catch (OverflowException)
   {
       throw new Exception("The number entered is too big, or small for an integer!");
   }
 */

