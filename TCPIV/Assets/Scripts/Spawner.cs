using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;  // Array de objetos que serão gerados
    public float spawnInterval = 2f;     // Intervalo entre cada spawn
    public float moveSpeed = 5f;         // Velocidade de movimento dos objetos

    private void Start()
    {
        // Inicia o método repetido de spawn
        InvokeRepeating("SpawnObject", 0f, spawnInterval);
    }

    private void SpawnObject()
    {
        // Escolhe aleatoriamente um objeto do array
        int randomIndex = Random.Range(0, objectsToSpawn.Length);
        GameObject selectedObject = objectsToSpawn[randomIndex];

        // Cria o objeto no ponto do spawner
        GameObject spawnedObject = Instantiate(selectedObject, transform.position, Quaternion.identity);

        // Adiciona o script de movimento ao objeto gerado
        MoveLeft moveScript = spawnedObject.AddComponent<MoveLeft>();
        moveScript.speed = moveSpeed;
    }
}

// Script de Movimento
public class MoveLeft : MonoBehaviour
{
    public float speed = 5f;
    private DetectarAssoproSprites detectarAssoproSprites;

    private void Start()
    {
        detectarAssoproSprites = FindObjectOfType<DetectarAssoproSprites>();
    }

    void Update()
    {
        // Move o objeto para a esquerda
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Castelo")
        {
            detectarAssoproSprites.taxaDecremento *= 2;
            Destroy(gameObject);
            Debug.Log("Gatinho");
        }
    }
}

