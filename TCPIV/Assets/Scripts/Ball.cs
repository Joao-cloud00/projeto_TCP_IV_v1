using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] private Text pontuacaoTexto;

    public float moveSpeed = 3.0f;
    public Rigidbody2D rb;
    private int pontuacao = 0;
    public bool _lixosColetados = false;

    private void Awake()
    {
        _lixosColetados = false;
    }

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

        AtualizarTextoPontuacao();

    }

    void Update()
    {
        if (SystemInfo.supportsGyroscope)
        {
            // Captura a rotação do giroscópio
            Vector3 gyroRotation = Input.gyro.rotationRateUnbiased;

            // Aqui fazemos o mapeamento correto dos eixos
            float moveX = gyroRotation.y; // Inclinar para frente/para trás move no eixo X (invertemos para corrigir a direção)
            float moveY = gyroRotation.x;  // Inclinar para os lados move no eixo Y

            // Aplicamos o movimento
            Vector2 movement = new Vector2(moveX, moveY);
            rb.velocity = movement * moveSpeed;
        }

        if(pontuacao >= 5)
        {
            _lixosColetados = true;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lixo"))
        {
            pontuacao += 1;

            AtualizarTextoPontuacao();

            Destroy(collision.gameObject);
        }
    }

    public void AtualizarTextoPontuacao()
    {
        // Atualiza o texto exibido com a pontuação atual
        pontuacaoTexto.text = "Lixo Coletado: " + pontuacao;
    }
}
