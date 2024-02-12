using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
      public KitchenObjectSO kitchenObjectSO;
    }

    private List<KitchenObjectSO> ingredients;
    [SerializeField] private List<KitchenObjectSO> validIngredients;
    private void Start()
    {
        ingredients = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO ingredient)
    {
         
        if (!ingredients.Contains(ingredient) && validIngredients.Contains(ingredient))
        {
            ingredients.Add(ingredient);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs()
            {
                kitchenObjectSO = ingredient
            });
            return true;
        }
        return false;
    }
    public List<KitchenObjectSO> GetIngredients()
    {
        return ingredients;
    }
}
