using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedobject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(PlayerController player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchesObject(kitchenObjectSO, player);

            OnPlayerGrabbedobject?.Invoke(this, EventArgs.Empty);
        }
       
    }

  
}
