using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlesManagerController : MonoBehaviour
{
    [SerializeField] Slider sliderDoor;
    [SerializeField] int maxValueSlider;
    [SerializeField] int ObjectiveValue;
    [SerializeField] float addValue;
    public static event Action<bool> OnCompleteSliderDoor;



    private void Start()
    {
        sliderDoor.maxValue = maxValueSlider;
        sliderDoor.value = 0;
        
    }
    private void Update()
    {
        SubtractValue();
        CompleteObjective();
    }

    private void OnEnable()
    {
        playerController.OnCollisionDoor += ShowSlider;
        playerController.OnCollisionDoorExit += HideSlider;
        playerController.OnFillingSlider += AddValue;

    }

    private void OnDisable()
    {
        playerController.OnCollisionDoor -= ShowSlider;
        playerController.OnCollisionDoorExit -= HideSlider;
        playerController.OnFillingSlider -= AddValue;   
        
    }
    void AddValue()
    {
        sliderDoor.value += addValue;
    }

    void SubtractValue()
    {
        if(sliderDoor.value > sliderDoor.minValue)
        {
            sliderDoor.value -= Time.deltaTime;
        }
        
    }

   void ShowSlider()
    {
        sliderDoor.gameObject.SetActive(true);
    }

    void HideSlider()
    {
        sliderDoor.gameObject.SetActive(false);
    }

    void CompleteObjective()
    {
        if (ObjectiveValue <= sliderDoor.value)
        {
            OnCompleteSliderDoor?.Invoke(true);
        }
    }

}
