using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyTrashed;
    new public static void ResetStaticData()
    {
        OnAnyTrashed = null;
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
            OnAnyTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
