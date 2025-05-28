using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBinCounter : BaseCounter
{
    public override void Interact(PlayerController player)
    {
        if (player.HasKitchenObject())
        { 
            player.GetKitchenObject().DestroySelf();
        }
    }
}
