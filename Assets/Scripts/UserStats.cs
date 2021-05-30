using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UserStats
{

    private static int money=120, rounds=0, slotsWinDuo=0, slotsWinTrio=0, bonusCount=0;

    private static int rouletteRounds, rouletteSpins, rouletteWins;

    public static List<float> slotResults = new List<float> { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };

    public static List<float> rouletteColorSquares = new List<float> { 0f, 0f, 0f };

    public static int Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
        }
    }

    public static int Rounds
    {
        get
        {
            return rounds;
        }
        set
        {
            rounds = value;
        }
    }

    public static int SlotsWinDuo
    {
        get
        {
            return slotsWinDuo;
        }
        set
        {
            slotsWinDuo = value;
        }
    }

    public static int SlotsWinTrio
    {
        get
        {
            return slotsWinTrio;
        }
        set
        {
            slotsWinTrio = value;
        }
    }

    public static int BonusCount
    {
        get
        {
            return bonusCount;
        }
        set
        {
            bonusCount = value;
        }
    }

    public static int RouletteRounds
    {
        get
        {
            return rouletteRounds;
        }
        set
        {
            rouletteRounds = value;
        }
    }

    public static int RouletteSpins
    {
        get
        {
            return rouletteSpins;
        }
        set
        {
            rouletteSpins = value;
        }
    }

    public static int RouletteWins
    {
        get
        {
            return rouletteWins;
        }
        set
        {
            rouletteWins = value;
        }
    }

}