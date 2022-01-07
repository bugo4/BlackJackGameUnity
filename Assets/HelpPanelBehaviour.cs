using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPanelBehaviour : MonoBehaviour
{
	public GameObject[] nonHelpObjects;
	public void ShowHelp()
	{
		ShowChilds(true);
	}
	public void DisableHelp()
	{
		ShowChilds(false);
		ShowNonHelpObjects(true);
	}
	public void DisableNonHelpObjects()
	{
		ShowNonHelpObjects(false);
	}
	public void ShowNonHelpObjects(bool isVisible)
	{
		foreach(GameObject nonHelpObject in nonHelpObjects)
		{
			nonHelpObject.SetActive(isVisible);
		}
	}
	public void ShowChilds(bool isVisible)
	{
		foreach(Transform child in transform)
		{
			child.gameObject.SetActive(isVisible);
		}
	}

	private void Update()
	{
		if (Input.anyKey)
		{
			ShowChilds(false);
			ShowNonHelpObjects(true);
			gameObject.SetActive(false);
		}
	}

}
