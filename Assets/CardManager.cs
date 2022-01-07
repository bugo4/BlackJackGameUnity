using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
	public SpriteRenderer image;

	public void SetNewImage(Sprite newSprite)
	{
		image.sprite = newSprite;
	}
}
