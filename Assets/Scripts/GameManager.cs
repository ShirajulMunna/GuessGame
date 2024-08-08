using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event EventHandler OnCheckComplete;
    public event EventHandler OnUnmatchRotationFinish;
    public event EventHandler OnControllButtonPressed;

    public bool firstGuess, secondGuess;
    public string firstImageName, secondImageName;
    public GameObject fisrtObject, secondObject;
    public GameObject singleObject;
    public bool isMatch;
    public string imageName;
    public int checkCounter;
    public RectTransform gameOver,scorePanel;
    public int hitCount,bestScore;
    public TextMeshProUGUI myHitCountTxt,bestscore,myScoreGameOverUI,bestScoregameOverUI;
    public GameObject gameOverPanel,grid, ControllPanel;
    public int time_1,time_2,time_3;
    void Start()
    {
        bestscore.text = PlayerPrefs.GetInt("Best_Score", 0).ToString();
        bestScoregameOverUI.text = PlayerPrefs.GetInt("Best_Score", 0).ToString();
        Instance = this;
        GetComponent<TimeManager>().OnTimeStopped += OpenResultPanel;
        OnCheckComplete += BooleanReset;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12)) 
        {
            ControllPanel.SetActive(true);
            OnControllButtonPressed?.Invoke(this, EventArgs.Empty);


        }
    }
    public void OpenResultPanel(object sender, EventArgs e) 
    {
        Debug.Log("Game time finished");
        AudioManager.instance.GameOver();
        CardRotateManager.instance.EnableCardCollider(false);
        gameOver.DOLocalMoveX(4.00f, 0.1f).SetEase(Ease.InElastic).OnComplete(() =>
        {
            gameOver.DOShakePosition(duration: 1f, strength: 10f, vibrato: 10).OnComplete(() =>
            {

                Invoke("AfterStartAnim", 0.2f);

            });

        });

    }
    public void AfterStartAnim()
    {
        gameOver.DOLocalMoveX(1000f, 0.1f).SetEase(Ease.InElastic).OnComplete(() =>
        {
            AudioManager.instance.GameStart(0);

            grid.SetActive(false);
            gameOverPanel.SetActive(true);
            scorePanel.DOLocalMoveY(43.00f, 0.1f).SetEase(Ease.InElastic).OnComplete(() =>
            {
                scorePanel.DOShakePosition(duration: 1f, strength: 10f, vibrato: 10);
               
            });

        });

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
                    hitCount++;
                    bestScore = hitCount;
                    bestscore.text=hitCount.ToString();
                    myHitCountTxt.text = hitCount.ToString();
                    myScoreGameOverUI.text = hitCount.ToString();
                    bestScoregameOverUI.text = PlayerPrefs.GetInt("Best_Score", 0).ToString();

                    if (bestScore > PlayerPrefs.GetInt("Best_Score", 0)) 
                    {
                        PlayerPrefs.SetInt("Best_Score", bestScore);

                    }
                    Debug.Log("matched");
                    fisrtObject.GetComponent<BoxCollider2D>().enabled = false;
                    secondObject.GetComponent<BoxCollider2D>().enabled = false;
                    AudioManager.instance.MatchSound();

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
        yield return new WaitForSeconds(0.3f);
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

    public void SceneReload() 
    {
        AudioManager.instance.SingleCardClick();
        SceneManager.LoadScene(0);
    
    }


}
