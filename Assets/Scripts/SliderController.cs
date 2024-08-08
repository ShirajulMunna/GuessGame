using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider[] sliders;
    public GameObject[] circles;
    public TimeManager timeManager;
    public GameManager gameManager;
    
    void Start()
    {
        gameManager.OnControllButtonPressed += UpdateSliders;
    }

    public void TimeSlider( float value) 
    {
        if (value == 1) 
        {
            circles[0].gameObject.SetActive(true);
            circles[1].gameObject.SetActive(false);
            circles[2].gameObject.SetActive(false);
            timeManager.SetTime(gameManager.time_1);
            PlayerPrefs.SetInt("Time", GameManager.Instance.time_1);

        }
        else if (value == 2)
        {
            circles[0].gameObject.SetActive(true);
            circles[1].gameObject.SetActive(true);
            circles[2].gameObject.SetActive(false);
            timeManager.SetTime(gameManager.time_2);


            PlayerPrefs.SetInt("Time", gameManager.time_2);


        }
       else if (value == 3)
       {
            circles[0].gameObject.SetActive(true);
            circles[1].gameObject.SetActive(true);
            circles[2].gameObject.SetActive(true);
            timeManager.SetTime(gameManager.time_3);


            PlayerPrefs.SetInt("Time", GameManager.Instance.time_3);


       }

    }

    public void GridSlider(float value)
    {
        if (value == 1)
        {
            circles[3].gameObject.SetActive(true);
            circles[4].gameObject.SetActive(false);
            circles[5].gameObject.SetActive(false);
            timeManager.SetTime(gameManager.time_1);
            PlayerPrefs.SetInt("Grid", GameManager.Instance.time_1);

        }
        if (value == 2)
        {
            circles[3].gameObject.SetActive(true);
            circles[4].gameObject.SetActive(true);
            circles[5].gameObject.SetActive(false);
            timeManager.SetTime(gameManager.time_2);
            PlayerPrefs.SetInt("Grid", gameManager.time_2);


        }
        if (value == 3)
        {
            circles[3].gameObject.SetActive(true);
            circles[4].gameObject.SetActive(true);
            circles[5].gameObject.SetActive(true);
            timeManager.SetTime(gameManager.time_3);
            PlayerPrefs.SetInt("Grid", GameManager.Instance.time_3);


        }

    }
    public void ParticleSlider(float value)
    {
        if (value == 1)
        {
            circles[0].gameObject.SetActive(true);
            circles[1].gameObject.SetActive(false);
            circles[2].gameObject.SetActive(false);
            timeManager.SetTime(gameManager.time_1);
            PlayerPrefs.SetInt("Time", GameManager.Instance.time_1);

        }
        if (value == 2)
        {
            circles[0].gameObject.SetActive(true);
            circles[1].gameObject.SetActive(true);
            circles[2].gameObject.SetActive(false);
            timeManager.SetTime(gameManager.time_2);


            PlayerPrefs.SetInt("Time", gameManager.time_2);


        }
     
    }

    public void UpdateSliders(object sender , EventArgs e) 
    {
        int timeValue= PlayerPrefs.GetInt("Time", 20);

        if (timeValue == 20)
        {
            sliders[0].value = 1;
        }
        else if (timeValue == 30)
        {
            sliders[0].value = 2;

        }
        else if (timeValue == 40) 
        {
            sliders[0].value = 3;
        
        }

    }




}
