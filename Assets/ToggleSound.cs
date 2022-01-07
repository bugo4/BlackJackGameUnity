using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ToggleSound : MonoBehaviour
{
	public Sprite onSoundActiveSprite;
	public Sprite onSoundNotActiveSprite;
	public AudioSource backgroundAudio;
	// Start is called before the first frame update
	private bool hasSound = true;
	private Image soundImage;
	private void Awake()
	{
		soundImage = gameObject.GetComponent<Image>();
	}
	void Start()
    {
        if (PlayerPrefs.HasKey("Enable Sound"))
		{
			Debug.Log("Has Sound Key!" + PlayerPrefs.GetInt("Enable Sound"));
			hasSound = PlayerPrefs.GetInt("Enable Sound") != 0; // A way to convert int to bool
		}
		else
		{
			PlayerPrefs.SetInt("Enable Sound", 1);
			hasSound = true;
		}
		ChangeSound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void ChangeSound()
	{
		if (hasSound)
		{
			soundImage.sprite = onSoundActiveSprite;
			backgroundAudio.enabled = true;
		}
		else
		{
			soundImage.sprite = onSoundNotActiveSprite;
			backgroundAudio.enabled = false;
		}
		PlayerPrefs.SetInt("Enable Sound", Convert.ToInt32(hasSound));
		hasSound = !hasSound;
	}
}
