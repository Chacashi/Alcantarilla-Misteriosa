using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class simonController : MonoBehaviour
{
    [SerializeField] Button[] arrayButtons;
    int numberRepeats;
    int numberButton;


    private void Start()
    {
        arrayButtons = new Button[4];
        StartCoroutine("SecuencieButtons");
    }
    IEnumerator SecuencieButtons()
    {
        
        numberRepeats = UnityEngine.Random.Range(1, 6);
        
        while (numberRepeats>0)
        {
            numberButton = UnityEngine.Random.Range(0, 3);
            arrayButtons[numberButton].GetComponent<Button>().image.color = Color.red;
            yield return new WaitForSeconds(2.0f);
            arrayButtons[numberButton].GetComponent<Button>().image.color = Color.white;
            numberRepeats--;
        }
    }




   
}
