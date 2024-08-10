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

            gameManager.grids[0].gameObject.SetActive(true);
            gameManager.grids[1].gameObject.SetActive(false);
            gameManager.grids[2].gameObject.SetActive(false);

            CardRotateManager.instance.SetGrid(4);


            PlayerPrefs.SetInt("Grid", 1);

        }
        if (value == 2)
        {
            circles[3].gameObject.SetActive(true);
            circles[4].gameObject.SetActive(true);
            circles[5].gameObject.SetActive(false);

            gameManager.grids[0].gameObject.SetActive(false);
            gameManager.grids[1].gameObject.SetActive(true);
            gameManager.grids[2].gameObject.SetActive(false);

            CardRotateManager.instance.SetGrid(6);


            PlayerPrefs.SetInt("Grid", 2);


        }
        if (value == 3)
        {
            circles[3].gameObject.SetActive(true);
            circles[4].gameObject.SetActive(true);
            circles[5].gameObject.SetActive(true);

            gameManager.grids[0].gameObject.SetActive(false);
            gameManager.grids[1].gameObject.SetActive(false);
            gameManager.grids[2].gameObject.SetActive(true);

            CardRotateManager.instance.SetGrid(12);


            PlayerPrefs.SetInt("Grid", 3);


        }

    }
    public void TargetSlider(float value)
    {
        if (value == 1)
        {
            circles[6].gameObject.SetActive(true);
            circles[7].gameObject.SetActive(false);
            circles[8].gameObject.SetActive(false);

            gameManager.grids[0].gameObject.SetActive(true);
            gameManager.grids[1].gameObject.SetActive(false);
            gameManager.grids[2].gameObject.SetActive(false);

            gameManager.Settarget(gameManager.target_1);


            PlayerPrefs.SetInt("Target", GameManager.Instance.target_1);

        }
        if (value == 2)
        {
            circles[6].gameObject.SetActive(true);
            circles[7].gameObject.SetActive(true);
            circles[8].gameObject.SetActive(false);

            gameManager.grids[0].gameObject.SetActive(false);
            gameManager.grids[1].gameObject.SetActive(true);
            gameManager.grids[2].gameObject.SetActive(false);

            gameManager.Settarget(gameManager.target_2);



            PlayerPrefs.SetInt("Target", GameManager.Instance.target_2);



        }
        if (value == 3)
        {
            circles[6].gameObject.SetActive(true);
            circles[7].gameObject.SetActive(true);
            circles[8].gameObject.SetActive(true);

            gameManager.grids[0].gameObject.SetActive(false);
            gameManager.grids[1].gameObject.SetActive(false);
            gameManager.grids[2].gameObject.SetActive(true);

            gameManager.Settarget(gameManager.target_3);

            PlayerPrefs.SetInt("Target", GameManager.Instance.target_3);



        }

    }
    public void ParticleSlider(float value)
    {
        if (value == 1)
        {
            circles[9].gameObject.SetActive(true);
            circles[10].gameObject.SetActive(false);
            

            gameManager.SetParticle(1);
            PlayerPrefs.SetInt("Particle", 1);

        }
        if (value == 2)
        {
            circles[9].gameObject.SetActive(true);
            circles[10].gameObject.SetActive(true);
          

            gameManager.SetParticle(2);
            PlayerPrefs.SetInt("Particle", 2);



        }
       

    }
    public void UpdateSliders(object sender , EventArgs e) 
    {
        int timeValue= PlayerPrefs.GetInt("Time", 20);
        int gridValue = PlayerPrefs.GetInt("Grid", 1);
        int targetValue = PlayerPrefs.GetInt("Target", 8);
        int particleValue = PlayerPrefs.GetInt("Particle", 1);

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

        if (gridValue == 1)
        {
            sliders[1].value = 1;
        }
        else if (gridValue == 2)
        {
            sliders[1].value = 2;

        }
        else if (gridValue == 3)
        {
            sliders[1].value = 3;

        }

        if (gridValue == 8)
        {
            sliders[2].value = 1;
        }
        else if (gridValue == 9)
        {
            sliders[2].value = 2;

        }
        else if (gridValue == 6)
        {
            sliders[2].value = 3;

        }

        if (particleValue == 1)
        {
            sliders[3].value = 1;
        }
        else if (particleValue == 2)
        {
            sliders[3].value = 2;

        }
       

    }




}
