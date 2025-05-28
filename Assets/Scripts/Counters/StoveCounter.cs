using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress
{

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    private enum State { 
        Idle,
        Frying,
        Fried,
        Burned,
    
    }

    [SerializeField] private FryingRecipesSO[] fryingRecipesSOArray;
    [SerializeField] private BurningRecipesSO[] burningRecipesSOArray;

    private State state;
    private float fryingTimer;
    private float burningTimer;
    private FryingRecipesSO fryingRecipesSO;
    private BurningRecipesSO burningRecipesSO;

    public event EventHandler OnFry;



    private void Start()
    {
        state = State.Idle;
    }
    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = fryingTimer / fryingRecipesSO.fryingTimerMax });

                    if (fryingTimer > fryingRecipesSO.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchesObject(fryingRecipesSO.output, this);
                        
                        state = State.Fried;
                        burningTimer = 0f;

                        burningRecipesSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        
                    }
                    
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = burningTimer / burningRecipesSO.burningTimerMax });

                    if (burningTimer > burningRecipesSO.burningTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchesObject(burningRecipesSO.output, this);
                        UnityEngine.Debug.Log("burned");
                        state = State.Burned;

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });
                    }


                    break;
                case State.Burned:
                    break;
            }
            
        }
    }

    public override void Interact(PlayerController player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasrecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    fryingRecipesSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnFry?.Invoke(this, EventArgs.Empty);
                    state = State.Frying;
                    fryingTimer = 0f;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = fryingTimer / fryingRecipesSO.fryingTimerMax });
                    
                }

            }
            else
            {

            }
        }
        else
        {
            if (player.HasKitchenObject())
            {

            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });
            }
        }
    }


    private bool HasrecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipesSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipesSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }


        else
        { return null; }
    }

    private FryingRecipesSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipesSO fryingRecipeSO in fryingRecipesSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }

        }
        return null;
    }
    private BurningRecipesSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipesSO burningRecipeSO in burningRecipesSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }

        }
        return null;
    }
}
