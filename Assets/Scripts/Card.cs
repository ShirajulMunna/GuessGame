using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public static Card Instance;
    private SpriteRenderer rend;

    [SerializeField]
    public Sprite faceSprite, backSprite;

    private bool coroutineAllowed, facedUp;
    public string imageName;
    public bool isUnmatched;



   
    void Start()
    {
        Instance = this;
      
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = backSprite;
        coroutineAllowed = true;
        facedUp = false;
    }

  

    private void OnMouseDown()
    {
        AudioManager.instance.SingleCardClick();
        GameManager.Instance.imageName = GetComponent<Card>().faceSprite.name;

        if (coroutineAllowed)
        {
            StartCoroutine(RotateCard());
        }



        if (GameManager.Instance.ImageCheck() == true)
        {
            if (GameManager.Instance.checkCounter == 2) 
            {
                Debug.Log("matched");
                //matching sound


            }


        }
        else 
        {
            if (GameManager.Instance.checkCounter == 2)
            {
                Debug.Log("Doesn't matched");
                isUnmatched = true;
                //Unmatched Sound


            }

        }

    }

    private IEnumerator RotateCard()
    {
        coroutineAllowed = false;

        if (!facedUp)
        {
            
            for (float i = 0f; i <= 180f; i += 10f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    rend.sprite = faceSprite;
                    
                }
                yield return new WaitForSeconds(0.01f);
            }

            if(isUnmatched)
                StartCoroutine(UnmatchedRotate(true));




        }

        else if (facedUp)
        {
            
            for (float i = 180f; i >= 0f; i -= 10f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    rend.sprite = backSprite;
                }
                yield return new WaitForSeconds(0.01f);
            }
        }

        coroutineAllowed = true;

        facedUp = !facedUp;
    }


    public IEnumerator UnmatchedRotate(bool isFaceUp) 
    {
        yield return new WaitForSeconds(1f);
        if (isFaceUp)
        {
            Debug.Log("Unmatched rotate");

            for (float i = 180f; i >= 0f; i -= 10f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    rend.sprite = backSprite;
                }
                yield return new WaitForSeconds(0.01f);
            }
        }
        isUnmatched = false;

    }
   
}
