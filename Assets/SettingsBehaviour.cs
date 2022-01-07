using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsBehaviour : MonoBehaviour
{
	public Toggle particlesToggle;
	public Toggle randomColorsToggle;
	public Slider cardSizeSlider;
	public Slider playersSizeSlider;
    // Start is called before the first frame update
    void Start()
    {
		if (PlayerPrefs.HasKey("Allow Particles"))
		{
			particlesToggle.isOn = PlayerPrefs.GetInt("Allow Particles") != 0;
		}
		if (PlayerPrefs.HasKey("Card Size"))
		{
			cardSizeSlider.value = PlayerPrefs.GetInt("Card Size");
		}
		if (PlayerPrefs.HasKey("Players Size"))
		{
			int playersValue = PlayerPrefs.GetInt("Players Size");
			if (playersValue < 2 || playersValue > 4)
			{
				PlayerPrefs.SetInt("Players Size", 2);
				playersValue = 2;
			}
			playersSizeSlider.value = playersValue;
		}
		else
		{
			PlayerPrefs.SetInt("Players Size", 2);
			playersSizeSlider.value = 2;
		}
		if (PlayerPrefs.HasKey("Player Random Colors"))
		{
			randomColorsToggle.isOn = PlayerPrefs.GetInt("Player Random Colors") != 0; // A way to convert int to bool
		}
		else
		{
			PlayerPrefs.SetInt("Player Random Colors", 1);
			randomColorsToggle.isOn = true;
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void ChangeCardSize()
	{
		PlayerPrefs.SetInt("Card Size", (int)cardSizeSlider.value);
		Debug.Log(PlayerPrefs.GetInt("Card Size"));
	}
	
	public void ChangePlayersSize()
	{
		PlayerPrefs.SetInt("Players Size", (int)playersSizeSlider.value);
	}

	public void SetPlayerRandomColors(bool allow)
	{
		PlayerPrefs.SetInt("Player Random Colors", Convert.ToInt32(allow));
	}
}
