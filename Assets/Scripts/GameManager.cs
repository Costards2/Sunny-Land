using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
    }

    public void Jogar()
    {
        SceneManager.LoadScene("Game");
    }

    public void Sair()
    {
        Debug.Log("Sair");
        Application.Quit();
    }
}
