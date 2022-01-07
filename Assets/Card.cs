using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
	#region Card Constants
	public const int MinCardNumber = 1;
	public const int MaxCardNumber = 13;
	public enum CardShapes
	{
		Diamond,
		Sprade,
		Club,
		Heart
	}
	public const int MinCardShapeNumber = (int)CardShapes.Diamond;
	public const int MaxCardShapeNumber = (int)CardShapes.Heart;
	#endregion

	private int cardNumber;
	private int cardShape;

	public Card()
	{

	}
	public Card(int cardNumber)
	{
		this.cardNumber = cardNumber;
	}
	/*
	public Card(int cardShape)
	{
		this.cardShape = cardShape;
	}
	*/
	public Card(int cardNumber, int cardShape)
	{
		this.cardNumber = cardNumber;
		this.cardShape = cardShape;
	}

	public int GetCardNumber()
	{
		return cardNumber;
	}
	public void SetCardNumber(int newCardNumber)
	{
		this.cardNumber = newCardNumber;
	}

	public int GetCardShape()
	{
		return cardShape;
	}
	public void SetCardShape(int newCardShape)
	{
		this.cardShape = newCardShape;
	}
}
