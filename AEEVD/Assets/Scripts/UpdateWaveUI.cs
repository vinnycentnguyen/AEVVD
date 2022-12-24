using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateWaveUI : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    public GameObject waveSpawner;
    private int waveNum;
    private bool display; 
    private float newAlpha;
    private float fadeTimer;
    private string waveNumDisplay;

    void Start()
    {  
        fadeTimer = 0.03f;
        newAlpha = 1;
        display = true;
        TextMeshProUGUI waveText = GetComponent<TextMeshProUGUI>();
        waveSpawner = GameObject.FindGameObjectWithTag("WaveSpawner");
        waveNum = 1;
    }
    void Update()
    {
        if(waveNum != waveSpawner.GetComponent<WaveSpawner>().CurrentWaveNum)
        {
            display = true;
            newAlpha = 1;
            waveNum++;
            waveNumDisplay = "WAVE " + waveNum;
            waveText.SetText(waveNumDisplay);
        }
    }

    void FixedUpdate()
    {
        if(display)
        {
            fadeTimer -= Time.deltaTime;
            if(fadeTimer <= 0)
            {
                fadeTimer = 0.08f;
                newAlpha -= 0.025f;
                if(newAlpha > 0)
                {
                    ChangeAlpha();
                }
                else
                {
                    display = false;
                }
            }
        }
    }

    public void ChangeAlpha()
    {
        Color newColor = waveText.color;
        newColor.a = newAlpha;
        waveText.color = newColor;
    }
}
