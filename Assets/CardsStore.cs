using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CardsStore : MonoBehaviour
{
	public const int CardNotFound = -1;

	public Sprite[] diamondCards;
	public Sprite[] spradeCards;
	public Sprite[] clubCards;
	public Sprite[] heartCards;
	public GameObject cardObject;
	public Vector3 cardsOffset = Vector3.up / 5f;
	public GameObject cardPanel;
	public AudioSource pickCardAudio;
	private int cardsSize = 30;


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
			Debug.LogWarning("Did not find card size");
		}

		organizedShapes = new Sprite[Card.MaxCardShapeNumber + 1][];
		organizedShapes[0] = diamondCards;
		organizedShapes[1] = spradeCards;
		organizedShapes[2] = clubCards;
		organizedShapes[3] = heartCards;
		GetComponent<BoxCollider>().enabled = false;
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
	}
	public void OnMouseDown()
	{
		int chosenCardNumber = GetCard();
		if (chosenCardNumber != CardNotFound)
		{
			BlackJackPoints.AddPlayerScore(chosenCardNumber);
			BlackJackPoints.AddPlayerTurn(1);
		}
	}

	public int GetCard()
	{
		if(currentRemovedIndex < cardsObjects.Count && cardsObjects[cardsObjects.Count - 1 - currentRemovedIndex] != null)
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
			pickCardAudio.Play();
			return cards[cardsObjects.Count - currentRemovedIndex].GetCardNumber() + 1;
		}
		BlackJackPoints.EndMatch();
		return CardNotFound;
	}
}
