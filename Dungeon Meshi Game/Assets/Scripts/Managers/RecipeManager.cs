using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    private List<RecipeScriptableObject> AllRecipes;
    private List<NoListScriptableObject> AllFailLists;

    [SerializeField] RecipeScriptableObject DefaultRecipe; //what do we return if a valid recipe is not found

    // Start is called before the first frame update
    void Awake()
    {
        GetAllRecipes();
        GetAllFailLists();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetAllRecipes()
    {
        AllRecipes = Resources.LoadAll<RecipeScriptableObject>("Data/Recipes").ToList();
    }

    void GetAllFailLists()
    {
        AllFailLists = Resources.LoadAll<NoListScriptableObject>("Data/FailureLists").ToList();
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
        //if something from the fail list is present, this is a failure
        foreach (NoListScriptableObject noList in AllFailLists)
        {
            if (noList.RecipeType != type) { continue; }

            if (HasFailListIngredients(noList, ingredients)) { return DefaultRecipe; }
        }

        //now go through our recipes looking for a positive match
        foreach (RecipeScriptableObject recipe in AllRecipes)
        {
            if (recipe == DefaultRecipe) { continue; } //dont want false positives: you can always make a failed dish

            if (recipe.RecipeType != type) { continue; }

            if (RecipeCanSucceed(recipe, ingredients)) { return recipe; }
        }

        //no match found
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
            IngredientScriptableObject ingredientMatch = null;
            foreach (IngredientScriptableObject ingredient in ingredients)
            {
                if (ingredient.Type == nonSpecificIngredient)
                {
                    ingredientMatch = ingredient;
                    break;
                }
            }
            if (ingredientMatch == null) { return false; }
            else { ingredients.Remove(ingredientMatch); }
        }

        //we made it to the end without finding any unmet conditions
        return true;
   }

    private bool HasFailListIngredients(NoListScriptableObject failList, List<IngredientScriptableObject> ingredients)
    {
        foreach (IngredientScriptableObject specificIngredient in failList.SpecificIngredients)
        {
            foreach (IngredientScriptableObject ingredient in ingredients)
            {
                if (ingredient.DisplayName == specificIngredient.DisplayName)
                {
                    return true;
                }
            }

        }

        foreach (IngredientCategory flexibleIngredient in failList.FlexibleIngredients)
        {
            foreach (IngredientScriptableObject ingredient in ingredients)
            {
                if (ingredient.Type == flexibleIngredient)
                {
                    return true;
                }
            }

        }

        //we made it to the end of the list without finding a match
        return false;
    }
}
