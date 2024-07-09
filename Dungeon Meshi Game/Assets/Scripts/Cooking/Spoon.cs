using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spoon : MonoBehaviour
{
    public RectTransform LeftBoundary;
    public RectTransform RightBoundary;

    public float MaxRotation;

    public bool CanStir = true; //dont let the user stir if its not time to do so

    private bool IsBeingDragged = false;

    public float StirAmount; //how much do we need to stir to fill the meter
    public float TimeToComplete;
    private float StirProgress; //how much have we stirred so far

    public MeterDisplay meter;
    public TimerDisplay timer;

    public float stirDistanceToMeterConversion;

    // Start is called before the first frame update
    void Start()
    {
        StartSpoonTask(StirAmount, TimeToComplete);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void StartSpoonTask(float amountToStir, float timeToStir)
    {
        CanStir = true;

        meter.SetMaxAmount(amountToStir);
        meter.ResetAndShowMeter();

        timer.SetMaxTime(timeToStir);
        timer.StartTimer();

        //we'll also want to show some arrows at this point to indicate to the user what they should do
    }

    public void OnBeginDrag()
    {
        if (CanStir) { IsBeingDragged = true; }
    }

    public void OnDrag()
    {
        //start by just following x position of the mouse
        Vector3 prevPosition = this.transform.localPosition;
        CanvasUtils.SetUIObjectToMousePosition(this.gameObject);

        //lock y and z
        this.transform.localPosition = new Vector3(transform.localPosition.x, prevPosition.y, prevPosition.z);

        //keep x in bounds
        if (CanvasUtils.IsPointInsideRect(transform.position, LeftBoundary))
        {
            Vector3[] result = new Vector3[4];
            LeftBoundary.GetWorldCorners(result);
            transform.position = new Vector3(result[2].x, transform.position.y, transform.position.z);
        }
        if (CanvasUtils.IsPointInsideRect(transform.position, RightBoundary))
        {
            Vector3[] result = new Vector3[4];
            RightBoundary.GetWorldCorners(result);
            transform.position = new Vector3(result[0].x, transform.position.y, transform.position.z);
        }

        //update meter as we move
        float stirDistance = Mathf.Abs(prevPosition.x - transform.localPosition.x);
        meter.AddToMeter(stirDistance * stirDistanceToMeterConversion);
        
    }

    public void OnDragEnd()
    {
        IsBeingDragged = false;
    }

}
