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
        }
        
        Instance = this;
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

    public static bool IsPointInsideRect(Vector3 pointWorldPosition, RectTransform containingRect, bool debug = false)
    {
        Vector3[] worldPosContainingCorners = new Vector3[4];
        containingRect.GetWorldCorners(worldPosContainingCorners); //order of these corners is bottom left, top left, top right, bottom right

        if (debug)
        {
            Debug.Log("point position: " + pointWorldPosition.ToString() + " \n bottom left position: " + worldPosContainingCorners[0].ToString()
                + " \n top left position: " + worldPosContainingCorners[1].ToString() + " \n top right position: " + worldPosContainingCorners[2].ToString()
                + " \n bottom right position: " + worldPosContainingCorners[3].ToString());
        }

        if (pointWorldPosition.x > worldPosContainingCorners[0].x && pointWorldPosition.x < worldPosContainingCorners[2].x
            && pointWorldPosition.y > worldPosContainingCorners[0].y && pointWorldPosition.y < worldPosContainingCorners[2].y)
        {
            return true;
        }

        return false;
    }


}
