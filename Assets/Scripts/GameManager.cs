using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{    
    
    void Start()
    {
        GetComponent<TimeManager>().OnTimeStopped += OpenResultPanel;
     

    }

    public void OpenResultPanel(object sender, EventArgs e) 
    {
        Debug.Log("Game time finished");
        // Here UI panel will open depands on the score of the player
    
    }


}
