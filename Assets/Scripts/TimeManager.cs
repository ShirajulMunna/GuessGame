using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    public event EventHandler OnTimeStopped;

    public int time;
    public TextMeshProUGUI timerTxt;
    public CardRotateManager cardRotationManager;
    public RectTransform ready,start;
    public GameObject spark, spark_2;
    void Start()
    {
        Instance = this;
        cardRotationManager.OnRotationFinished += StartCoundown;
        time = PlayerPrefs.GetInt("Time", GameManager.Instance.time_1);
        timerTxt.text = time.ToString();


    }

    public void SetTime(int time) 
    {
        this.time = time;
        timerTxt.text = time.ToString();


    }


    public void StartCoundown(object sender,EventArgs e)
    {
        AudioManager.instance.BackGroundSoundOff();
        ready.DOLocalMoveY(-39.00f, 0.1f).SetEase(Ease.InElastic).OnComplete(() =>
        {
            AudioManager.instance.GameStart(0);



            ready.DOShakePosition(duration: 1f, strength: 10f, vibrato: 10).OnComplete(() =>
            {
                Invoke("AfterStartAnimFinish", 0.2f);

            });



        });
     

    }
    public void AfterStartAnimFinish()
    {
        ready.DOLocalMoveX(1500f, 0.1f).SetEase(Ease.InBounce).OnComplete(() => {

            start.DOLocalMoveX(4.00f, 0.1f).SetEase(Ease.InElastic).OnComplete(() =>
            {
                AudioManager.instance.GameStart(1);

                spark.SetActive(true);
                spark_2.SetActive(true);
                start.DOShakePosition(duration: 1f, strength: 10f, vibrato: 10).OnComplete(() =>
                {
                    start.DOLocalMoveX(1000f, 0.1f).SetEase(Ease.InElastic).OnComplete(() => {
                        CardRotateManager.instance.EnableCardCollider(true);
                    });
                    spark.SetActive(false);
                    spark_2.SetActive(false);
                    StartCoroutine(CountDown());


                });

            });

        });

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
                    OnTimeStopped?.Invoke(this, EventArgs.Empty);

                }

            }
                              

        }

    }

   
   
}
