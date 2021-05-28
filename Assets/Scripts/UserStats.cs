using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UserStats
{
    private static int kills, deaths, assists, points;

    private static int money=120, rounds=0, slotsWinDuo=0, slotsWinTrio=0;

    public static List<float> slotResults = new List<float> { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };

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

    public static int Kills
    {
        get
        {
            return kills;
        }
        set
        {
            kills = value;
        }
    }

    public static int Deaths
    {
        get
        {
            return deaths;
        }
        set
        {
            deaths = value;
        }
    }

    public static int Assists
    {
        get
        {
            return assists;
        }
        set
        {
            assists = value;
        }
    }

    public static int Points
    {
        get
        {
            return points;
        }
        set
        {
            points = value;
        }
    }
}