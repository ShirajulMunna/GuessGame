using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public event EventHandler OnstartPressed;
    public static AudioManager instance;
    private AudioSource m_AudioSource;
    public AudioClip click;
    public AudioClip cardClick;
    public AudioClip timeClick;
    public AudioClip quickTime;
    void Start()
    {
        
        instance = this;
        m_AudioSource= GetComponent<AudioSource>();
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
    public void SingleTimeClick(bool isPlaying)
    {
        if (isPlaying)
        {
            m_AudioSource.PlayOneShot(timeClick);


        }
        else 
        {
            //m_AudioSource.Stop();
          //  m_AudioSource.PlayOneShot(quickTime);

        }

    }






}
