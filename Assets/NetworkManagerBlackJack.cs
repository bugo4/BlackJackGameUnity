using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkManagerBlackJack : NetworkManager
{
	public List<GameObject> spawnedObjects = new List<GameObject>();
	public override void OnServerAddPlayer(NetworkConnection conn)
	{
		base.OnServerAddPlayer(conn);
		/*
		Transform startPos = GetStartPosition();
		GameObject player = startPos != null ?
			Instantiate(spawnedObjects[(int)Random.Range(0, spawnedObjects.Count)], startPos.position, startPos.rotation)
			: Instantiate(spawnedObjects[(int)Random.Range(0, spawnedObjects.Count)]);
		NetworkServer.AddPlayerForConnection(conn, player);
		*/
	}
}
