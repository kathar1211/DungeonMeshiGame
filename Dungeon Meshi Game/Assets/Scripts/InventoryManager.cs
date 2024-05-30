using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private Dictionary<string, InventoryEntry> Inventory;

    public struct InventoryEntry
    {
        public string Name;
        public IngredientScriptableObject Ingredient;
        public int Count;
    }

    // Start is called before the first frame update
    void Awake()
    {
        Inventory = new Dictionary<string, InventoryEntry>();
        //todo: load inventory from save if it exists

        //temp while testing
        GrantAllIngredients();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<IngredientScriptableObject> GetIngredientsInInventory()
    {
        List<IngredientScriptableObject> toReturn = new List<IngredientScriptableObject>();

        foreach (string key in Inventory.Keys)
        {
            if (Inventory[key].Count > 0)
            {
                toReturn.Add(Inventory[key].Ingredient);
            }
        }

        return toReturn;
    }

    public List<IngredientScriptableObject> GetIngredientsInInventoryForCategory(CategoryForSorting category)
    {
        List<IngredientScriptableObject> toReturn = new List<IngredientScriptableObject>();

        foreach (string key in Inventory.Keys)
        {
            if (Inventory[key].Count > 0 && Inventory[key].Ingredient.SortingCategory == category)
            {
                toReturn.Add(Inventory[key].Ingredient);
            }
        }

        return toReturn;
    }

    //give the player one of every ingredient we have defined
    void GrantAllIngredients()
    {
        List<IngredientScriptableObject> ingredientEntries = new List<IngredientScriptableObject>();
        ingredientEntries = Resources.LoadAll<IngredientScriptableObject>("Data/Ingredients").ToList();


        foreach(IngredientScriptableObject entry in ingredientEntries.OfType<IngredientScriptableObject>())
        {
            GrantIngredient(entry);
        }
    }

    //returns whether the given ingredient is present in the inventory
    public bool HasIngredient(IngredientScriptableObject ingredient)
    {
        return Inventory.ContainsKey(ingredient.DisplayName);
    }

    //returns how many instances of the ingredient the player has
    public int GetIngredientCount(IngredientScriptableObject ingredient)
    {
        if (Inventory.ContainsKey(ingredient.DisplayName))
        {
            InventoryEntry invEntry = Inventory[ingredient.DisplayName];
            return invEntry.Count;
        }
        else
        {
            return 0;
        }
    }

    //add an ingredient to the inventory. optional parameter for amount
    public void GrantIngredient(IngredientScriptableObject ingredient, int count = 1)
    {
        if (Inventory.ContainsKey(ingredient.DisplayName))
        {
            InventoryEntry invEntry = Inventory[ingredient.DisplayName];
            invEntry.Count += count;
        }
        else
        {
            InventoryEntry invEntry = new InventoryEntry();
            invEntry.Name = ingredient.DisplayName;
            invEntry.Ingredient = ingredient;
            invEntry.Count = count;
            Inventory.Add(ingredient.DisplayName, invEntry);
        }
    }

}
