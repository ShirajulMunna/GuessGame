using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardRotateManager : MonoBehaviour
{
    public event EventHandler OnRotationFinished;
    public Sprite[] frontCard;

    public AudioManager audioManager;
    public List<GameObject> cards;  
    private bool coroutineAllowed, facedUp;
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
            int val = Random.Range(0, frontCard.Length);
            
            cards[i].GetComponent<BoxCollider2D>().enabled=false;
            cards[i].GetComponent<Card>().faceSprite = frontCard[val];
        
        }
        StartCoroutine(RotateCard());
        
    }

    private IEnumerator RotateCard()
    {
        yield return new WaitForSeconds(1f);

        coroutineAllowed = false;

        if (!facedUp)
        {
            Debug.Log("first rotate");
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
            Debug.Log("second rotate");

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
