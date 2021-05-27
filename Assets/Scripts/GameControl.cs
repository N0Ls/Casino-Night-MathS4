using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class GameControl : MonoBehaviour
{

    public static event Action HandlePulled = delegate { };

    [SerializeField]
    private TMP_Text prizeText;

    [SerializeField]
    private Row[] rows;

    [SerializeField]
    private Transform handle;

    private int prizeValue;

    private bool resultsChecked = false;

    private float prizeBonus = 1.0f;

    public Slider sliderUI;

    // Update is called once per frame
    void Update()
    {
        if(!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped)
        {
            prizeValue = 0;
            prizeBonus = 1.0f;
            prizeText.enabled = false;
            resultsChecked = false;

        }
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && !resultsChecked)
        {
            CheckResults();
            prizeText.enabled = true;
            prizeText.text = "Prize : " + Mathf.RoundToInt(prizeValue * prizeBonus);

        }
    }

    private void OnMouseDown()
    {
        if(rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
        {
            StartCoroutine("PullHandle");
        }
    }

    private IEnumerator PullHandle()
    {
        for(int i = 0; i < 15; i += 5)
        {
            handle.Rotate(0f, 0f, i);
            yield return new WaitForSeconds(0.1f);
        }

        HandlePulled();

        for (int i = 0; i < 15; i += 5)
        {
            handle.Rotate(0f, 0f, -i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void CheckResults()
    {

        Debug.Log("Results Checked");

        //if (rows[0].stoppedSlot == "Watermelon"
        //    && rows[1].stoppedSlot == "Watermelon"
        //    && rows[2].stoppedSlot == "Watermelon")
        //    prizeValue = 100;

        //else if(rows[0].stoppedSlot == "Grapes"
        //    && rows[1].stoppedSlot == "Grapes"
        //    && rows[2].stoppedSlot == "Grapes")
        //    prizeValue = 100;

        //else if (rows[0].stoppedSlot == "Strawberry"
        //    && rows[1].stoppedSlot == "Strawberry"
        //    && rows[2].stoppedSlot == "Strawberry")
        //    prizeValue = 100;

        //else if (rows[0].stoppedSlot == "Lemon"
        //    && rows[1].stoppedSlot == "Lemon"
        //    && rows[2].stoppedSlot == "Lemon")
        //    prizeValue = 100;

        //else if (rows[0].stoppedSlot == "Orange"
        //    && rows[1].stoppedSlot == "Orange"
        //    && rows[2].stoppedSlot == "Orange")
        //    prizeValue = 100;

        //else if (rows[0].stoppedSlot == "Pear"
        //    && rows[1].stoppedSlot == "Pear"
        //    && rows[2].stoppedSlot == "Pear")
        //    prizeValue = 100;

        //else if (rows[0].stoppedSlot == "Cherry"
        //    && rows[1].stoppedSlot == "Cherry"
        //    && rows[2].stoppedSlot == "Cherry")
        //    prizeValue = 100;

        //else if (rows[0].stoppedSlot == "Banana"
        //    && rows[1].stoppedSlot == "Banana"
        //    && rows[2].stoppedSlot == "Banana")
        //    prizeValue = 100;


        //In the case we have the three fruits
        if(rows[0].stoppedSlot == rows[1].stoppedSlot && rows[1].stoppedSlot == rows[2].stoppedSlot)
        {
            switch (rows[0].stoppedSlot)
            {
                case "Watermelon":
                    prizeValue = 200;
                    break;
                case "Grapes":
                    prizeValue = 120;
                    break;
                case "Strawberry":
                    prizeValue = 100;
                    break;
                case "Lemon":
                    prizeValue = 150;
                    break;
                case "Orange":
                    prizeValue = 250;
                    break;
                case "Pear":
                    prizeValue = 1000;
                    break;
                case "Cherry":
                    prizeValue = 500;
                    break;
                case "Banana":
                    prizeValue = 300;
                    break;
                default:
                    prizeValue = 0;
                    break;
            }

        } 
        // In case we have two fruits
        else if (rows[0].stoppedSlot == rows[1].stoppedSlot || rows[0].stoppedSlot == rows[2].stoppedSlot || rows[1].stoppedSlot == rows[2].stoppedSlot)
        {
            int equalRows = 1;
            if(rows[0].stoppedSlot == rows[1].stoppedSlot)
            {
                equalRows = 12;
            }
            if (rows[0].stoppedSlot == rows[2].stoppedSlot)
            {
                equalRows = 13;
            }
            if (rows[1].stoppedSlot == rows[2].stoppedSlot)
            {
                equalRows = 23;
            }

            int winningRow = equalRows % 10;

            switch (rows[winningRow-1].stoppedSlot)
            {
                case "Watermelon":
                    prizeValue = 20;
                    break;
                case "Grapes":
                    prizeValue = 12;
                    break;
                case "Strawberry":
                    prizeValue = 10;
                    break;
                case "Lemon":
                    prizeValue = 15;
                    break;
                case "Orange":
                    prizeValue = 25;
                    break;
                case "Pear":
                    prizeValue = 100;
                    break;
                case "Cherry":
                    prizeValue = 50;
                    break;
                case "Banana":
                    prizeValue = 30;
                    break;
                default:
                    prizeValue = 0;
                    break;
            }
        }

        //Bonus application
        float randomValue = UnityEngine.Random.Range(0.0f, 1.0f);

        if(randomValue < sliderUI.value)
        {
            prizeBonus = 1.3f;
        }

        resultsChecked = true;
    }
}
