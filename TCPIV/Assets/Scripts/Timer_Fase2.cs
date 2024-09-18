using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer_Fase2 : MonoBehaviour
{
    [SerializeField] private Text txtTime;
    [SerializeField] private float timeValue;
    private int timer;
    public DetectarAssoproSprites detectarAssoproSprites;
    public int indexScene;

    [SerializeField] private GameObject telaFimdeJogo;
    [SerializeField] private RotacaoDispositivo rotacaoDispositivo;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    void Start()
    {
        InvokeRepeating("DiminuirTempo", 1f, 1f);
        detectarAssoproSprites = FindObjectOfType<DetectarAssoproSprites>();

    }

    private void Update()
    {
        if (timer >= 5)
        {
            timer = 0;
            detectarAssoproSprites.taxaDecremento += 1;
        }
    }

    private void DiminuirTempo()
    {
        timer += 1;
        if (timeValue <= 0f && rotacaoDispositivo.posCorreta == true)
        {
            SceneManager.LoadScene(indexScene);
        }
        else if(timeValue <= 0f && rotacaoDispositivo.posCorreta == false)
        {
            Time.timeScale = 0f;
            telaFimdeJogo.SetActive(true);
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

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        txtTime.text = string.Format("{00:00}:{1:00}", minutes, seconds);
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
