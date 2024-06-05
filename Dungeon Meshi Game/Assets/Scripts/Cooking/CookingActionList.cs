using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingActionList : MonoBehaviour
{
    public enum CookingActionType { Stir, AddIngredient} //more of these to come

    public enum CookingResult { None, Good, Bad} //maybe also more of these to come

    public class CookingAction //fuck it, im making this a class just so it can be nullable
    {
        public CookingActionType Type; //what is the action you need to do
        public float TimingWindow; //how long do you have in seconds to do this action
        public CookingResult Result; //is this action done and if so how did you do
    }

    public CookingAction ActiveAction = null;
    public float TimeBetweenActions = 0;

    private float timer;
    private bool paused = true;
    private int actionIndex = 0;

    public void StartCooking()
    {
        timer = 0;
        actionIndex = 0;
        paused = false;
    }

    public void PauseCooking(bool pause)
    {
        paused = pause;
    }

    private void Update()
    {
        if (!paused)
        {
            timer += Time.deltaTime;

            //is our action window over
            if (ActiveAction != null && timer > ActiveAction.TimingWindow)
            {
                //kitchen utensil class is in charge of letting us know if these are completed successfully
                //if that didnt happen, it means this wasnt completed
                if (ActiveAction.Result == CookingResult.None) { ActiveAction.Result = CookingResult.Bad; }
            }

            //are we ready for the next action
        }
    }

    public void MarkActionComplete()
    {

    }

    public void AdvanceCookingAction()
    {

    }
}
