using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayForest()
    {
        SceneManager.LoadScene("Forest");
    }

    public void PlayCastle()
    {
        SceneManager.LoadScene("Castle");
    }

    public void PlayRealm()
    {
        SceneManager.LoadScene("Realm");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
