using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static Dictionary<string, GameObject> playersDict = new Dictionary<string, GameObject>();
	
	public static void RegisterPlayer(string playerID, GameObject playerObject)
	{
		playersDict.Add(playerID, playerObject);
		Debug.Log(playersDict);
	}

	public static void UnregisterPlayer(string playerID)
	{
		if (playersDict.ContainsKey(playerID))
		{
			playersDict.Remove(playerID);
		}
		else
		{
			Debug.LogError("GameManager: " + playerID + "was not found in dictionary!");
		}
	}

	public static GameObject GetPlayer(string playerID)
	{
		return playersDict[playerID];
	}

	private void OnGUI()
	{
		GUILayout.BeginArea(new Rect(200, 200, 200, 500));
		GUILayout.BeginVertical();

		foreach(var key in playersDict.Keys)
		{
			GUILayout.Label(key + " - " + playersDict[key]);
		}

		GUILayout.EndVertical();
		GUILayout.EndArea();
	}

}
