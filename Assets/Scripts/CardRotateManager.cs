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
    public Sprite naver;

    public AudioManager audioManager;
    public List<GameObject> cards;
    public GameObject card;
    public GameObject dummyCard;
    public List<int> shuffleValues = new List<int>();
    public List<int> shuffleValues_2 = new List<int>();
    public List<int> mergeList= new List<int>();
    public List<int> availAbleCardsNumber= new List<int>();
    public List<GameObject>dummy=new List<GameObject>();


    private bool coroutineAllowed, facedUp;
    public int gridValue;
    public int randomDummyNumber;
    void Start()
    {
        randomDummyNumber = Random.Range(0, 8);
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

        if (gridValue == 9)
        {
            for (int i = 0; i < cards.Count; i++)
            {

                if (gridValue == 9)
                {
                    if (i != randomDummyNumber)
                    {
                        dummy.Add(cards[i]);

                    }
                    else
                    {
                        dummyCard = card[i];

                    }                

                }
                           
            }

            for (int i = 0; i < dummy.Count; i++) 
            {
                int suffleValue;
                suffleValue = Random.Range(0, frontCard_9.Length);

                if (!shuffleValues.Contains(suffleValue) && shuffleValues.Count <= 3)
                {
                    shuffleValues.Add(suffleValue);
                    shuffleValues_2.Add(suffleValue);
                    mergeList = shuffleValues.Concat(shuffleValues_2).ToList();

                }
                dummy[i].GetComponent<BoxCollider2D>().enabled = false;


            }

            for (int i = 0; i < dummy.Count; i++)
            {
                availAbleCardsNumber.Add(i);

            }

            for (int i = 0; i < dummy.Count; i++)
            {
                int randomIndex = Random.Range(0, availAbleCardsNumber.Count);
                int cardValue = availAbleCardsNumber[randomIndex];
                availAbleCardsNumber.RemoveAt(randomIndex);
                int cardVal = mergeList[i];
                                
                dummy[cardValue].GetComponent<Card>().faceSprite = frontCard_9[cardVal];
                dummyCard.GetComponent<Card>().faceSprite = naver;

            }
            StartCoroutine(RotateDummyCard());

            StartCoroutine( RotateDummySingleCard());

        }
        else if (gridValue == 12)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                int suffleValue;

                suffleValue = Random.Range(0, frontCard_12.Length);
                if (!shuffleValues.Contains(suffleValue) && shuffleValues.Count <= 5)
                {
                     shuffleValues.Add(suffleValue);
                     shuffleValues_2.Add(suffleValue);
                     mergeList = shuffleValues.Concat(shuffleValues_2).ToList();

                }           

                cards[i].GetComponent<BoxCollider2D>().enabled = false;


            }

            for (int i = 0; i < cards.Count; i++)
            {
                availAbleCardsNumber.Add(i);

            }

            for (int i = 0; i < cards.Count; i++)
            {
                int randomIndex = Random.Range(0, availAbleCardsNumber.Count);
                int cardValue = availAbleCardsNumber[randomIndex];
                availAbleCardsNumber.RemoveAt(randomIndex);
                int cardVal = mergeList[i];                             
                cards[cardValue].GetComponent<Card>().faceSprite = frontCard_12[cardVal];
                
            }
            StartCoroutine(RotateCard());

        }
        else if (gridValue == 16)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                int suffleValue;
             
               
                suffleValue = Random.Range(0, frontCard_16.Length);
                 if (!shuffleValues.Contains(suffleValue) && shuffleValues.Count <= 7)
                 {
                        shuffleValues.Add(suffleValue);
                        shuffleValues_2.Add(suffleValue);
                        mergeList = shuffleValues.Concat(shuffleValues_2).ToList();

                 }
               

                cards[i].GetComponent<BoxCollider2D>().enabled = false;


            }

            for (int i = 0; i < cards.Count; i++)
            {
                availAbleCardsNumber.Add(i);

            }

            for (int i = 0; i < cards.Count; i++)
            {
                int randomIndex = Random.Range(0, availAbleCardsNumber.Count);
                int cardValue = availAbleCardsNumber[randomIndex];
                availAbleCardsNumber.RemoveAt(randomIndex);
                int cardVal = mergeList[i];
                             
                cards[cardValue].GetComponent<Card>().faceSprite = frontCard_16[cardVal];
              
            }
            StartCoroutine(RotateCard());

        }            
       
    }

    private IEnumerator RotateDummySingleCard()
    {
        yield return new WaitForSeconds(1f);

        coroutineAllowed = false;

        if (!facedUp)
        {
                    
                

                for (float i = 0f; i <= 360f; i += 10f)
                {
                    
                    dummyCard.transform.rotation = Quaternion.Euler(0f, i, 0f);

                    if (i == 90f)
                    {
                         dummyCard.GetComponent<SpriteRenderer>().sprite = dummyCard.GetComponent<Card>().faceSprite;

                       
                    }
                    yield return new WaitForSeconds(0.00001f);
                }

          


        }
      

    }

   public IEnumerator RotateDummySingleBackward()
    {
        yield return new WaitForSeconds(1);

        if (facedUp)
        {
        

                for (float i = 0f; i <=360f; i += 10f)
                {
                    dummyCard.transform.rotation = Quaternion.Euler(0f, i, 0f);
                 
                     Debug.Log("dsfssfsfsfsfsfsfsfsfsfs");

                    if (i == 90f)
                    {
                       dummyCard.GetComponent<SpriteRenderer>().sprite = dummyCard.GetComponent<Card>().backSprite;

                    
                    }
                 


            }


        }

        coroutineAllowed = true;
        


    }

    private IEnumerator RotateDummyCard()
    {
        yield return new WaitForSeconds(1f);

        coroutineAllowed = false;

        if (!facedUp)
        {

            for (int j = 0; j < dummy.Count; j++)
            {
                AudioManager.instance.SingleCardClick();

                for (float i = 0f; i <= 180f; i += 10f)
                {
                    dummy[j].transform.rotation = Quaternion.Euler(0f, i, 0f);
                    

                    if (i == 90f)
                    {

                        dummy[j].GetComponent<SpriteRenderer>().sprite = dummy[j].GetComponent<Card>().faceSprite;
                    }
                    yield return new WaitForSeconds(0.00001f);
                }



            }


        }
       facedUp = !facedUp;
        StartCoroutine(RotateDummySingleBackward());

        StartCoroutine(RotateDummyBackward());


    }

    IEnumerator RotateDummyBackward()
    {
        yield return new WaitForSeconds(1);

        if (facedUp)
        {

            for (int j = 0; j < dummy.Count; j++)
            {

                for (float i = 0f; i <= 360f; i += 10f)
                {
                    dummy[j].transform.rotation = Quaternion.Euler(0f, i, 0f);

                    if (i == 90f)
                    {

                        dummy[j].GetComponent<SpriteRenderer>().sprite = dummy[j].GetComponent<Card>().backSprite;
                    }
                    

                }

            }
        }

        coroutineAllowed = true;

        facedUp = !facedUp;
        OnRotationFinished?.Invoke(this, EventArgs.Empty);


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
