using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakeDetector : MonoBehaviour
{
    public float shakeThreshold = 2.0f; // Limiar de agita��o
    public float shakeDetectionTime = 0.2f; // Tempo m�ximo entre as agita��es
    private float lastShakeTime;
    private Vector3 initialAcceleration;
    public Text shakeText; // Refer�ncia ao objeto de texto na cena

    void Start()
    {
        initialAcceleration = Input.acceleration;
        lastShakeTime = Time.time;
    }

    void Update()
    {
        // Calcula a diferen�a entre a acelera��o atual e a acelera��o inicial
        Vector3 deltaAcceleration = Input.acceleration - initialAcceleration;

        // Calcula a magnitude da diferen�a de acelera��o
        float accelerationMagnitude = deltaAcceleration.magnitude;

        // Verifica se a magnitude excede o limiar de agita��o
        if (accelerationMagnitude >= shakeThreshold)
        {
            // Verifica se o tempo entre as agita��es � suficiente
            if (Time.time - lastShakeTime >= shakeDetectionTime)
            {
                // Agita��o detectada!
                shakeText.text = "Shake detected!"; // Atualiza o texto na tela

                // Inicia a corrotina para fazer o texto desaparecer ap�s alguns segundos
                StartCoroutine(HideShakeText());

                // Atualiza o tempo da �ltima agita��o
                lastShakeTime = Time.time;
            }
        }
    }

    IEnumerator HideShakeText()
    {
        // Espera por 2 segundos
        yield return new WaitForSeconds(2f);

        // Limpa o texto
        shakeText.text = "";
    }
}

