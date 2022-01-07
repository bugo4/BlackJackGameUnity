using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class BlackJackPoints
{
	private static int playerScore = 0;
	private static int botScore = 0;
	private static int playerTurns = 0;
	private static TextMeshProUGUI playerScoreText;
	private static TextMeshProUGUI botScoreText;
	private static GameObject winPanel;
	public static class ReturnCodes
	{
		public static class PlayerOne
		{
			public const int ExceededMaxScore = 1;
			public const int Win = -1;
		}
		public static class PlayerTwo
		{
			public const int ExceededMaxScore = -1;
			public const int Win = 1;
		}
		public const int BothExceededMaxScore = 999;
		public const int BothScoresAreEqual = 0;
	}

	public static int GetPlayerScore()
	{
		return playerScore;
	}
	public static int GetBotScore()
	{
		return botScore;
	}
	public static int GetPlayerTurns()
	{
		return playerTurns;
	}
	public static void SetPlayerScoreText(TextMeshProUGUI newPlayerScoreText)
	{
		playerScoreText = newPlayerScoreText;
		Debug.LogWarning($"New Player Score Text: {playerScoreText.name}");
	}
	public static void SetBotScoreText(TextMeshProUGUI newBotScoreText)
	{
		botScoreText = newBotScoreText;
		Debug.LogWarning($"New Bot Score Text: {botScoreText.name}");
	}
	public static void SetWinPanel(GameObject newWinPanel)
	{
		winPanel = newWinPanel;
		Debug.LogError("Set Win Panel!");
	}
	public static void AddPlayerScore(int amount)
	{
		playerScore += amount;
	}
	public static void AddBotScore(int amount)
	{
		botScore += amount;
	}
	public static void AddPlayerTurn(int turns)
	{
		playerTurns += turns;
	}
	private static void AddBotWins(int winsAmount)
	{
		botScoreText.text = (int.Parse(botScoreText.text) + winsAmount).ToString();
		Debug.LogError("Bot Won!");
	}
	private static void AddPlayerWins(int winsAmount)
	{
		playerScoreText.text = (int.Parse(playerScoreText.text) + winsAmount).ToString();
		Debug.LogError("Player Won!");
	}

	public static void EndRound()
	{
		Debug.Log($"End Of Round: {playerScore} vs {botScore}");
		if (playerScore > BlackJackRules.ApproximateScore && botScore > BlackJackRules.ApproximateScore)
		{
			AddPlayerWins(1);
		}
		else if (playerScore > BlackJackRules.ApproximateScore)
		{
			AddBotWins(1);
		}
		else if (botScore > BlackJackRules.ApproximateScore)
		{
			AddPlayerWins(1);
		}
		else if (playerScore == botScore)
		{
			AddPlayerWins(1);
			AddBotWins(1);
		}
		else if (playerScore > botScore)
		{
			AddPlayerWins(1);
		}
		else
		{
			AddBotWins(1);
		}
		ResetScores();
		ResetPlayerTurns();
	}
	public static int EndRoundPlayers(int playerScore, int secondPlayerScore)
	{
		Debug.Log($"End Of Round: {playerScore} vs {secondPlayerScore}");
		// Checks if both players have got more than the max score.
		if (playerScore > BlackJackRules.ApproximateScore && secondPlayerScore > BlackJackRules.ApproximateScore)
		{
			return ReturnCodes.BothExceededMaxScore;
		}
		else if (playerScore > BlackJackRules.ApproximateScore) // Checks if the first player got more than the max score.
		{
			return ReturnCodes.PlayerTwo.Win;
		}
		else if (secondPlayerScore > BlackJackRules.ApproximateScore) // Checks if the first player got more than the max score.
		{
			return ReturnCodes.PlayerOne.Win;
		}
		// If the scores are less than / equal to the max score...
		else if (playerScore == secondPlayerScore)
		{
			return ReturnCodes.BothScoresAreEqual;
		}
		else if (playerScore > secondPlayerScore)
		{
			return ReturnCodes.PlayerOne.Win;
		}
		else
		{
			return ReturnCodes.PlayerTwo.Win;
		}
	}

	public static void ResetPlayerTurns()
	{
		playerTurns = 0;
	}
	public static void ResetScores()
	{
		playerScore = 0;
		botScore = 0;
	}
	public static void EndMatch()
	{
		ResetPlayerTurns();
		ResetScores();
		if (winPanel == null) { return; }
		winPanel.SetActive(true);
		Transform childOfWinPanel = winPanel.transform.GetChild(0);
		if (childOfWinPanel == null) { return; }
		var childText = childOfWinPanel.GetComponent<TextMeshProUGUI>();
		if (childText != null)
		{
			if (int.Parse(playerScoreText.text) < int.Parse(botScoreText.text))
			{
				childText.text = "Bot Won!";
			}
			else
			{
				childText.text = "Player Won!";
			}
		}
	}
	public static void EndMatchPlayers(int[] playerPoints)
	{
		if (winPanel == null) { return; }
		int maxPointsIndex = 0;
		List<string> maxPointsIndexes = new List<string>();
		for (int i = maxPointsIndex + 1; i < playerPoints.Length; i++)
		{
			maxPointsIndex = playerPoints[i] > playerPoints[maxPointsIndex] ? i : maxPointsIndex;
		}
		for (int i = 0; i < playerPoints.Length; i++)
		{
			if (playerPoints[i] == playerPoints[maxPointsIndex])
				maxPointsIndexes.Add(i.ToString());
		}
		winPanel.SetActive(true);
		Transform childOfWinPanel = winPanel.transform.GetChild(0);
		if (childOfWinPanel == null) { return; }
		var childText = childOfWinPanel.GetComponent<TextMeshProUGUI>();
		if (childText != null)
		{
			childText.text = maxPointsIndexes.Count == playerPoints.Length ? "All players won!" : string.Format("Player {0} Won!", string.Join(" and ", maxPointsIndexes));
		}
	}
}
