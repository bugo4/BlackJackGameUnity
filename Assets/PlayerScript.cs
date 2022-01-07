using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	[SerializeField]
	private Vector3 cameraOffset = Vector3.up;
	[SerializeField]
	private TMPro.TextMeshProUGUI playerScoreText;
	public GameObject otherPlayer;

	private Camera mainCamera;
	private void OnEnable()
	{
		mainCamera = Camera.main;
		BotScript otherBotScript = otherPlayer.GetComponent<BotScript>();
		if(otherBotScript != null)
		{
			otherBotScript.enabled = true;
		}
		foreach(Transform child in transform)
		{
			foreach(Transform childOfChild in child)
			{
				childOfChild.GetComponent<Renderer>().enabled = false;
			}
		}
		BlackJackPoints.SetPlayerScoreText(playerScoreText);
	}
	// Start is called before the first frame update
	void Start()
    {
		mainCamera.transform.position = transform.position + cameraOffset;
		mainCamera.transform.LookAt(otherPlayer.transform);
    }
}
