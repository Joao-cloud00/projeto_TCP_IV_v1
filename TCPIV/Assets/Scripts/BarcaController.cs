using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarcaController : MonoBehaviour
{
    public float maxTiltAngle = 30.0f; // �ngulo m�ximo permitido
    public float shakeThreshold = 1.5f; // Sensibilidade para detectar balan�adas
    public float tiltSpeedMultiplier = 5.0f; // Multiplicador de velocidade para balan�adas

    public Slider _diversao_Slider;

    public int score = 5; // Pontua��o do jogador
    private bool hasLost = false; // Verifica se o jogador perdeu

    private Vector3 lastAcceleration; // �ltima acelera��o detectada

    void Start()
    {
        if (!SystemInfo.supportsAccelerometer)
        {
            Debug.Log("Aceler�metro n�o suportado neste dispositivo.");
        }

        // Define o valor inicial do slider para o volume atual do dispositivo
        _diversao_Slider.value = score;
    }

    void Update()
    {
        if (hasLost) return;

        Vector3 acceleration = Input.acceleration; // Obt�m dados do aceler�metro

        // Calcula a inclina��o do dispositivo em rela��o ao eixo X e Y
        // float tiltX = Mathf.Atan2(acceleration.x, acceleration.z) * Mathf.Rad2Deg;
        float tiltY = Mathf.Atan2(acceleration.y, acceleration.z) * Mathf.Rad2Deg;

        // Verifica se a inclina��o ultrapassa o limite definido
        if (Mathf.Abs(tiltY) > maxTiltAngle)
        {
            LoseGame(); // Chama a fun��o que lida com a perda da fase
        }

        // Detecta balan�adas significativas
        if (Vector3.Distance(acceleration, lastAcceleration) > shakeThreshold)
        {
            AddScore(); // Aumenta a pontua��o a cada balan�ada
        }

        if (score <= 0)
        {
            LoseGame();
        }

        lastAcceleration = acceleration; // Atualiza a �ltima acelera��o
    }

    void AddScore()
    {
        score++;
        Debug.Log("Pontua��o: " + score);
    }

    void LoseGame()
    {
        hasLost = true;
        Debug.Log("Voc� perdeu a fase! Inclina��o excedida.");
        // Aqui voc� pode adicionar a l�gica para terminar o jogo, como recarregar a fase
    }

    private IEnumerator PerderPonto()
    {
        // Espera pelo tempo determinado
        yield return new WaitForSeconds(3);

        score--;
    }
}
