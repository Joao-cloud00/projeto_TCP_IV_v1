using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakeDetector : MonoBehaviour
{
    public float shakeThreshold = 2.0f; // Limiar de agitação
    public float shakeDetectionTime = 0.2f; // Tempo máximo entre as agitações
    private float lastShakeTime;
    private Vector3 initialAcceleration;
    public Text shakeText; // Referência ao objeto de texto na cena

    void Start()
    {
        initialAcceleration = Input.acceleration;
        lastShakeTime = Time.time;
    }

    void Update()
    {
        // Calcula a diferença entre a aceleração atual e a aceleração inicial
        Vector3 deltaAcceleration = Input.acceleration - initialAcceleration;

        // Calcula a magnitude da diferença de aceleração
        float accelerationMagnitude = deltaAcceleration.magnitude;

        // Verifica se a magnitude excede o limiar de agitação
        if (accelerationMagnitude >= shakeThreshold)
        {
            // Verifica se o tempo entre as agitações é suficiente
            if (Time.time - lastShakeTime >= shakeDetectionTime)
            {
                // Agitação detectada!
                shakeText.text = "Shake detected!"; // Atualiza o texto na tela

                // Inicia a corrotina para fazer o texto desaparecer após alguns segundos
                StartCoroutine(HideShakeText());

                // Atualiza o tempo da última agitação
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

