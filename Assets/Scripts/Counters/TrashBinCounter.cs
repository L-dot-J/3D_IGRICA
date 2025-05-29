using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashBinCounter : BaseCounter
{

    public static event EventHandler OnAnyObjectTrashed;

    public override void Interact(PlayerController player)
    {
        if (player.HasKitchenObject())
        { 
            player.GetKitchenObject().DestroySelf();
            OnAnyObjectTrashed?.Invoke(this,EventArgs.Empty);
        }
    }
}
