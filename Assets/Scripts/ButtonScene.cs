using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScene : MonoBehaviour
{
    Button myButton;
    [SerializeField] string scene;

    private void Awake()
    {
        myButton = GetComponent<Button>(); 
    }

    private void Start()
    {
        myButton.onClick.AddListener(ChangueScene);
    }

    public void ChangueScene()
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1.0f;
    }
}
