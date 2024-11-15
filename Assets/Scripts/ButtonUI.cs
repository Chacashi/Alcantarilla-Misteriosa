using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour
{
    Button myButton;
    [SerializeField] GameObject[] arrayObjectives;
    [SerializeField] GameObject[] arrayIntertactues;

    private void Awake()
    {
        myButton = GetComponent<Button>();
    }

    private void Start()
    {
        myButton.onClick.AddListener(Interactue);
    }

    void Interactue()
    {
        for (int i = 0; i < arrayObjectives.Length; i++)
        {
            if (arrayObjectives[i].activeSelf)
            {

                arrayObjectives[i].SetActive(false);
                Time.timeScale = 1.0f;
            }
            else
            {
                arrayObjectives[i].SetActive(true);
                Time.timeScale = 0.0f;
            }
        }

        for (int i = 0; i < arrayIntertactues.Length; i++)
        {
            if (arrayIntertactues[i].GetComponent<Button>().interactable)
            {
                arrayIntertactues[i].GetComponent<Button>().interactable = false;
            }
            else
            {
                arrayIntertactues[i].GetComponent<Button>().interactable = true;
            }
        }
    }
}
