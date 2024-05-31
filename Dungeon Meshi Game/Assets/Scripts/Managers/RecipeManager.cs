using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    private List<RecipeScriptableObject> AllRecipes;

    [SerializeField] RecipeScriptableObject DefaultRecipe; //what do we return if a valid recipe is not found

    // Start is called before the first frame update
    void Awake()
    {
        GetAllRecipes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetAllRecipes()
    {
        AllRecipes = Resources.LoadAll<RecipeScriptableObject>("Data/Recipes").ToList();
    }

    public RecipeScriptableObject GetRecipeForIngredients(List<IngredientScriptableObject> ingredients)
    {
        foreach (RecipeScriptableObject recipe in AllRecipes)
        {

        }

        return DefaultRecipe;
    }

    public RecipeScriptableObject GetRecipeForIngredients(List<IngredientScriptableObject> ingredients, RecipeType type)
    {
        foreach (RecipeScriptableObject recipe in AllRecipes)
        {
            if (recipe.RecipeType != type) { continue; }

            //todo: if something from the fail list is present, this is a failure
        }

        return DefaultRecipe;
    }

    //returns true if this recipe's requirements are met by this list of ingredients
   /* private bool RecipeCanSucceed(RecipeScriptableObject recipe, List<IngredientScriptableObject> ingredients)
    {

        foreach (IngredientScriptableObject specificIngredient in recipe.SpecificIngredients)
        {
            System.Linq.FirstOrDefault(ingredients, x => x.DisplayName == specificIngredient.DisplayName, null);
        }
    }*/
}
