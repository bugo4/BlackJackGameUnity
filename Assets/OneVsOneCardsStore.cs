using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(BoxCollider))]
public class OneVsOneCardsStore : MonoBehaviour
{
	public const int CardNotFound = -1;

	public Sprite[] diamondCards;
	public Sprite[] spradeCards;
	public Sprite[] clubCards;
	public Sprite[] heartCards;
	public GameObject cardObject;
	public Vector3 cardsOffset = Vector3.up / 5f;
	public GameObject cardPanel;
	public int cardsSize = 30;
	public int players = 2;
	private GameObject[] playerCameras;
	public TextMeshProUGUI[] playerPointsText;
	public TextMeshProUGUI playerTurnText;
	public AudioSource pickCardSound;
	public AudioSource changeTurnSound;

	// Experimental
	public List<GameObject> playersObjects;

	private int turn = 0;
	private int playsInTurn = 0;
	private int[] playerScores;

	private List<Card> cards = new List<Card>();
	private List<GameObject> cardsObjects = new List<GameObject>();
	private int currentRemovedIndex = 0;
	private Sprite[][] organizedShapes;
	private GameObject tempCardObject;

	public static Card getRandomCard()
	{
		return new Card(Random.Range(Card.MinCardNumber, Card.MaxCardNumber + 1),
			Random.Range(Card.MinCardShapeNumber, Card.MaxCardShapeNumber));
	}
	public void SetCardsStore()
	{
		for(int i = 0; i < cardsSize; i++)
		{
			cards.Add(getRandomCard());
		}
	}
	private void Awake()
	{
		if (PlayerPrefs.HasKey("Card Size"))
		{
			cardsSize = PlayerPrefs.GetInt("Card Size");
			Debug.LogWarning(cardsSize);
		}
		else
		{
			PlayerPrefs.SetInt("Card Size", 30);
			Debug.LogWarning("Did not find Card Size!");
		}
		List<GameObject> newPlayersObject = new List<GameObject>();
		if (PlayerPrefs.HasKey("Players Size"))
		{
			int currentPlayersSize = PlayerPrefs.GetInt("Players Size");
			if (currentPlayersSize <= playersObjects.Count && currentPlayersSize > 1)
			{
				for (int i = 0; i < currentPlayersSize; i++)
				{
					newPlayersObject.Add(playersObjects[i]);
				} 
			}
			else
			{
				PlayerPrefs.SetInt("Players Size", 2);
				currentPlayersSize = 2;
				for (int i = 0; i < currentPlayersSize; i++)
				{
					newPlayersObject.Add(playersObjects[i]);
				} 
			}
		}
		else
		{
				PlayerPrefs.SetInt("Players Size", 2);
				int currentPlayersSize = 2;
				for (int i = 0; i < currentPlayersSize; i++)
				{
					newPlayersObject.Add(playersObjects[i]);
				} 
		}
		playersObjects = newPlayersObject;
		for (int i = 0; i < playersObjects.Count; i++)
		{
			playersObjects[i].SetActive(true);
			if (playersObjects.Count + i < playerPointsText.Length)
			{
				playerPointsText[playersObjects.Count + i].transform.parent.gameObject.SetActive(false);
			}
		}
		if (PlayerPrefs.HasKey("Player Random Colors"))
			RandomisePlayerColors(PlayerPrefs.GetInt("Player Random Colors") != 0);
		else
		{
			PlayerPrefs.SetInt("Player Random Colors", 1);
			RandomisePlayerColors(true);
		}

		organizedShapes = new Sprite[Card.MaxCardShapeNumber + 1][];
		organizedShapes[0] = diamondCards;
		organizedShapes[1] = spradeCards;
		organizedShapes[2] = clubCards;
		organizedShapes[3] = heartCards;
		playerScores = new int[playerCameras.Length];
	}

	private void Start()
	{
		Vector3 cardPosition = transform.position;
		SetCardsStore();
		for(int i = 0; i < cards.Count; i++)
		{
			tempCardObject = Instantiate(cardObject, cardPosition, transform.rotation, transform);
			cardsObjects.Add(tempCardObject);
			Debug.Log("Card Shape: " + cards[i].GetCardShape() + ", Card Number: " + cards[i].GetCardNumber());
			tempCardObject.GetComponent<CardManager>().SetNewImage(
				organizedShapes[cards[i].GetCardShape()][cards[i].GetCardNumber() - 1]);
			cardPosition += cardsOffset;
		}
		playerCameras[turn].SetActive(true);
	}
	private void RandomisePlayerColors(bool toRandom)
	{
		playerCameras = new GameObject[playersObjects.Count];
		for(int i = 0; i < playersObjects.Count; i++)
		{
			if (playersObjects[i] != null)
			{
				var playerMesh = playersObjects[i].transform.Find("playerMesh");
				playerCameras[i] = playersObjects[i].transform.Find("Camera").gameObject;
				if (playerMesh == null || !toRandom) continue;
				for (int j = 0; j < playerMesh.childCount; j++)
				{
					playerMesh.GetChild(j).GetComponent<MeshRenderer>().material.color = UnityEngine.Random.ColorHSV();
				}
			}
		}
	}
	public void OnMouseDown()
	{
		int chosenCardNumber = GetCard();
		if (chosenCardNumber != CardNotFound)
		{
			playerScores[turn] += chosenCardNumber;
			playsInTurn++;
			//BlackJackPoints.AddPlayerScore(chosenCardNumber);
			//BlackJackPoints.AddPlayerTurn(1);
		}
	}

	public void EndTurn()
	{
		int maxScore = 0;
		int currentReturnCode = 0;
		if (!CardsAvailable() /*&& turn + 1 != playerCameras.Length*/) { EndMatch(); }
		if (playsInTurn < BlackJackRules.PlayerMinTurns) { return; }
		if (turn + 1 == playerCameras.Length)
		{
			for (int i = 1; i < playerScores.Length; i++)
			{
				currentReturnCode = BlackJackPoints.EndRoundPlayers(playerScores[i], playerScores[maxScore]);
				if (currentReturnCode == BlackJackPoints.ReturnCodes.BothExceededMaxScore || currentReturnCode == BlackJackPoints.ReturnCodes.BothScoresAreEqual)
				{
					ResetScores();
					ChangePlayer();
					return;
				}
				maxScore = currentReturnCode < 0 ? i : maxScore;
			}
			playerPointsText[maxScore].text = (int.Parse(playerPointsText[maxScore].text) + 1).ToString();
			ResetScores();
		}
		changeTurnSound.Play();
		ChangePlayer();
		Debug.LogWarning(playerPointsText[maxScore].text);
	}
	private void ResetScores()
	{
		for (int i = 0; i < playerScores.Length; i++)
		{
			playerScores[i] = 0;
		}

	}
	private void ChangePlayer()
	{
		playerCameras[turn].SetActive(false);
		turn = (turn + 1) % playerScores.Length;
		playerCameras[turn].SetActive(true);
		playerTurnText.text = string.Format("Player {0} Is Playing!", turn);
		playsInTurn = 0;
		if (!CardsAvailable()) { EndMatch(); }
	}
	public int GetCard()
	{
		if(CardsAvailable())
		{
			CardsPanelManager cardsPanelManager = cardPanel.GetComponent<CardsPanelManager>();
			if (cardsPanelManager != null)
			{
				cardsPanelManager.AddCard(organizedShapes[
					cards[cardsObjects.Count - 1 - currentRemovedIndex].GetCardShape()]
					[cards[cardsObjects.Count - 1 - currentRemovedIndex].GetCardNumber() - 1]);
			}
			Destroy(cardsObjects[cardsObjects.Count - 1 - currentRemovedIndex]);
			currentRemovedIndex++;
			Debug.Log(cardsObjects.Count + " current: " + currentRemovedIndex);
			Debug.LogWarning($"Card Number: {cards[cardsObjects.Count - currentRemovedIndex].GetCardNumber() + 1}");
			pickCardSound.Play();
			return cards[cardsObjects.Count - currentRemovedIndex].GetCardNumber() + 1;
		}
		EndMatch();
		return CardNotFound;
	}

	private bool CardsAvailable()
	{
		return currentRemovedIndex < cardsObjects.Count && cardsObjects[cardsObjects.Count - 1 - currentRemovedIndex] != null;
	}

	public void EndMatch()
	{
		int[] points = new int[playerScores.Length];
		for (int i = 0; i < points.Length; i++)
		{
			points[i] = int.Parse(playerPointsText[i].text);
		}
		BlackJackPoints.EndMatchPlayers(points);
	}
}
