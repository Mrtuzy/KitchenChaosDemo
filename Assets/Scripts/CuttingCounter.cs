using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CuttingCounter : BaseCounter, IHasProgress
{
    public static event EventHandler OnAnyCut;
    new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }

    public event EventHandler<IHasProgress.OnProgressChangedEventAargs> OnProgressChanged;
    public event EventHandler OnCut;
    public class OnProgressChangedEventAargs : EventArgs
    {
        public float progressNormalized;
    }

    [SerializeField] private CuttingRecipeSO[] cuttingRecipe;
    private int cuttingProgress;

    private CuttingRecipeSO GetCuttingRecipeSOFromInput(KitchenObjectSO kitchenObjectInput)
    {
        foreach (CuttingRecipeSO recipe in cuttingRecipe)
        {
            if (recipe.input == kitchenObjectInput)
            {
                return recipe;
            }
        }
        return null;
    }
    private KitchenObjectSO GetOutputFromInput(KitchenObjectSO kitchenObjectInput)
    {
       CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromInput(kitchenObjectInput);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        return null;
    }
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromInput(inputKitchenObjectSO);
       
        return cuttingRecipeSO != null;
    }
    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            
            
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());
            cuttingProgress++;
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventAargs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });
            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax )
            {
                KitchenObjectSO outputKitchenObject = GetOutputFromInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObject, this);
            }
            
        }
    }
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // Counter hasn't object
            if (player.HasKitchenObject())
            {
                // Player has object
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventAargs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });

                }

            }
            else
            {

            }
        }
        else
        {
            // Counter has object
            if (player.HasKitchenObject())
            {
                //player has object
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // Player has plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }

                }
            }
            else
            {
                // Player hasn't object
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

    }
}
