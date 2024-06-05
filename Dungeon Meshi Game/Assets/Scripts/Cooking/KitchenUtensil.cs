using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base kitchen tool that oven pan etc all inherit from
public class KitchenUtensil : MonoBehaviour
{
    protected CookingActionList CookingActions;
    public RecipeScriptableObject Recipe;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //put the actual start stuff in here
    public virtual void Init()
    {
        CreateCookingActions();
    }

    public virtual void CreateCookingActions()
    {

    }

    public void SetRecipe(RecipeScriptableObject recipe)
    {
        Recipe = recipe;
    }
}

