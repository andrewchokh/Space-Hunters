using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public Text scoreDisplay;

    private void Update()
    {
        scoreDisplay.text = "Score: " + score.ToString();
    }

    public void AddScore(int prize)
    {
        score += prize;
    }	
}
