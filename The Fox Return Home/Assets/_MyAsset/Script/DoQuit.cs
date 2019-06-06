using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoQuit : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Has Quit Game");
        Application.Quit();
    }
}
