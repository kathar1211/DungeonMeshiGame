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
            if (recipe == DefaultRecipe) { continue; } //dont want false positives: you can always make a failed dish
            if (RecipeCanSucceed(recipe, ingredients)) { return recipe; }
        }

        return DefaultRecipe;
    }

    public RecipeScriptableObject GetRecipeForIngredients(List<IngredientScriptableObject> ingredients, RecipeType type)
    {
        //todo: if something from the fail list is present, this is a failure

        foreach (RecipeScriptableObject recipe in AllRecipes)
        {
            if (recipe == DefaultRecipe) { continue; } //dont want false positives: you can always make a failed dish

            if (recipe.RecipeType != type) { continue; }

            if (RecipeCanSucceed(recipe, ingredients)) { return recipe; }
        }

        return DefaultRecipe;
    }

    //returns true if this recipe's requirements are met by this list of ingredients
   private bool RecipeCanSucceed(RecipeScriptableObject recipe, List<IngredientScriptableObject> ingredients)
    {

        foreach (IngredientScriptableObject specificIngredient in recipe.SpecificIngredients)
        {
            IngredientScriptableObject ingredientMatch = null;
            foreach (IngredientScriptableObject ingredient in ingredients)
            {
                if (ingredient.DisplayName == specificIngredient.DisplayName)
                {
                    ingredientMatch = ingredient;
                    break;
                }
            }

            //couldnt find an ingredient to satisfy this requirement: fail
            if (ingredientMatch == null)
            {
                return false;
            }
            else
            {
                //each ingredient can only be used to satisfy one requirement
                //remove this ingredient to prevent it from double dipping
                ingredients.Remove(ingredientMatch);
            }
        }

        //now do it again w the more flexible requirements
        foreach(IngredientCategory nonSpecificIngredient in recipe.FlexibleIngredients)
        {
            IngredientScriptableObject ingredientMatch = ingredients.DefaultIfEmpty(null).First(x => x.Type == nonSpecificIngredient);
            if (ingredientMatch == null) { return false; }
            else { ingredients.Remove(ingredientMatch); }
        }

        //we made it to the end without finding any unmet conditions
        return true;
    }
}
