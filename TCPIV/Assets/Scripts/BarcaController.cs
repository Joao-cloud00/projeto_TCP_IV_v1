using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarcaController : MonoBehaviour
{
    public float maxTiltAngle = 30.0f; // Ângulo máximo permitido
    public float shakeThreshold = 1.5f; // Sensibilidade para detectar balançadas
    public float tiltSpeedMultiplier = 5.0f; // Multiplicador de velocidade para balançadas

    public Slider _diversao_Slider;

    public int score = 5; // Pontuação do jogador
    private bool hasLost = false; // Verifica se o jogador perdeu

    private Vector3 lastAcceleration; // Última aceleração detectada

    void Start()
    {
        if (!SystemInfo.supportsAccelerometer)
        {
            Debug.Log("Acelerômetro não suportado neste dispositivo.");
        }

        // Define o valor inicial do slider para o volume atual do dispositivo
        _diversao_Slider.value = score;
    }

    void Update()
    {
        if (hasLost) return;

        Vector3 acceleration = Input.acceleration; // Obtém dados do acelerômetro

        // Calcula a inclinação do dispositivo em relação ao eixo X e Y
        // float tiltX = Mathf.Atan2(acceleration.x, acceleration.z) * Mathf.Rad2Deg;
        float tiltY = Mathf.Atan2(acceleration.y, acceleration.z) * Mathf.Rad2Deg;

        // Verifica se a inclinação ultrapassa o limite definido
        if (Mathf.Abs(tiltY) > maxTiltAngle)
        {
            LoseGame(); // Chama a função que lida com a perda da fase
        }

        // Detecta balançadas significativas
        if (Vector3.Distance(acceleration, lastAcceleration) > shakeThreshold)
        {
            AddScore(); // Aumenta a pontuação a cada balançada
        }

        if (score <= 0)
        {
            LoseGame();
        }

        lastAcceleration = acceleration; // Atualiza a última aceleração
    }

    void AddScore()
    {
        score++;
        Debug.Log("Pontuação: " + score);
    }

    void LoseGame()
    {
        hasLost = true;
        Debug.Log("Você perdeu a fase! Inclinação excedida.");
        // Aqui você pode adicionar a lógica para terminar o jogo, como recarregar a fase
    }

    private IEnumerator PerderPonto()
    {
        // Espera pelo tempo determinado
        yield return new WaitForSeconds(3);

        score--;
    }
}
