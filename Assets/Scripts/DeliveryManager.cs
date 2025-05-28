using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;


    private void Awake()
    {
        Instance = this;
        waitingRecipeList = new List<RecipeSO>();
    }
    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        { 
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                UnityEngine.Debug.Log(waitingRecipeSO.recipeName);
                waitingRecipeList.Add(waitingRecipeSO);
            }
            
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeList.Count; i++)
        { 
            RecipeSO waitingRecipeSO = waitingRecipeList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool plateContentMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        { 
                            ingredientFound=true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    { 
                        plateContentMatchesRecipe=false;
                    }
                }

                if (plateContentMatchesRecipe)
                {
                    UnityEngine.Debug.Log("deliverd the right recipe");
                    waitingRecipeList.RemoveAt(i);
                    return;
                }
            
            }
        }
        UnityEngine.Debug.Log("player did not deliver the right recipe");
    }
}
