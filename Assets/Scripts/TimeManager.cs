using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public int time;
    public TextMeshProUGUI timerTxt;
    public CardRotateManager cardRotationManager;
    void Start()
    {
        cardRotationManager.OnRotationFinished += StartCoundown;

    }

    public void StartCoundown(object sender,EventArgs e)
    {
       // AudioManager.instance.m_AudioSource.Stop();
        StartCoroutine(CountDown());

    }

    public IEnumerator CountDown()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);

            time--;
            timerTxt.text = time.ToString();

            if (time > 5)
            {
                AudioManager.instance.SingleTimeClick();


            }
            else if (time <= 5)
            {
               
                AudioManager.instance.ClosingTime();
                AudioManager.instance.isClosingTimeStarted = true;

                if (time <= 0) 
                {                   
                    
                    AudioManager.instance.ClosingTimeOff();
                  

                }

            }
           
          


                      

        }

    }
}
