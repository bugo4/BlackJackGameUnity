using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	private const int PlayerVsPlayerSceneIndex = 4;
	private const int PlayerVsComputerSceneIndex = 2;

	public GameObject helpPanel;
	public void BattleAgainstPlayer()
	{
		SceneManager.LoadScene(PlayerVsPlayerSceneIndex);
	}
	public void BattleAgainstComputer()
	{
		SceneManager.LoadScene(PlayerVsComputerSceneIndex);
	}
	public void ShowHelp()
	{
		helpPanel.SetActive(true);
	}
}
