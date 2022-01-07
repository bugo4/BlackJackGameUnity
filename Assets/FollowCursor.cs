using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{

	private void Awake()
	{
	}
	// Start is called before the first frame update
	void Start()
    {
		if (PlayerPrefs.HasKey("Allow Particles"))
		{
			gameObject.SetActive(PlayerPrefs.GetInt("Allow Particles") != 0);
		}
    }

    // Update is called once per frame
    void Update()
    {
		if (Camera.current == null) { return; }
		Vector3 cursorPose = Camera.current.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
		this.transform.position = cursorPose;
    }

	public void EnableScript(bool allow)
	{
		PlayerPrefs.SetInt("Allow Particles", allow == true ? 1 : 0);
		this.enabled = allow;
	}
}
