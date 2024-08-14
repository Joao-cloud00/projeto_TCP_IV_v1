using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectarAssopro : MonoBehaviour
{
    public Slider barraDeCarregamento; // Referência para a barra de carregamento
    private AudioClip clipeMicrofone;
    private bool microfoneInicializado;
    public float taxaDecremento = 1f; // Valor decrementado da barra a cada segundo
    public float taxaCrescimento = 0.1f; // Valor incrementado da barra a cada segundo
    public float intervaloDecremento = 1.0f; // Intervalo de decremento em segundos
    private float tempoDesdeUltimoDecremento = 0f;
    public float limiteAssoproForte = 0.2f; // Limite para detectar um assopro forte

    void Start()
    {
        InicializarMicrofone();
        barraDeCarregamento.value = 50; // Inicializar a barra de carregamento com um valor de 50
    }

    void InicializarMicrofone()
    {
        if (Microphone.devices.Length > 0)
        {
            clipeMicrofone = Microphone.Start(null, true, 10, 44100);
            microfoneInicializado = true;
        }
        else
        {
            Debug.LogWarning("Nenhum microfone detectado!");
            microfoneInicializado = false;
        }
    }

    void Update()
    {
        if (microfoneInicializado && Microphone.IsRecording(null))
        {
            float[] amostras = new float[128];
            int posicaoMicrofone = Microphone.GetPosition(null) - (128 + 1);
            if (posicaoMicrofone < 0) return;

            clipeMicrofone.GetData(amostras, posicaoMicrofone);
            float volume = ObterVolumeRMS(amostras);

            if (volume > limiteAssoproForte) // Detectar um assopro forte
            {
                IncrementarBarraDeCarregamento();
            }

            // Atualiza o temporizador e decrementa a barra se necessário
            tempoDesdeUltimoDecremento += Time.deltaTime;
            if (tempoDesdeUltimoDecremento >= intervaloDecremento)
            {
                DecrementarBarraDeCarregamento();
                tempoDesdeUltimoDecremento = 0f;
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            SimularAssopro();
        }

        float ObterVolumeRMS(float[] amostras)
        {
            float soma = 0f;
            for (int i = 0; i < amostras.Length; i++)
            {
                soma += amostras[i] * amostras[i];
            }
            return Mathf.Sqrt(soma / amostras.Length);
        }

        void IncrementarBarraDeCarregamento()
        {
            barraDeCarregamento.value += taxaCrescimento; // Ajuste o valor do incremento conforme necessário
            if (barraDeCarregamento.value >= barraDeCarregamento.maxValue)
            {
                barraDeCarregamento.value = barraDeCarregamento.maxValue;
                Debug.Log("Barra de carregamento completa!");
            }
        }

        void DecrementarBarraDeCarregamento()
        {
            barraDeCarregamento.value -= taxaDecremento; // Ajuste o valor do decremento conforme necessário
            if (barraDeCarregamento.value < 0f)
            {
                barraDeCarregamento.value = 0f;
            }
        }

        void SimularAssopro()
        {
            IncrementarBarraDeCarregamento();
        }
    }
}
