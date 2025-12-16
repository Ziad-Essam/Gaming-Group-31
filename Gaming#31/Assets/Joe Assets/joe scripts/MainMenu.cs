using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Level2");
    }
 
    public void Quit()
    {
        SceneManager.LoadScene("Quit");
    }
}