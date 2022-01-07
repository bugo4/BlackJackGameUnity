using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BlinkBehaviour : MonoBehaviour
{
	public float blinkRate = 1f;
	public float blinkFreeze = 1f;
	private const float ALPHA_RATIO = 255f;
	private float currentOpacity = 255f;
	private Color startColor;
	private bool toBlend = true;
	private Animator parentAnimator;

	private void Awake()
	{
		startColor = GetComponent<TextMeshProUGUI>().color;
		parentAnimator = transform.parent.GetComponent<Animator>();
	}
	void Update()
    {
		StartCoroutine(Blink());
		if (parentAnimator is null) { return; }
		// if any key pressed AND no animation is playing...
		if (Input.anyKey && parentAnimator.GetCurrentAnimatorStateInfo(0).length < parentAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime)
		{
			SceneManager.LoadScene(0);
		}
    }
	IEnumerator Blink()
	{
		GetComponent<TextMeshProUGUI>().color = new Color(startColor.r, startColor.g, startColor.b, currentOpacity / ALPHA_RATIO);
		if (toBlend)
		{
			if (currentOpacity > 0)
			{
				currentOpacity -= blinkRate * Time.deltaTime;
			}
			else
			{
				yield return new WaitForSeconds(blinkFreeze);
				toBlend = false;
			}
		}
		else
		{
			if (currentOpacity < ALPHA_RATIO)
			{
				currentOpacity += blinkRate * Time.deltaTime;
			}
			else
			{
				yield return new WaitForSeconds(blinkFreeze);
				toBlend = true;
			}
		}
	}
}
