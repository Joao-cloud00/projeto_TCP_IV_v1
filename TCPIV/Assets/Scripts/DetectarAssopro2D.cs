using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectarAssopro2D : MonoBehaviour
{
    public GameObject quadrado; // Referência para o quadrado 2D
    public Text textoTeste;
    private AudioClip clipeMicrofone;
    private bool microfoneInicializado;
    public float taxaDecremento = 1f; // Valor decrementado a cada segundo
    public float taxaCrescimento = 1f; // Valor incrementado a cada segundo
    public float intervaloDecremento = 1.0f; // Intervalo de decremento em segundos
    private float tempoDesdeUltimoDecremento = 0f;
    public float limiteAssoproForte = 0.2f; // Limite para detectar um assopro forte
    private float valorTotal = 50f; // Valor inicial
    private float valorMaximo = 100f;
    private bool jogoAtivo = true; // Estado do jogo

    void Start()
    {
        InicializarMicrofone();
        AtualizarCorQuadrado();
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
        if (!jogoAtivo) return; // Se o jogo não estiver ativo, não faz nada

        if (microfoneInicializado && Microphone.IsRecording(null))
        {
            float[] amostras = new float[128];
            int posicaoMicrofone = Microphone.GetPosition(null) - (128 + 1);
            if (posicaoMicrofone < 0) return;

            clipeMicrofone.GetData(amostras, posicaoMicrofone);
            float volume = ObterVolumeRMS(amostras);

            if (volume > limiteAssoproForte) // Detectar um assopro forte
            {
                IncrementarValor();
            }

            // Atualiza o temporizador e decrementa o valor se necessário
            tempoDesdeUltimoDecremento += Time.deltaTime;
            if (tempoDesdeUltimoDecremento >= intervaloDecremento)
            {
                DecrementarValor();
                tempoDesdeUltimoDecremento = 0f;
            }
        }

        textoTeste.text = valorTotal.ToString();

        if (Input.GetKey(KeyCode.Space))
        {
            SimularAssopro();
        }
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

    void IncrementarValor()
    {
        valorTotal += taxaCrescimento; // Ajuste o valor do incremento conforme necessário
        if (valorTotal > valorMaximo)
        {
            valorTotal = valorMaximo;
            PerderJogo();
        }
        AtualizarCorQuadrado();
    }

    void DecrementarValor()
    {
        valorTotal -= taxaDecremento; // Ajuste o valor do decremento conforme necessário
        if (valorTotal < 0f)
        {
            valorTotal = 0f;
            PerderJogo();
        }
        AtualizarCorQuadrado();
    }

    void SimularAssopro()
    {
        IncrementarValor();
    }

    void AtualizarCorQuadrado()
    {
        // Interpolação linear para mudar a cor do quadrado
        float t = valorTotal / valorMaximo;
        quadrado.GetComponent<Renderer>().material.color = Color.Lerp(Color.red, Color.green, t);
    }

    void PerderJogo()
    {
        jogoAtivo = false; // Desativa o jogo
        Debug.Log("Você perdeu o jogo!");
        // Aqui você pode adicionar código para exibir uma mensagem na tela ou realizar outras ações
    }
}


