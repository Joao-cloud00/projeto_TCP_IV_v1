using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public Rigidbody2D rb;


    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
        else
        {
            Debug.Log("Girosc�pio n�o suportado neste dispositivo.");
        }

    }

    void Update()
    {
        if (SystemInfo.supportsGyroscope)
        {
            // Captura a rota��o do girosc�pio
            Vector3 gyroRotation = Input.gyro.rotationRateUnbiased;

            // Aplica a rota��o do girosc�pio ao movimento da bola
            Vector2 movement = new Vector2(gyroRotation.x, gyroRotation.y);
            rb.velocity = movement * moveSpeed;
        }
    }
}
