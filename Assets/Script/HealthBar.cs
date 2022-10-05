using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Healthbar serve per gestire lo slider della barra di salute
 * si può gestire il massimo della salute e la salute corrente
 */
public class HealthBar : MonoBehaviour
{
	public Slider slider;

	public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;
	}

	public void SetHealth(int health) { 
	
		
		slider.value = health;

	}

}