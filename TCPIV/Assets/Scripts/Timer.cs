using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text txtTime;
    [SerializeField] private float timeValue;
    private int timer;
    public DetectarAssoproSprites detectarAssoproSprites;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DiminuirTempo", 1f, 1f);
        detectarAssoproSprites = FindObjectOfType<DetectarAssoproSprites>();

    }

    private void Update()
    {
        if(timer >= 5)
        {
            timer = 0;
            detectarAssoproSprites.taxaDecremento += 1;
        }
    }

    private void DiminuirTempo()
    {
        timer += 1;
        if (timeValue <= 0f)
        {
            SceneManager.LoadScene(2);
           
        }

        if (timeValue > 0f)
        {
            timeValue--;
        }

        else
        {
            timeValue = 0f;
        }

        ExibirTempo(timeValue);
    }

    private void ExibirTempo(float timeToDisplay)
    {
        if (timeToDisplay < 0f)
        {
            timeToDisplay = 0f;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay/60);
        float seconds = Mathf.FloorToInt(timeToDisplay%60);

        txtTime.text = string.Format("{00:00}:{1:00}",minutes,seconds);
    }

    private void AumentarTempo()
    {
        if (timeValue < 0f) return;

        if (timeValue > 0f)
        {
            timeValue++;
        }

        ExibirTempo(timeValue);
    }
}
