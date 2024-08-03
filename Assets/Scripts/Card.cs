using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    
    private SpriteRenderer rend;

    [SerializeField]
    public Sprite faceSprite, backSprite;

    private bool coroutineAllowed, facedUp;

   
    void Start()
    {
      
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = backSprite;
        coroutineAllowed = true;
        facedUp = false;
    }

  

    private void OnMouseDown()
    {
        AudioManager.instance.SingleCardClick();
        if (coroutineAllowed)
        {
            StartCoroutine(RotateCard());
        }
    }

    private IEnumerator RotateCard()
    {
        coroutineAllowed = false;

        if (!facedUp)
        {
            Debug.Log("first rotate");
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

        else if (facedUp)
        {
            Debug.Log("second rotate");

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
}
