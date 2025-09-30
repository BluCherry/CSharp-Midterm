public static class MainClass
{
    private static void WriteRunToFile(string username, int score, string path)
    {
        if (!File.Exists(path))
        {
            using StreamWriter writer = new StreamWriter(path, true);
            string line = string.Join(',', username, score);
            writer.WriteLine(line);
        }
        else
        {
            using StreamWriter writer = new StreamWriter(path, true);
            string line = string.Join(',', username, score);
            writer.WriteLine(line);
        }
    }

    private static int GetLineCount(string path)
    {
        if (!File.Exists(path))
        {
        throw new FileNotFoundException("Cannot get line count of a missing file ", path);
        }

        int count = 0;
        using StreamReader reader = new StreamReader(path);

        while (!reader.EndOfStream)
        {
            reader.ReadLine();
            count++;
        }

        return count;
    }

    private static LeaderBoard[] ReadRunsFromFile(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("Cannot read runs from missing File", path);
        }

        int lineCount = GetLineCount(path);
        LeaderBoard[] leaderboard = new LeaderBoard[lineCount - 1];

        using StreamReader reader = new StreamReader(path);
        reader.ReadLine();

        for (int i = 0; i < lineCount - 1; i++)
        {
            string line = reader.ReadLine();

            string[] columns = line.Split(',');

            string username = columns[0];
            int score = int.Parse(columns[1]);

            leaderboard[i] = new LeaderBoard(username, score);
            }

            return leaderboard;
        }

    private static LeaderBoard[] Sort(LeaderBoard[] leaderboard)
    {
        for (int i = 0; i < leaderboard.Length - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < leaderboard.Length; j++)
            {
                if (leaderboard[minIndex].Score > leaderboard[j].Score)
                {
                    minIndex = j;
                }
            }
            if (minIndex != i)
            {
                int temporary = leaderboard[minIndex].Score;
                leaderboard[minIndex].Score = leaderboard[i].Score;
                leaderboard[i].Score = temporary;
            }
        }

        return leaderboard;
    }

    private static void Main()
    {
        string path = "scoreboard.csv";
        string username = "i";
        int score = 1;
        //these two would be collected at beginning of run; standin values here

        //at end of game
        WriteRunToFile(username, score, path);

        LeaderBoard[] leaderboard = ReadRunsFromFile(path);

        LeaderBoard[] sortedBoard = Sort(leaderboard);

        Console.WriteLine($"Top 5 Runs: \n");
        for (int i = 0; i < 5; i++)
        {
            if (sortedBoard[i] == null)
            {
                Console.WriteLine("-");
            }
            else
            {
                Console.WriteLine(sortedBoard[i]);
            }
        }
    }
}
