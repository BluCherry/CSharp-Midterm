public class LeaderBoard 
{
	private string username;
	private int score;

	public LeaderBoard(string username, int score) 
	{
		Username = username;
		Score = score;
    }

	public override string ToString() 
	{
		return $"{username}: {score}";
	}

	public string Username 
	{
		get => username;
		set 
		{
			if (string.IsNullOrWhiteSpace(value)) 
			{
				throw new ArgumentNullException(nameof(Username), "Username cannot be null or empty");
			}

			username = value;
		}
	}

	public int Score 
	{
		get => score;
		set 
		{
			if (value < 0) 
			{
				throw new ArgumentOutOfRangeException(nameof(Score), "Score must be a positive integer");
			}

			score = value;
		}
	}
}