using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSelectionBasket : MonoBehaviour
{
    private List<IngredientScriptableObject> selectedIngredients;

    public RecipeManager RecipeManager;
    public Transform IngredientAttachTarget;
    public RectTransform DragArea;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsGameobjectInDragArea(GameObject obj)
    {
        Vector3 dif = obj.transform.position - DragArea.transform.position;
        float xDif = Mathf.Abs(dif.x);
        float yDif = Mathf.Abs(dif.y);
        return (xDif <= DragArea.rect.width / 2f && yDif <= DragArea.rect.height / 2f);
    }

    public void AddIngredient(IngredientScriptableObject ingredient, GameObject icon)
    {
        if (selectedIngredients == null) { selectedIngredients = new List<IngredientScriptableObject>(); }

        selectedIngredients.Add(ingredient);

        icon.transform.SetParent(IngredientAttachTarget);

        //maybe add some new event listeners/handlers here for dragging these around
    }

    public void ClearIngredients()
    {
        foreach (Transform child in IngredientAttachTarget)
        {
            Destroy(child.gameObject);
        }

        selectedIngredients.Clear();
    }

    public void GetRecipeForSelectedIngredients()
    {
        RecipeScriptableObject recipe = RecipeManager.GetRecipeForIngredients(selectedIngredients);
        Debug.Log(recipe.DisplayName);
    }

}
