using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplayEntry : MonoBehaviour
{
    public InventoryDisplay ParentDisplay;
    public Canvas ParentCanvas;
    public IngredientSelectionBasket Basket;
    public TextMeshProUGUI textLabel;
    public Image spriteLabel;
    public Image backgroundImage;

    public Color selectedColor;
    public Color emptyBoxColor;
    public Color defaultColor;

    private IngredientScriptableObject ingredient;

    private GameObject dragCopy; //copy of the image that gets dragged around

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

    public void SetParents(InventoryDisplay displayParent, Canvas canvasParent, IngredientSelectionBasket basket)
    {
        ParentCanvas = canvasParent;
        ParentDisplay = displayParent;
        Basket = basket;
    }

    public void OnHover()
    {
        if (ingredient == null) { return; }

        ParentDisplay.SetSelectedIngredient(ingredient.DisplayName);
        backgroundImage.color = selectedColor;

    }

    public void OnExitHover()
    {
        if (ingredient == null) { return; }

        ParentDisplay.SetSelectedIngredient(null);
        backgroundImage.color = defaultColor;

    }

    public void OnClickAndDrag()
    {
        if (ingredient == null) { return; }

        if (spriteLabel.gameObject.activeSelf)
        {
            dragCopy = Instantiate(spriteLabel.gameObject, ParentCanvas.transform);
        }
        else
        {
            dragCopy = Instantiate(textLabel.gameObject, ParentCanvas.transform);
        }
    }

    public void OnDrag()
    {
        if (ingredient == null) { return; }
        if (dragCopy == null) { return; }

        //update the position of the copy of our sprite so it follows the mouse
        /* Vector2 movePos;
         RectTransformUtility.ScreenPointToLocalPointInRectangle(
             ParentCanvas.transform as RectTransform,
             Input.mousePosition, ParentCanvas.worldCamera,
             out movePos);
         dragCopy.transform.position = ParentCanvas.transform.TransformPoint(movePos);*/
        CanvasUtils.SetUIObjectToMousePosition(dragCopy);
    }

    public void OnEndDrag()
    {
        if (ingredient == null) { return; }

        //unselect this ingredient
        OnExitHover();

        // detect whether to put da ingredient in da basket
        if (Basket.IsGameobjectInDragArea(dragCopy))
        {
            Basket.AddIngredient(ingredient, dragCopy);

            //sever our connection to this gameobject
            dragCopy = null;
        }
        else
        {
            //destroy our hovering icon
            Destroy(dragCopy.gameObject);
            dragCopy = null;
        }
        
    }
}
