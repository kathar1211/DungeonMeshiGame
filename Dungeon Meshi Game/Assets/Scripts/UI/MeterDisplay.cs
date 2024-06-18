using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterDisplay : MonoBehaviour
{
    public Image FillPercent;
    public bool Paused;

    private float currentAmount;
    private float maxAmount;

    private Action MeterFilledCallback;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetAndShowMeter()
    {
        currentAmount = 0;
        this.gameObject.SetActive(true);
    }

    public void SetMaxAmount(float amount)
    {
        maxAmount = amount;
    }

    //if we need to do something when time's up, set it here
    public void SetMeterFilledCallback(Action onMeterFilled)
    {
        MeterFilledCallback += onMeterFilled;
    }

    public void ClearMeterFilledCallback()
    {
        MeterFilledCallback = null;
    }

    public void AddToMeter(float amount)
    {
        currentAmount += amount;

        FillPercent.fillAmount = currentAmount / maxAmount;

        if (currentAmount >= maxAmount)
        {
            MeterFilledCallback?.Invoke();
        }
    }
}
