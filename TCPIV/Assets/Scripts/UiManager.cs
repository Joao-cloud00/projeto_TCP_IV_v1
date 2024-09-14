using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
    public Slider volumeSlider;
    public Text texto; // Refer�ncia ao objeto de texto na UI
    public float tempoExibicao = 3f; // Dura��o em segundos para o texto ficar vis�vel
    public DetectarAssoproSprites detectarAssoproSprites;

    void Start()
    {
        detectarAssoproSprites = FindObjectOfType<DetectarAssoproSprites>();
        // Inicialmente esconde o texto
        texto.gameObject.SetActive(false);

        // Chama a fun��o para exibir o texto
        StartCoroutine(ExibirTextoTemporario());

        // Define o valor inicial do slider para o volume atual do dispositivo
        volumeSlider.value = detectarAssoproSprites.valorTotal; 
    }

    void Update()
    {
        // Atualiza o valor do slider para refletir o volume atual do dispositivo
        volumeSlider.value = detectarAssoproSprites.valorTotal;
    }
    private IEnumerator ExibirTextoTemporario()
    {
        // Exibe o texto
        texto.gameObject.SetActive(true);

        // Espera pelo tempo determinado
        yield return new WaitForSeconds(tempoExibicao);

        // Esconde o texto ap�s o tempo
        texto.gameObject.SetActive(false);
    }
}








