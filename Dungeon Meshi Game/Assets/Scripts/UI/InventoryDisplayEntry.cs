using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplayEntry : MonoBehaviour
{
    public InventoryDisplay ParentDisplay;
    public Canvas ParentCanvas;
    public TextMeshProUGUI textLabel;
    public Image spriteLabel;
    public Image backgroundImage;

    public Color selectedColor;
    public Color emptyBoxColor;
    public Color defaultColor;

    private bool selected = false;
    private IngredientScriptableObject ingredient;

    private Image dragCopy; //copy of the image that gets dragged around

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(IngredientScriptableObject ingredient)
    {
        if (ingredient == null)
        {
            spriteLabel.gameObject.SetActive(false);
            textLabel.gameObject.SetActive(false);
            backgroundImage.color = emptyBoxColor;
        }
        else if (ingredient.Image == null)
        {
            spriteLabel.gameObject.SetActive(false);
            textLabel.gameObject.SetActive(true);
            textLabel.text = ingredient.DisplayName;
            this.ingredient = ingredient;
        }
        else
        {
            spriteLabel.gameObject.SetActive(true);
            textLabel.gameObject.SetActive(false);
            spriteLabel.sprite = ingredient.Image;
            this.ingredient = ingredient;
        }
    }

    public void SetParents(InventoryDisplay displayParent, Canvas canvasParent)
    {
        ParentCanvas = canvasParent;
        ParentDisplay = displayParent;
    }

    public void OnHover()
    {
        if (ingredient == null) { return; }

        ParentDisplay.SetSelectedIngredient(ingredient.DisplayName);
        backgroundImage.color = selectedColor;

        Debug.Log("hover enter");
    }

    public void OnExitHover()
    {
        if (ingredient == null) { return; }

        ParentDisplay.SetSelectedIngredient(null);
        backgroundImage.color = defaultColor;

        Debug.Log("hover exit");
    }

    public void OnClickAndDrag()
    {
        if (ingredient == null) { return; }

        dragCopy = Instantiate(spriteLabel);

        Debug.Log("drag begin");
    }

    public void OnDrag()
    {
        if (ingredient == null) { return; }
        if (dragCopy == null) { return; }

        //update the position of the copy of our sprite so it follows the mouse
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            ParentCanvas.transform as RectTransform,
            Input.mousePosition, ParentCanvas.worldCamera,
            out movePos);
        dragCopy.transform.position = ParentCanvas.transform.TransformPoint(movePos);

    }

    public void OnEndDrag()
    {
        if (ingredient == null) { return; }

        //unselect this ingredient
        OnExitHover();

        //destroy our hovering icon
        Destroy(dragCopy.gameObject);
        dragCopy = null;

        //todo: detect whether to put da ingredient in da basket

        Debug.Log("drag over");
    }
}
