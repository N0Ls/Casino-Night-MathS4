using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class StatsDisplay : MonoBehaviour
{

    public TMP_Text userRoundsText;


    public TMP_Text userText;



    // Start is called before the first frame update
    void Start()
    {
        userRoundsText.text = "Rounds played : " + UserStats.Rounds;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
