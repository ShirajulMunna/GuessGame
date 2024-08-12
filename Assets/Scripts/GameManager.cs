using DG.Tweening;
using DG.Tweening.Core.Easing;
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
   // public event EventHandler OnAllItemHit;

    public bool firstGuess, secondGuess;
    public bool isMatch,isGameStart;

    public GameObject gameOverPanel, grid, ControllPanel;
    public GameObject fisrtObject, secondObject;
    public GameObject singleObject;
    public GameObject[] grids;
    public GameObject[] particles;

    public string firstImageName, secondImageName;
    public string imageName;

    public int checkCounter;
    public int hitCount, bestScore;
    public int time_1, time_2, time_3;
    public int target,target_1,target_2,target_3;
    public int particleNumber;


    public RectTransform gameOver,scorePanel;
    public TextMeshProUGUI myHitCountTxt,bestscore,myScoreGameOverUI,bestScoregameOverUI,targetTxt,targetGameOverUI;
    void Start()
    {
        Instance = this;
        int gridValue = PlayerPrefs.GetInt("Grid", 1);
        if (gridValue == 1)
        {
            grids[0].gameObject.SetActive(true);
            grids[1].gameObject.SetActive(false);
            grids[2].gameObject.SetActive(false);

        }
        else if (gridValue == 2)
        {
            grids[0].gameObject.SetActive(false);
            grids[1].gameObject.SetActive(true);
            grids[2].gameObject.SetActive(false);

        }
        else if (gridValue == 3) 
        {
            grids[0].gameObject.SetActive(false);
            grids[1].gameObject.SetActive(false);
            grids[2].gameObject.SetActive(true);

        }

        bestscore.text = PlayerPrefs.GetInt("Best_Score", 0).ToString();
        particleNumber = PlayerPrefs.GetInt("particle", 1);
        targetTxt.text=PlayerPrefs.GetInt("Target",target_1).ToString();
        targetGameOverUI.text=PlayerPrefs.GetInt("Target",target_1).ToString() ;
        GetComponent<TimeManager>().OnTimeStopped += OpenResultPanel;
        OnCheckComplete += BooleanReset;
    }

    private void Update()
    {
        if (!isGameStart) 
        {
            if (Input.GetKeyDown(KeyCode.F12))
            {
                ControllPanel.SetActive(true);
                OnControllButtonPressed?.Invoke(this, EventArgs.Empty);
                isGameStart = true;


            }

        }
      
    }

    public void Settarget(int target) 
    {
        this.target = target;
        targetTxt.text=target.ToString();
        targetGameOverUI.text=target.ToString();
    
    }
    public void SetParticle(int particle) 
    {
        this.particleNumber = particle;

    
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
                if (particleNumber == 1)
                {
                    particles[0].SetActive(true);
                    particles[1].SetActive(false);


                }
                else if (particleNumber == 2)
                {
                    particles[0].SetActive(false);
                    particles[1].SetActive(true);

                }

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
                   

                    if (bestScore > PlayerPrefs.GetInt("Best_Score", 0))
                    {
                        PlayerPrefs.SetInt("Best_Score", bestScore);
                        bestScoregameOverUI.text = PlayerPrefs.GetInt("Best_Score", 0).ToString();


                    }
                    else if (bestScore < PlayerPrefs.GetInt("Best_Score", 0))
                    {
                        bestScoregameOverUI.text = PlayerPrefs.GetInt("Best_Score", 0).ToString();



                    }
                    else if (bestScore == hitCount) 
                    {
                        bestScoregameOverUI.text = hitCount.ToString();


                    }


                    Debug.Log("matched");

                    if (hitCount == target) 
                    {
                       
                        TimeManager.Instance.time = 1;


                    }

                  
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
                    CardRotateManager.instance.MouseClickable(false);
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

                   // OnUnmatchRotationFinish?.Invoke(this, EventArgs.Empty);
                  
                }
                yield return new WaitForSeconds(0.01f);
            }
        }
        checkCounter = 0;
        OnUnmatchRotationFinish?.Invoke(this, EventArgs.Empty);


    }

    public void SceneReload() 
    {
        AudioManager.instance.SingleCardClick();
        SceneManager.LoadScene(0);
    
    }

    public void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }


}
