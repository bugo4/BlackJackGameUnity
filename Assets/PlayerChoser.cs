using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerChoser : NetworkBehaviour
{
	public Behaviour[] ownedComponents;

	private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
		{
			foreach(var ownedComponent in ownedComponents)
			{
				ownedComponent.enabled = false;
			}
		}
		else
		{
			mainCamera = Camera.main; 
			mainCamera.gameObject.SetActive(false);
		}
    }
	void OnDestroy()
	{
		if(mainCamera != null)
		{
			mainCamera.gameObject.SetActive(true);
		}
	}
}
