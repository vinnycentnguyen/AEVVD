using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateScore : MonoBehaviour
{
    
    public TextMeshProUGUI scoreText;

    private int score;
    private string scoreDisplay;

    void Start()
    {
        TextMeshProUGUI scoreText = GetComponent<TextMeshProUGUI>();
        score = 0;
    }

    void Update()
    {  
        scoreDisplay = "SCORE: " + score;
        scoreText.SetText(scoreDisplay);
    }

    public void incrementScore(int score)
    {
        this.score += score;
    }
}
