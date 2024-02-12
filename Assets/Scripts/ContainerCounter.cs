using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectPrefab;
    public EventHandler OnPlayerGrabbedObject;
    public override void Interact(Player player)
    {
        
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectPrefab, player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }



    }
   
}
