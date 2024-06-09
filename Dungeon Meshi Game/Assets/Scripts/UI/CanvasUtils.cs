using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//method used for handling commonly used screen inputs and stuff
[RequireComponent(typeof(Canvas))]
public class CanvasUtils : MonoBehaviour
{
    Canvas MainCanvas;

    public static CanvasUtils Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }
        else
        {
            Instance = this;
        }

        MainCanvas = this.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //set a given game object's position on the canvas to wherever the mouse is
    public static void SetUIObjectToMousePosition(GameObject gameObject)
    {
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            Instance.MainCanvas.transform as RectTransform,
            Input.mousePosition, Instance.MainCanvas.worldCamera,
            out movePos);
        gameObject.transform.position = Instance.MainCanvas.transform.TransformPoint(movePos);
    }


}
