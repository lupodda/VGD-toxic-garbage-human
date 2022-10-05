using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public Text scoreText;

    public void Start() 
    {
        scoreText = GameObject.Find("GameInterface/Score").GetComponent<Text>();
    }

    public void SetScoreText(int punti)
    {
        scoreText.text = "Punti ambiente: " + punti.ToString();
    }
}
