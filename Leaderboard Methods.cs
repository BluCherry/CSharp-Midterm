using System;
using System.IO;
using TMPro;
using UnityEngine;

public class LeaderboardMethods : MonoBehaviour
{
    public TextMeshProUGUI outputText;

    void Start()
    {
        string username = GetUsername.username;
        int score = QuestionOutput.score;
        string path = "scoreboard.csv";

        WriteRunToFile(username, score, path);

        LeaderBoard[] leaderboard = ReadRunsFromFile(path);

        LeaderBoard[] sortedBoard = Sort(leaderboard);

        string text = ($"-Top 5 Runs- \n");
        for (int i = 0; i < 5; i++)
        {
            if (sortedBoard.Length - 1 >= i)
            {
                if (sortedBoard[i] == null)
                {
                    text = text + "-\n";
                }
                else
                {
                    string line = ($"{sortedBoard[i].ToString()}\n");
                    text = text + line;
                }
            }
            else
            {
                text = text + "-\n";
            }
        }

        outputText.text = text;
    }

    public static void WriteRunToFile(string username, int score, string path)
    {
        
        using StreamWriter writer = new StreamWriter(path, true);
        string line = string.Join(',', username, score);
        writer.WriteLine(line);
        

        Debug.Log("Data exported to: " + path);
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

    public static LeaderBoard[] ReadRunsFromFile(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("Cannot read runs from missing File", path);
        }

        int lineCount = GetLineCount(path);
        LeaderBoard[] leaderboard = new LeaderBoard[lineCount];

        using StreamReader reader = new StreamReader(path);

        for (int i = 0; i < lineCount; i++)
        {
            string line = reader.ReadLine();

            string[] columns = line.Split(',');

            string username = columns[0];
            int score = int.Parse(columns[1]);

            leaderboard[i] = new LeaderBoard(username, score);
        }

        return leaderboard;
    }

    public static LeaderBoard[] Sort(LeaderBoard[] leaderboard)
    {
        for (int i = 0; i < leaderboard.Length; i++)
        {
            int maxIndex = i;
            for (int j = i + 1; j < leaderboard.Length; j++)
            {
                if (leaderboard[maxIndex].Score < leaderboard[j].Score)
                {
                    maxIndex = j;
                }
            }
            if (maxIndex != i)
            {
                LeaderBoard temporary = leaderboard[maxIndex];
                leaderboard[maxIndex] = leaderboard[i];
                leaderboard[i] = temporary;
            }
        }

        return leaderboard;
    }
}

