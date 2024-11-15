using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManagerController : MonoBehaviour
{
    [SerializeField] GameObject panelWin;
    [SerializeField] GameObject panelLose;




    private void OnEnable()
    {
        playerController.OnPlayerDeath += Lose;   
    }

    private void OnDisable()
    {
        playerController.OnPlayerDeath -= Lose;
    }
    void Win()
    {
        panelWin.SetActive(true);
        Time.timeScale = 0.0f;
    }

    void Lose()
    {
        panelLose.SetActive(true);
        Time.timeScale = 0.0f;
    }
   
}
