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
            time--;
            if (time >= 5)
            {
                AudioManager.instance.SingleTimeClick(true);


            }
            else 
            {
             AudioManager.instance.SingleTimeClick(false);

            }

            yield return new WaitForSeconds(1);

            timerTxt.text = time.ToString();
            Debug.Log(time);

           


        }

    }
}
