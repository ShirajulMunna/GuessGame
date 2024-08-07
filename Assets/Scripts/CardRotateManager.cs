using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardRotateManager : MonoBehaviour
{
    public event EventHandler OnRotationFinished;
    public Sprite[] frontCard;

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
        coroutineAllowed = true;
        facedUp = false;
        audioManager.OnstartPressed += FlipAllTheCardAtOnce;

    }

    public void FlipAllTheCardAtOnce(object sender, EventArgs e)
    {
        GameObject[] card = GameObject.FindGameObjectsWithTag("Card");
        cards=new List<GameObject>(card);
        
        for (int i = 0; i < cards.Count; i++) 
        {
            int suffleValue;

            if (gridValue == 4)
            {
                suffleValue = Random.Range(0, frontCard.Length);

                if (!shuffleValues.Contains(suffleValue) && shuffleValues.Count<=7) 
                {
                    shuffleValues.Add(suffleValue);
                    shuffleValues_2.Add(suffleValue);
                    mergeList=shuffleValues.Concat(shuffleValues_2).ToList();                   

                }
                
            }
            else if (gridValue == 6)
            {
                suffleValue = Random.Range(0, frontCard.Length);
                if (!shuffleValues.Contains(suffleValue) && shuffleValues.Count <= 8)
                {
                    shuffleValues.Add(suffleValue);
                    shuffleValues_2.Add(suffleValue);
                    mergeList = shuffleValues.Concat(shuffleValues_2).ToList();

                }


            }
            else 
            {
                suffleValue = Random.Range(0, frontCard.Length);
                if (!shuffleValues.Contains(suffleValue) && shuffleValues.Count <= 5)
                {
                    shuffleValues.Add(suffleValue);
                    shuffleValues_2.Add(suffleValue);
                    mergeList = shuffleValues.Concat(shuffleValues_2).ToList();

                }


            }

            cards[i].GetComponent<BoxCollider2D>().enabled=false;

            Debug.Log(mergeList.Count);
       
        
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
            Debug.Log("cardVal"+cardValue);
            int cardVal = mergeList[i];
            cards[cardValue].GetComponent<Card>().faceSprite = frontCard[cardVal];

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
                    yield return new WaitForSeconds(0.0001f);
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
                
                for (float i = 0f; i <= 180f; i += 10f)
                {
                    cards[j].transform.rotation = Quaternion.Euler(0f, i, 0f);
                   
                    if (i == 90f)
                    {

                        cards[j].GetComponent<SpriteRenderer>().sprite = cards[j].GetComponent<Card>().backSprite;
                    }
                    yield return new WaitForSeconds(0.0001f);

                }

            }
        }

        coroutineAllowed = true;

        facedUp = !facedUp;
        OnRotationFinished?.Invoke(this, EventArgs.Empty);
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].GetComponent<BoxCollider2D>().enabled = true;

        }



    }



}
