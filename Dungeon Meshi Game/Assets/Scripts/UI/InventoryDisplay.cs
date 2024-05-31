using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InventoryDisplay : MonoBehaviour
{
    public InventoryDisplayEntry InventoryEntryPrefab;
    public Transform EntryAttachTarget;

    private InventoryManager PlayerInventory;

    public Canvas ParentCanvas;

    private int minEntries = 16; //show empty boxes if we have fewer entries than this

    public enum Tab { FruitVeg, AnimalProduct, Meat, Grain, Misc}
    private Tab currentTab;

    public Image BackgroundImage;

    public Sprite FruitVegTabSelected;
    public Sprite AnimalProductTabSelected;
    public Sprite MeatTabSelected;
    public Sprite GrainTabSelected;
    public Sprite MiscTabSelected;

    public TextMeshProUGUI SelectedLabel;

    // Start is called before the first frame update
    void Start()
    {
        //todo: not this
        PlayerInventory = FindObjectOfType<InventoryManager>();

        currentTab = Tab.FruitVeg;

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFruitVegTabSelected()
    {
        currentTab = Tab.FruitVeg;
        UpdateUI();
    }

    public void OnAnimalProductTabSelected()
    {
        currentTab = Tab.AnimalProduct;
        UpdateUI();
    }
    public void OnMeatTabSelected()
    {
        currentTab = Tab.Meat;
        UpdateUI();
    }

    public void OnGrainTabSelected()
    {
        currentTab = Tab.Grain;
        UpdateUI();
    }
    public void OnMiscTabSelected()
    {
        currentTab = Tab.Misc;
        UpdateUI();
    }

    private void UpdateUI()
    {
        //kill the old entries
        foreach(Transform child in EntryAttachTarget)
        {
            Destroy(child.gameObject);
        }

        List<IngredientScriptableObject> displayItems = new List<IngredientScriptableObject>();

        //display depending on current tab
        switch (currentTab)
        {
            case Tab.FruitVeg:
                BackgroundImage.sprite = FruitVegTabSelected;
                displayItems = PlayerInventory.GetIngredientsInInventoryForCategory(CategoryForSorting.Vegan);
                break;
            case Tab.AnimalProduct:
                BackgroundImage.sprite = AnimalProductTabSelected;
                displayItems = PlayerInventory.GetIngredientsInInventoryForCategory(CategoryForSorting.Vegetarian);
                break;
            case Tab.Meat:
                BackgroundImage.sprite = MeatTabSelected;
                displayItems = PlayerInventory.GetIngredientsInInventoryForCategory(CategoryForSorting.Meat);
                break;
            case Tab.Grain:
                BackgroundImage.sprite = GrainTabSelected;
                displayItems = PlayerInventory.GetIngredientsInInventoryForCategory(CategoryForSorting.Grain);
                break;
            case Tab.Misc:
                BackgroundImage.sprite = MiscTabSelected;
                displayItems = PlayerInventory.GetIngredientsInInventoryForCategory(CategoryForSorting.Misc);
                break;
            default:
                break;
        }


        //create a display entry for each listing
        foreach (IngredientScriptableObject ingredient in displayItems)
        {
            InventoryDisplayEntry newEntry = Instantiate<InventoryDisplayEntry>(InventoryEntryPrefab, EntryAttachTarget);
            newEntry.SetData(ingredient);
            newEntry.SetParents(this, ParentCanvas);
        }

        //keep going with empty entries if we need to fill out the page
        if (displayItems.Count < minEntries)
        {
            for (int i = displayItems.Count; i < minEntries; i++)
            {
                InventoryDisplayEntry newEntry = Instantiate<InventoryDisplayEntry>(InventoryEntryPrefab, EntryAttachTarget);
                newEntry.SetData(null);
                newEntry.SetParents(this, ParentCanvas);
            }
        }
        
    }

    public void SetSelectedIngredient(string name)
    {
        if (name == null) { SelectedLabel.text = ""; }
        else { SelectedLabel.text = name; }
    }
    
}
