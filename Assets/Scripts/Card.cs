using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public static Card Instance;
    private SpriteRenderer rend;

    [SerializeField]
    public Sprite faceSprite, backSprite;

    public bool coroutineAllowed, facedUp;
    public string imageName;
    public bool isUnmatched,isClickable=true;
   
    void Start()
    {
        Instance = this;
        GameManager.Instance.OnUnmatchRotationFinish += BooleanReset;     
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = backSprite;
        coroutineAllowed = true;
        facedUp = false;
    }
    public void BooleanReset(object sender, EventArgs e) 
    {
        facedUp=false;
        isClickable=true;
        GetComponent<BoxCollider2D>().enabled = true;


    }

    private void OnMouseDown()
    {
        if (isClickable) 
        {
            AudioManager.instance.SingleCardClick();
            GameManager.Instance.imageName = GetComponent<Card>().faceSprite.name;
            GameManager.Instance.singleObject = this.gameObject;
            GetComponent<BoxCollider2D>().enabled = false;
            if (coroutineAllowed)
            {
                StartCoroutine(RotateCard());
            }
            GameManager.Instance.ImageCheck();

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
         
        }
      
        coroutineAllowed = true;
        facedUp = !facedUp;
    }


   
   
}
