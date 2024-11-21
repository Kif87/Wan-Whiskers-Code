using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float _timeToWaitBeforeExit;

    public GameObject GameOverMenu;

    public void OnPlayerDied()
    {
        Invoke(nameof(EndGame), _timeToWaitBeforeExit);
    }

    public void EndGame()
    {
        GameOverMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;

    }

}
