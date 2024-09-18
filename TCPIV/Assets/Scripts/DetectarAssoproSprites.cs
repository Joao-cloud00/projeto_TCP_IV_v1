using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectarAssoproSprites : MonoBehaviour
{
    public Image imagem; // Referência para a imagem que exibirá os Sprites
    public Text textoTeste;
    private AudioClip clipeMicrofone;
    private bool microfoneInicializado;
    public float taxaDecremento = 1f; // Valor decrementado a cada segundo
    public float taxaCrescimento = 1f; // Valor incrementado a cada segundo
    public float intervaloDecremento = 1.0f; // Intervalo de decremento em segundos
    private float tempoDesdeUltimoDecremento = 0f;
    public float limiteAssoproForte = 0.2f; // Limite para detectar um assopro forte
    public float valorTotal = 50f; // Valor inicial
    private float valorMaximo = 100f;
    private bool jogoAtivo = true; // Estado do jogo
    public Sprite spriteZero; // Sprite para valor 0
    public Sprite spriteUm; // Sprite para valor de 1 até 33
    public Sprite spriteDois; // Sprite para valor de 34 até 66
    public Sprite spriteTres; // Sprite para valor de 67 até 99
    public Sprite spriteCem; // Sprite para valor 100
    public GameObject timer;
    public GameObject castelo;

    [SerializeField] private GameObject telaFimdeJogo;

    void Start()
    {
        InicializarMicrofone();
        AtualizarSprite();
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
        AtualizarSprite();
    }

    void DecrementarValor()
    {
        valorTotal -= taxaDecremento; // Ajuste o valor do decremento conforme necessário
        if (valorTotal < 0f)
        {
            valorTotal = 0f;
            PerderJogo();
        }
        AtualizarSprite();
    }

    void SimularAssopro()
    {
        IncrementarValor();
    }

    void AtualizarSprite()
    {
        if (valorTotal < 1)
        {
            imagem.sprite = spriteZero;
        }
        else if (valorTotal > 0 && valorTotal <= 33)
        {
            imagem.sprite = spriteUm;
        }
        else if (valorTotal > 33 && valorTotal <= 66)
        {
            imagem.sprite = spriteDois;
        }
        else if (valorTotal > 66 && valorTotal < 99.9f)
        {
            imagem.sprite = spriteTres;
        }
        else if (valorTotal > 99.9f)
        {
            imagem.sprite = spriteCem;
        }
    }

    void PerderJogo()
    {
        jogoAtivo = false; // Desativa o jogo
        timer.SetActive(false);
        telaFimdeJogo.SetActive(true);
        // Aqui você pode adicionar código para exibir uma mensagem na tela ou realizar outras ações
    }
}
