using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotacaoDispositivo : MonoBehaviour
{
    // Define os limites de rota��o
    public float limiteRotacaoMinima = 0f; // Limite m�nimo (graus)
    public float limiteRotacaoMaxima = 45f; // Limite m�ximo (graus)
    public float tempoParaConfirmarRotacao = 2f; // Tempo necess�rio para segurar a posi��o correta (segundos)
    public float margemRotacao = 5f; // Margem de erro permitida (graus)

    public GameObject imagemParaGirar; // Imagem que ser� girada na tela
    private Quaternion rotacaoOriginalImagem; // Rota��o original da imagem

    public Image imagemDeStatus; // Imagem que ser� trocada com base na rota��o
    public Sprite imagemOriginal; // Imagem a ser mostrada fora da posi��o correta
    public Sprite imagemCorreta; // Imagem a ser mostrada na posi��o correta

    private float rotacaoIdeal; // Valor m�dio entre o limite m�nimo e m�ximo
    private float tempoNaPosicaoCorreta; // Tempo que o jogador ficou na posi��o correta
    public float dificuldade = 2;

    private float rotacaoDispositivoZ; // Rota��o real do dispositivo

    [SerializeField] private GameObject telaFimdeJogo;
    public bool posCorreta = false;

    void Start()
    {
        // Calcula o valor m�dio entre o limite m�nimo e m�ximo
        rotacaoIdeal = (limiteRotacaoMinima + limiteRotacaoMaxima) / 2f;

        // Salva a rota��o original da imagem
        if (imagemParaGirar != null)
        {
            rotacaoOriginalImagem = imagemParaGirar.transform.rotation;
        }

        // Inicializa a rota��o real do dispositivo
        rotacaoDispositivoZ = 0f;

        // Verifica se o dispositivo suporta girosc�pio e ativa-o
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
        // Verifica se o dispositivo possui girosc�pio
        if (SystemInfo.supportsGyroscope)
        {
            // Obt�m a rota��o do dispositivo
            Quaternion rotacaoDispositivo = Input.gyro.attitude;
            Vector3 rotacaoEuler = rotacaoDispositivo.eulerAngles;

            // Ajusta os valores para trabalhar com �ngulos de -180 a 180
            rotacaoDispositivoZ = rotacaoEuler.z;
            if (rotacaoDispositivoZ > 180) rotacaoDispositivoZ -= 360;

            // Gira a imagem conforme a rota��o do dispositivo
            if (imagemParaGirar != null)
            {
                imagemParaGirar.transform.rotation = Quaternion.Euler(0, 0, rotacaoDispositivoZ);
            }

            // Verifica se a rota��o est� dentro dos limites estabelecidos
            if (rotacaoDispositivoZ < limiteRotacaoMinima + dificuldade)
            {
                PerderJogo("Rotacionou pouco");
            }
            else if (rotacaoDispositivoZ > limiteRotacaoMaxima + dificuldade)
            {
                PerderJogo("Rotacionou demais");
            }
            else if (rotacaoDispositivoZ >= rotacaoIdeal - margemRotacao && rotacaoDispositivoZ <= rotacaoIdeal + margemRotacao) // Verifica se est� dentro da margem
            {
                tempoNaPosicaoCorreta += Time.deltaTime;
                if (imagemDeStatus != null)
                {
                    imagemDeStatus.sprite = imagemCorreta; // Muda para a imagem correta
                    posCorreta = true;
                }
                // Verifica se o tempo na posi��o correta foi suficiente
                if (tempoNaPosicaoCorreta >= tempoParaConfirmarRotacao)
                {
                    Debug.Log("Rota��o correta confirmada!");

                    // Retorna a imagem para a rota��o original e zera a rota��o do dispositivo
                    if (imagemParaGirar != null)
                    {
                        imagemParaGirar.transform.rotation = rotacaoOriginalImagem;
                        
                    }
                    //if (imagemDeStatus != null)
                    //{
                    //    imagemDeStatus.sprite = imagemCorreta; // Muda para a imagem correta
                    //}

                    rotacaoDispositivoZ = 0f; // Zera a rota��o do jogador
                }
            }
            else
            {
                // Reseta o tempo na posi��o correta se o jogador sair da �rea
                tempoNaPosicaoCorreta = 0f;

                // Troca a imagem de volta para a original se sair da posi��o correta
                if (imagemDeStatus != null)
                {
                    imagemDeStatus.sprite = imagemOriginal;
                    posCorreta = false;
                }
            }
        }
    }

    // Fun��o que � chamada quando o jogador perde o jogo
    void PerderJogo(string motivo)
    {
        Debug.Log("Voc� perdeu! " + motivo);
        telaFimdeJogo.SetActive(true);
        // Implementar l�gica para quando o jogador perde o jogo
        // Por exemplo, carregar uma nova cena ou exibir uma mensagem na tela
    }
}
