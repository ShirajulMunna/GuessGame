using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public event EventHandler OnstartPressed;
    public static AudioManager instance;
    private AudioSource m_AudioSource;
    private AudioSource m_AudioSource_1;
    public AudioClip click;
    public AudioClip cardClick;
    public AudioClip timeClick;
    public AudioClip quickTime;
    public AudioClip matchSound;
    public AudioClip gameOver;
    public AudioClip[] gameStart;
    public AudioClip perfectSound;
    public bool isClosingTimeStarted=true;
    void Start()
    {
        
        instance = this;
        m_AudioSource= GetComponent<AudioSource>();
        m_AudioSource_1 = GetComponent<AudioSource>();

    }
    public void SingleClick() 
    {
        m_AudioSource.PlayOneShot(click);
        OnstartPressed?.Invoke(this, EventArgs.Empty);
        
    
    }
    public void SingleCardClick()
    {
        m_AudioSource.PlayOneShot(cardClick);
        
    }
    public void SingleTimeClick()
    {
        m_AudioSource.PlayOneShot(timeClick);                 
             

    }
    public void ClosingTime() 
    {
        if (isClosingTimeStarted)
        {
            m_AudioSource.Stop();
            m_AudioSource_1.PlayOneShot(quickTime);

           
        }       


    }
    public void ClosingTimeOff()
    {
        m_AudioSource_1.Stop();
      

    }
    public void MatchSound()
    {
        m_AudioSource.PlayOneShot(matchSound);


    }

    public void BackGroundSoundOff() 
    {
            m_AudioSource.Stop();

    }

    public void GameStart(int index)
    {
        m_AudioSource.PlayOneShot(gameStart[index]);

    }

    public void GameOver() 
    {
        m_AudioSource.PlayOneShot(gameOver);
    
    }

    public void Perfect() 
    {
        m_AudioSource.PlayOneShot(perfectSound);
    
    }



}
