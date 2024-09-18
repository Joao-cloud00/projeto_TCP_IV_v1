using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReiniciarBtn : MonoBehaviour
{
    [SerializeField] private int indexScene;

    public void ReiniciarGame()
    {
        SceneManager.LoadScene(indexScene);
    }
}
