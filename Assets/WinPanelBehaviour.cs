using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanelBehaviour : MonoBehaviour
{
	void Awake()
	{
		BlackJackPoints.SetWinPanel(gameObject);
		gameObject.SetActive(false);
	}
}
