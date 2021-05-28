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
    private TMP_Text userMoneyText;

    [SerializeField]
    private TMP_Text userRoundText;

    [SerializeField]
    private Row[] rows;

    [SerializeField]
    private Transform handle;

    private int prizeValue;

    private bool resultsChecked = false;
    private bool textUpdated = false;

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
        if (textUpdated)
        {
            textUpdated = false;

            //Increments rounds number
            userRoundText.text = UserStats.slotResults[2].ToString();

            userMoneyText.text = UserStats.Money.ToString();
        }
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && !resultsChecked)
        {
            //Checking results
            CheckResults();

            //Update prize text
            prizeText.enabled = true;
            prizeText.text = "Prize : " + Mathf.RoundToInt(prizeValue * prizeBonus);

            //Add money to user
            UserStats.Money += Mathf.RoundToInt(prizeValue * prizeBonus);
            userMoneyText.text = UserStats.Money.ToString();



        }
    }

    private void OnMouseDown()
    {
        if(rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
        {
            UserStats.Rounds += 1;
            UserStats.Money -= 10;
            textUpdated = true;
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
            UserStats.SlotsWinTrio += 1;
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
            UserStats.SlotsWinDuo += 1;
        }

        //Bonus application
        float randomValue = UnityEngine.Random.Range(0.0f, 1.0f);

        if(randomValue < sliderUI.value)
        {
            float bonusValue = Probabilities.Poisson(0, sliderUI.value);
            prizeBonus = 1+bonusValue;
        }

        resultsChecked = true;
    }
}
