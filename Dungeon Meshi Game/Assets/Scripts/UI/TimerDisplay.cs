using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerDisplay : MonoBehaviour  
{
    public Image FillPercent;
    public bool Paused;

    //times are in seconds
    private float timer; 
    private float timeWhenTimesUp;

    private Action TimesUpCallback; 

    // Start is called before the first frame update
    void Start()
    {
        Paused = true;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Paused)
        {
            timer += Time.deltaTime;

            FillPercent.fillAmount = timer / timeWhenTimesUp;

            if (timer >= timeWhenTimesUp)
            {
                TimesUpCallback?.Invoke();
                StopTimer();
            }
        }
    }

    public void SetMaxTime(float timeInSeconds)
    {
        timeWhenTimesUp = timeInSeconds;
    }

    public void StartTimer()
    {
        gameObject.SetActive(true);
        timer = 0;
        Paused = false;
    }

    public void StopTimer()
    {
        gameObject.SetActive(false);
        Paused = true;
    }

    public void UnPauseTimer()
    {
        Paused = false;
    }

    //if we need to do something when time's up, set it here
    public void SetTimesUpCallback(Action onTimerFinished)
    {
        TimesUpCallback += onTimerFinished;
    }

    public void ClearTimesUpCallback()
    {
        TimesUpCallback = null;
    }
}
