using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
    public static event EventHandler OnAnyPlatePickedUp;

    [SerializeField] private KitchenObjectSO plateKitcheOBjectSO;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int platesSpawnAmount;
    private int platesSpawnAmountMax = 4;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;

        if (spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;

            if (platesSpawnAmount < platesSpawnAmountMax)
            {
                platesSpawnAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(PlayerController player)
    {
        if (!player.HasKitchenObject())
        {
            if (platesSpawnAmount > 0)
            {
                platesSpawnAmount--;

                KitchenObject.SpawnKitchesObject(plateKitcheOBjectSO, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
                OnAnyPlatePickedUp?.Invoke(this, EventArgs.Empty);
            }
        }
    }


}
