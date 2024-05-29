using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe")]
public class RecipeScriptableObject : ScriptableObject
{
    public string DisplayName;
    [TextArea(10, 1)]
    public string Description;
    public Sprite Image;

    public int BaseValue;

    public RecipeType RecipeType;
    public IngredientScriptableObject[] SpecificIngredients;
    public IngredientCategory[] FlexibleIngredients;

    [Tooltip("If we need to adjust text based on a specific ingredient, assign ingredient type here and then reference it by index in text fields")]
    public IngredientCategory[] TextParameters;
}

public enum RecipeType { StandMixerOven, StockPot, Oven, Pan, KnifeCuttingBoard}
