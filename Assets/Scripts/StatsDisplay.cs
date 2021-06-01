using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class StatsDisplay : MonoBehaviour
{

    public TMP_Text userRoundsText;

    public TMP_Text WinsDuoText;
    public TMP_Text WinsTrioText;
    public TMP_Text BonusCountText;

    public TMP_Text rouletteRounds;
    public TMP_Text rouletteSpins;
    public TMP_Text rouletteWins;

    void Start()
    {
        userRoundsText.text = "Rounds played : " + UserStats.Rounds;
        WinsDuoText.text = "Wins duo : " + UserStats.SlotsWinDuo;
        WinsTrioText.text = "Wins trio : " + UserStats.SlotsWinTrio;
        BonusCountText.text = "Bonus count : " + UserStats.BonusCount;

        rouletteRounds.text = "Rounds played : " + UserStats.RouletteRounds;
        rouletteSpins.text = "Spins : " + UserStats.RouletteSpins;
        rouletteWins.text = "Wins : " + UserStats.RouletteWins;

    }



}
