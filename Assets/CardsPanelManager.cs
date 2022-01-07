using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsPanelManager : MonoBehaviour
{
	public GameObject cardImageObject;
	public Vector3 startOffset = Vector3.left;
	public Vector3 cardsOffset = Vector3.right;
	private Vector3 currentAddedCardPosition;
	private Transform parentOfCards;

	private void Awake()
	{
		parentOfCards = transform.GetChild(0);
		currentAddedCardPosition = parentOfCards.position + startOffset;
	}
	public void AddCard(Sprite cardSprite)
	{
		GameObject newCard = Instantiate(cardImageObject, currentAddedCardPosition, parentOfCards.rotation, parentOfCards);
		Image cardImage = newCard.GetComponent<Image>();
		if (cardImage != null)
		{
			cardImage.sprite = cardSprite;
		}
		currentAddedCardPosition += cardsOffset;
	} 
}
