using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    private int currentDay = 0; //what day is it now
    private const int maxDays = 30; //how many days of gameplay do u have to reach the goal

    public int CurrentDay { get => currentDay; }
    public int MaxDays { get => maxDays; }

    public enum TimeOfDay { Morning, Midday, Afternoon, Evening }
    private TimeOfDay currentTimeOfDay;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void AdvanceTime()
    {
        switch (currentTimeOfDay)
        {
            case TimeOfDay.Morning:
                currentTimeOfDay = TimeOfDay.Midday;
                break;
            case TimeOfDay.Midday:
                currentTimeOfDay = TimeOfDay.Afternoon;
                break;
            case TimeOfDay.Afternoon:
                currentTimeOfDay = TimeOfDay.Evening;
                break;
            case TimeOfDay.Evening:
                currentDay++;
                currentTimeOfDay = TimeOfDay.Morning;
                break;
            default:
                break;
        }
    }


}

//config stuff, keys used for saving, etc
public static class Constants
{
    public const string TextSpeed = "TEXT_SPEED";
}
