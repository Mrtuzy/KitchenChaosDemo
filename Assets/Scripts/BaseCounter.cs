using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParents
{
    public static event EventHandler OnAnyDropSomthing;
    public static void ResetStaticData()
    {
        OnAnyDropSomthing = null;
    }
    [SerializeField] private Transform topPointOfClearCounter;

    private KitchenObject kitchenObject;
    public virtual void Interact(Player player)
    {

    }
    public virtual void InteractAlternate(Player player)
    {

    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return topPointOfClearCounter;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnAnyDropSomthing?.Invoke(this,EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
