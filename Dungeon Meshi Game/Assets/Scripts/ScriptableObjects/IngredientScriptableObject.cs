using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "ScriptableObjects/Ingredient")]
public class IngredientScriptableObject : ScriptableObject
{
    public string DisplayName;
    [TextArea(10, 1)]
    public string Description;
    public Sprite Image;
    public bool IsMonsterIngredient;
    public IngredientCategory Type;
    public int Uses = 1; //for ingredients that we allow the user to use multiple times
    public CategoryForSorting SortingCategory;
}

public enum IngredientCategory { None, Veggies, Fruit, Meat, ChocoVanilla, Eggs, Salt, Cheese, Bones, RiceAndNoodles} //used for recipes where we accept anything from a given category
public enum CategoryForSorting { Vegan, Vegetarian, Meat, Grain, Misc}
