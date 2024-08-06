using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public event EventHandler OnCheckComplete;
    public bool firstGuess, secondGuess;
    public string firstImageName, secondImageName;
    public bool isMatch;
    public string imageName;
    public int checkCounter;

    void Start()
    {
        Instance = this;
        GetComponent<TimeManager>().OnTimeStopped += OpenResultPanel;
        OnCheckComplete += BooleanReset;



    }

    public void OpenResultPanel(object sender, EventArgs e) 
    {
        Debug.Log("Game time finished");
       
    
    }

    public void BooleanReset(object sender, EventArgs e) 
    {
        firstGuess = false;
        secondGuess = false;
      
    
    }

    public bool ImageCheck()
    {
        if (!firstGuess)
        {
            firstGuess = true;
            firstImageName = imageName;
            checkCounter = 1;

        }
        else if (!secondGuess)
        {
            secondGuess = true; 
            secondImageName=imageName;
            checkCounter = 2;
            OnCheckComplete?.Invoke(this, EventArgs.Empty);
            
            if (firstImageName == secondImageName)
            {
                isMatch = true;

            }
            else 
            {
                isMatch=false;
            
            }
          

        }

        
        return isMatch==true?true:false;

    }


}
