using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardRotateManager : MonoBehaviour
{
    public static CardRotateManager instance;
    public event EventHandler OnRotationFinished;

    public Sprite[] frontCard_9;
    public Sprite[] frontCard_12;
    public Sprite[] frontCard_16;

    public AudioManager audioManager;
    public List<GameObject> cards;  
    public List<int> shuffleValues = new List<int>();
    public List<int> shuffleValues_2 = new List<int>();
    public List<int> mergeList= new List<int>();
    public List<int> availAbleCardsNumber= new List<int>();

    private bool coroutineAllowed, facedUp;
    public int gridValue=4;
    void Start()
    {
        int gridNumber = PlayerPrefs.GetInt("Grid", 1);

        if (gridNumber == 1)
        {
            gridValue = 9;

        }
        else if (gridNumber == 2)
        {
            gridValue = 12;


        }

        else if (gridNumber == 3) 
        {
            gridValue = 16;
        
        }
        instance = this;
        coroutineAllowed = true;
        facedUp = false;
        audioManager.OnstartPressed += FlipAllTheCardAtOnce;

    }

    public void SetGrid(int grid)
    {
        this.gridValue = grid;
        


    }
    public void FlipAllTheCardAtOnce(object sender, EventArgs e)
    {
        GameManager.Instance.isGameStart = true;
        GameObject[] card = GameObject.FindGameObjectsWithTag("Card");
        cards=new List<GameObject>(card);
        
        for (int i = 0; i < cards.Count; i++) 
        {
            int suffleValue;

            if (gridValue == 9)
            {
                suffleValue = Random.Range(0, frontCard_9.Length);

                if (!shuffleValues.Contains(suffleValue) && shuffleValues.Count<=3) 
                {
                    shuffleValues.Add(suffleValue);
                    shuffleValues_2.Add(suffleValue);
                    mergeList=shuffleValues.Concat(shuffleValues_2).ToList();                   

                }
                
            }
            else if (gridValue == 12)
            {
                suffleValue = Random.Range(0, frontCard_12.Length);
                if (!shuffleValues.Contains(suffleValue) && shuffleValues.Count <= 5)
                {
                    shuffleValues.Add(suffleValue);
                    shuffleValues_2.Add(suffleValue);
                    mergeList = shuffleValues.Concat(shuffleValues_2).ToList();

                }


            }
            else if(gridValue==16)
            {
                suffleValue = Random.Range(0, frontCard_16.Length);
                if (!shuffleValues.Contains(suffleValue) && shuffleValues.Count <= 7)
                {
                    shuffleValues.Add(suffleValue);
                    shuffleValues_2.Add(suffleValue);
                    mergeList = shuffleValues.Concat(shuffleValues_2).ToList();

                }

            }

            cards[i].GetComponent<BoxCollider2D>().enabled=false;
  
        
        }

        for (int i = 0; i < cards.Count; i++) 
        {
            availAbleCardsNumber.Add(i);
        
        }

        for (int i = 0;i < cards.Count;i++) 
        {
            int randomIndex = Random.Range(0, availAbleCardsNumber.Count);
            int cardValue = availAbleCardsNumber[randomIndex];
            availAbleCardsNumber.RemoveAt(randomIndex);
            int cardVal = mergeList[i];
            if (gridValue == 9)
            {
                
                cards[cardValue].GetComponent<Card>().faceSprite = frontCard_9[cardVal];

            }
            else if (gridValue == 12)
            {
                cards[cardValue].GetComponent<Card>().faceSprite = frontCard_12[cardVal];


            }
            else if (gridValue == 16) 
            {
                cards[cardValue].GetComponent<Card>().faceSprite = frontCard_16[cardVal];

            }


        }
        StartCoroutine(RotateCard());
        
    }

    private IEnumerator RotateCard() 
    {
        yield return new WaitForSeconds(1f);

        coroutineAllowed = false;

        if (!facedUp)
        {
          
            for (int j = 0; j < cards.Count; j++) 
            {
                AudioManager.instance.SingleCardClick();

                for (float i = 0f; i <= 180f; i += 10f)
                {
                    cards[j].transform.rotation = Quaternion.Euler(0f, i, 0f);
                    if (i == 90f)
                    {
                        
                        cards[j].GetComponent<SpriteRenderer>().sprite = cards[j].GetComponent<Card>().faceSprite;
                    }
                    yield return new WaitForSeconds(0.00001f);
                }

            }
           
        }
        facedUp = !facedUp;

        StartCoroutine(RotateBackward());

    }

    IEnumerator RotateBackward() 
    {
        yield return new WaitForSeconds(1);

       if (facedUp)
       {
            
            for (int j = 0; j < cards.Count; j++)
            {
                
                for (float i = 0f; i <= 360f; i += 10f)
                {
                    cards[j].transform.rotation = Quaternion.Euler(0f, i, 0f);
                   
                    if (i == 90f)
                    {

                        cards[j].GetComponent<SpriteRenderer>().sprite = cards[j].GetComponent<Card>().backSprite;
                    }
                   // yield return new WaitForSeconds(0.00001f);

                }

            }
        }

        coroutineAllowed = true;

        facedUp = !facedUp;
        OnRotationFinished?.Invoke(this, EventArgs.Empty);
       

    }

    public void EnableCardCollider(bool isEnable) 
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].GetComponent<BoxCollider2D>().enabled = isEnable;

        }


    }

    public void MouseClickable(bool isClickable) 
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].GetComponent<Card>().isClickable = isClickable;

        }


    }



}
