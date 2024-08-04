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
        StartCoroutine(CountDown());

    }

    public IEnumerator CountDown()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);

            time--;
            if (time > 5)
            {
                AudioManager.instance.SingleTimeClick();


            }
            else 
            {
                AudioManager.instance.ClosingTime();
                AudioManager.instance.isClosingTimeStarted = true;
                
            }


            timerTxt.text = time.ToString();
                      

        }

    }
}
