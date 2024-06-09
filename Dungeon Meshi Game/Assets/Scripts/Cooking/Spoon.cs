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
    private float StirProgress; //how much have we stirred so far

    public Image meter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag()
    {
        if (CanStir) { IsBeingDragged = true; }
    }

    public void OnDrag()
    {
        //start by just following x position of the mouse
        Vector3 prevPosition = new Vector3();
        //this.transform.position.co
        CanvasUtils.SetUIObjectToMousePosition(this.gameObject);
    }


    public void OnDragEnd()
    {
        IsBeingDragged = false;
    }

    public void Updatemeter()
    {
        meter.fillAmount = StirProgress / StirAmount;
    }
}
