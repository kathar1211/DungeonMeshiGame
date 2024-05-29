using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//list of ingredients that if added to a certain type of recipe will result in a failed dish
[CreateAssetMenu(fileName = "FailList", menuName = "ScriptableObjects/FailList")]
public class NoListScriptableObject : ScriptableObject
{
    public RecipeType RecipeType;
    public IngredientScriptableObject[] SpecificIngredients;
    public IngredientCategory[] FlexibleIngredients;

}
