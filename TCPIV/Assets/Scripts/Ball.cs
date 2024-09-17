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
            Debug.Log("Giroscópio não suportado neste dispositivo.");
        }

    }

    void Update()
    {
        if (SystemInfo.supportsGyroscope)
        {
            // Captura a rotação do giroscópio
            Vector3 gyroRotation = Input.gyro.rotationRateUnbiased;

            // Aplica a rotação do giroscópio ao movimento da bola
            Vector2 movement = new Vector2(gyroRotation.x, gyroRotation.y);
            rb.velocity = movement * moveSpeed;
        }
    }
}
