using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnDeliveryCorrect;
    public event EventHandler OnDeliveryWrong;
    public event EventHandler OnDeliverySpawned;
    public event EventHandler OnDeliveryCompleted;
    public static DeliveryManager Instance { get; set; }
    [SerializeField] private RecipeListSO recipeListSO;
    
    private List<RecipeSO> waitingDeliveryList;
    private float deliveryTimer;
    private float deliveryTimerMax = 4f;
    private int maxDeliveryCount = 4;
    private int succesDeliveryCount;
    private void Awake()
    {
        Instance = this;
        waitingDeliveryList = new List<RecipeSO>();
    }
    private void Update()
    {
        deliveryTimer -= Time.deltaTime;
        if (deliveryTimer <= 0)
        {
            deliveryTimer = deliveryTimerMax;
            if (GameManager.Instance.IsGamePlaying() && waitingDeliveryList.Count < maxDeliveryCount)
            {
                RecipeSO waitingRecipeSO = recipeListSO.Recipes[UnityEngine.Random.Range(0,recipeListSO.Recipes.Count)]; 
                waitingDeliveryList.Add(waitingRecipeSO);
                OnDeliverySpawned?.Invoke(this, EventArgs.Empty);
            }
            
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingDeliveryList.Count; i++)
        {
            //tüm recipeleri kontrol ediyoruz
            RecipeSO waitingRecipSO = waitingDeliveryList[i];
            if (waitingRecipSO.kitchenObjectSOList.Count == plateKitchenObject.GetIngredients().Count)
            {
                // they has same amount ingredients
                bool ingredientsMatch = true;
                foreach (KitchenObjectSO waitingRecipeIngredient in waitingRecipSO.kitchenObjectSOList)
                {
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateIngredient in plateKitchenObject.GetIngredients())
                    {
                        if (waitingRecipeIngredient == plateIngredient)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        ingredientsMatch = false;
                    }

                }
                if (ingredientsMatch)
                {
                 succesDeliveryCount++;
                    waitingDeliveryList.RemoveAt(i);
                    OnDeliveryCompleted?.Invoke(this, EventArgs.Empty);
                    OnDeliveryCorrect?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
            
        }
        // no match
        OnDeliveryWrong?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeList()
    {
        return waitingDeliveryList;
    }
    public int GetSuccesDeliveryCount()
    {
        return succesDeliveryCount;
    }
}
