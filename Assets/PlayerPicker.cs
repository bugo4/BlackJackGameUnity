using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerPicker : NetworkBehaviour
{
	private List<GameObject> gameObjectChilds = new List<GameObject>();

	[SyncVar]
	private int chosenPlayerSkin;
	private void Awake()
	{
		foreach(Transform child in transform)
		{
			if (child.gameObject.tag == "Player")
			{
				gameObjectChilds.Add(child.gameObject);
			}
		}
		chosenPlayerSkin = (int) Random.Range(0, gameObjectChilds.Count);
	}
	// Start is called before the first frame update
	void Start()
    {
		AssignUniqueId();
		gameObjectChilds[chosenPlayerSkin].SetActive(true);
		//CmdSetCharacterSkin((int)Random.Range(0, gameObjectChilds.Count));
    }

	public override void OnStartClient()
	{
		base.OnStartClient();

		string netID = GetComponent<NetworkIdentity>().netId.ToString();
		GameManager.RegisterPlayer("Player " + netId, gameObject);
	}


	private void OnDestroy()
	{
		GameManager.UnregisterPlayer(transform.name);
	}
	private void AssignUniqueId()
	{
		transform.name = "Player " + GetComponent<NetworkIdentity>().netId;
	}
	[Command]
	private void CmdSetCharacterSkin(int skinIndex)
	{
		gameObjectChilds[skinIndex].SetActive(true);
	}
}
