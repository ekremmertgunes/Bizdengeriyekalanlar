using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
public void OnPlayButton(){
    SceneManager.LoadScene("ZombieLand");
}
public void OnQuitButton(){
    Debug.Log("Quitting game...");
    Application.Quit();
}
}