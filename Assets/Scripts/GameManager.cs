using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public event EventHandler OnCheckComplete;
    public event EventHandler OnUnmatchRotationFinish;
    public bool firstGuess, secondGuess;
    public string firstImageName, secondImageName;
    public GameObject fisrtObject, secondObject;
    public GameObject singleObject;
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

    public void ImageCheck()
    {
        if (!firstGuess)
        {
            firstGuess = true;
            firstImageName = imageName;
            fisrtObject = singleObject;
            checkCounter = 1;

        }
        else if (!secondGuess)
        {
            secondGuess = true; 
            secondImageName=imageName;
            secondObject= singleObject;
            checkCounter = 2;
            OnCheckComplete?.Invoke(this, EventArgs.Empty);
            
            if (firstImageName == secondImageName)
            {
                isMatch = true;
                if (checkCounter == 2)
                {
                    Debug.Log("matched");
                    fisrtObject.GetComponent<BoxCollider2D>().enabled = false;
                    secondObject.GetComponent<BoxCollider2D>().enabled = false; 
                    //matching sound

                }

            }
            else 
            {
                isMatch=false;
                if (checkCounter == 2)
                {
                    Debug.Log("Unmatched");
                    StartCoroutine(UnmatchedRotate(true));
                    //Unmatching sound

                }

            }
          

        }

      

    }
    public IEnumerator UnmatchedRotate(bool isFaceUp)
    {
        yield return new WaitForSeconds(1f);
        if (isFaceUp)
        {
            Debug.Log("Unmatched rotate");

            for (float i = 180f; i >= 0f; i -= 10f)
            {
                fisrtObject.transform.rotation=Quaternion.Euler(0f, i, 0f);
                secondObject.transform.rotation=Quaternion.Euler(0f, i, 0f);
             
                if (i == 90f)
                {
                    fisrtObject.GetComponent<SpriteRenderer>().sprite=fisrtObject.GetComponent<Card>().backSprite;
                    secondObject.GetComponent<SpriteRenderer>().sprite = fisrtObject.GetComponent<Card>().backSprite;
                    OnUnmatchRotationFinish?.Invoke(this, EventArgs.Empty);

                  
                }
                yield return new WaitForSeconds(0.01f);
            }
        }
        checkCounter = 0;
      

    }


}
