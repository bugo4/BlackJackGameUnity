using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePlayer : MonoBehaviour
{
	public GameObject chosenPlayer;
	public Transform particleArrow;
	public Vector3 particleOffset = Vector3.up;
	public string chosenTag = "Player";
	public GameObject[] chosenObjects;
	public GameObject[] afterChosenObjects;
	public BoxCollider cardsStoreCollider;

	private Camera mainCamera;
	private void Awake()
	{
		mainCamera = Camera.main;
	}
	private void Start()
	{
		EnableAfterChosenObjects(false);
		EnableChosenObjects(true);
	}

	// Update is called once per frame
	void Update()
    {
		Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			if (!hit.transform.CompareTag(chosenTag)) { return; }
			if (Input.GetMouseButtonDown(0))
			{
				PlayerScript chosenPlayerScript = hit.collider.gameObject.GetComponent<PlayerScript>();
				if(chosenPlayerScript != null)
				{
					chosenPlayerScript.enabled = true;
					particleArrow.gameObject.SetActive(false);
					EnableChosenObjects(false);
					EnableAfterChosenObjects(true);
					cardsStoreCollider.enabled = true;
					gameObject.SetActive(false);
				}
				Debug.Log("ChosenPlayer: " + chosenPlayer.transform.name);
			}
			else
			{
				particleArrow.position = hit.transform.position + particleOffset;
				Debug.Log("HoveredPlayer: " + hit.collider.gameObject.name);
			}
		}
    }
	void EnableChosenObjects(bool toEnable)
	{
		foreach (GameObject chosenObject in chosenObjects)
		{
			chosenObject.SetActive(toEnable);
		}
	}
	void EnableAfterChosenObjects(bool toEnable)
	{
		foreach (GameObject chosenAfterObject in afterChosenObjects)
		{
			chosenAfterObject.SetActive(toEnable);
		}
	}
}
