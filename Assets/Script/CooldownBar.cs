using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour
{
	public Slider slider;

	public void SetMaxCooldown(int time)
	{
		slider.maxValue = time;
		slider.value = time;
	}

	public void SetCooldown(int time, int cooldown)
	{
		slider.value = cooldown - time;
	}
}
