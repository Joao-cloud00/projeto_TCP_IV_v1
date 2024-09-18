using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotacaoDispositivo : MonoBehaviour
{
    // Define os limites de rotação
    public float limiteRotacaoMinima = 0f; // Limite mínimo (graus)
    public float limiteRotacaoMaxima = 45f; // Limite máximo (graus)
    public float tempoParaConfirmarRotacao = 2f; // Tempo necessário para segurar a posição correta (segundos)
    public float margemRotacao = 5f; // Margem de erro permitida (graus)

    public GameObject imagemParaGirar; // Imagem que será girada na tela
    private Quaternion rotacaoOriginalImagem; // Rotação original da imagem

    public Image imagemDeStatus; // Imagem que será trocada com base na rotação
    public Sprite imagemOriginal; // Imagem a ser mostrada fora da posição correta
    public Sprite imagemCorreta; // Imagem a ser mostrada na posição correta

    private float rotacaoIdeal; // Valor médio entre o limite mínimo e máximo
    private float tempoNaPosicaoCorreta; // Tempo que o jogador ficou na posição correta
    public float dificuldade = 2;

    private float rotacaoDispositivoZ; // Rotação real do dispositivo

    [SerializeField] private GameObject telaFimdeJogo;
    public bool posCorreta = false;

    void Start()
    {
        // Calcula o valor médio entre o limite mínimo e máximo
        rotacaoIdeal = (limiteRotacaoMinima + limiteRotacaoMaxima) / 2f;

        // Salva a rotação original da imagem
        if (imagemParaGirar != null)
        {
            rotacaoOriginalImagem = imagemParaGirar.transform.rotation;
        }

        // Inicializa a rotação real do dispositivo
        rotacaoDispositivoZ = 0f;

        // Verifica se o dispositivo suporta giroscópio e ativa-o
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }

        // Inicializa a imagem de status com a imagem original
        if (imagemDeStatus != null)
        {
            imagemDeStatus.sprite = imagemOriginal;
            posCorreta = false;
        }
    }

    void Update()
    {
        // Verifica se o dispositivo possui giroscópio
        if (SystemInfo.supportsGyroscope)
        {
            // Obtém a rotação do dispositivo
            Quaternion rotacaoDispositivo = Input.gyro.attitude;
            Vector3 rotacaoEuler = rotacaoDispositivo.eulerAngles;

            // Ajusta os valores para trabalhar com ângulos de -180 a 180
            rotacaoDispositivoZ = rotacaoEuler.z;
            if (rotacaoDispositivoZ > 180) rotacaoDispositivoZ -= 360;

            // Gira a imagem conforme a rotação do dispositivo
            if (imagemParaGirar != null)
            {
                imagemParaGirar.transform.rotation = Quaternion.Euler(0, 0, rotacaoDispositivoZ);
            }

            // Verifica se a rotação está dentro dos limites estabelecidos
            if (rotacaoDispositivoZ < limiteRotacaoMinima + dificuldade)
            {
                PerderJogo("Rotacionou pouco");
            }
            else if (rotacaoDispositivoZ > limiteRotacaoMaxima + dificuldade)
            {
                PerderJogo("Rotacionou demais");
            }
            else if (rotacaoDispositivoZ >= rotacaoIdeal - margemRotacao && rotacaoDispositivoZ <= rotacaoIdeal + margemRotacao) // Verifica se está dentro da margem
            {
                tempoNaPosicaoCorreta += Time.deltaTime;
                if (imagemDeStatus != null)
                {
                    imagemDeStatus.sprite = imagemCorreta; // Muda para a imagem correta
                    posCorreta = true;
                }
                // Verifica se o tempo na posição correta foi suficiente
                if (tempoNaPosicaoCorreta >= tempoParaConfirmarRotacao)
                {
                    Debug.Log("Rotação correta confirmada!");

                    // Retorna a imagem para a rotação original e zera a rotação do dispositivo
                    if (imagemParaGirar != null)
                    {
                        imagemParaGirar.transform.rotation = rotacaoOriginalImagem;
                        
                    }
                    //if (imagemDeStatus != null)
                    //{
                    //    imagemDeStatus.sprite = imagemCorreta; // Muda para a imagem correta
                    //}

                    rotacaoDispositivoZ = 0f; // Zera a rotação do jogador
                }
            }
            else
            {
                // Reseta o tempo na posição correta se o jogador sair da área
                tempoNaPosicaoCorreta = 0f;

                // Troca a imagem de volta para a original se sair da posição correta
                if (imagemDeStatus != null)
                {
                    imagemDeStatus.sprite = imagemOriginal;
                    posCorreta = false;
                }
            }
        }
    }

    // Função que é chamada quando o jogador perde o jogo
    void PerderJogo(string motivo)
    {
        Debug.Log("Você perdeu! " + motivo);
        telaFimdeJogo.SetActive(true);
        // Implementar lógica para quando o jogador perde o jogo
        // Por exemplo, carregar uma nova cena ou exibir uma mensagem na tela
    }
}
