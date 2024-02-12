using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter 
{

 
    

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // Counter hasn't object
            if (player.HasKitchenObject())
            {
                // Player has object
                player.GetKitchenObject().SetKitchenObjectParent(this);
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
                // Player has object
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // Player has plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }

                }
                else
                {
                    // Player has object except plate
                    if (GetKitchenObject().TryGetPlate(out  plateKitchenObject))
                    {
                        // Counter has plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }

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
