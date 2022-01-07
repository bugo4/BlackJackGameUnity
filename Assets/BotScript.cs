using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotScript : MonoBehaviour
{
	[SerializeField]
	private CardsStore cardsStore;
	[SerializeField]
	private TMPro.TextMeshProUGUI botScoreText;
	public AudioSource changeTurnAudio;

	private void OnEnable()
	{
		BlackJackPoints.SetBotScoreText(botScoreText);
	}
	public void PlayRound(int turns)
	{
		changeTurnAudio.Play();
		if (BlackJackPoints.GetPlayerTurns() < BlackJackRules.PlayerMinTurns) { return; }
		int chosenCardNumber = 0;
		for (int i = 0; i < turns; i++)
		{
			chosenCardNumber = GetCard();
			if (chosenCardNumber != CardsStore.CardNotFound)
			{
				BlackJackPoints.AddBotScore(chosenCardNumber);
			}
		}
		BlackJackPoints.EndRound();
	}

	public int GetCard()
	{
		return cardsStore.GetCard();
	}
}
