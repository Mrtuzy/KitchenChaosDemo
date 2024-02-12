using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventAargs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] private FriedRecipeSO[] friedRecipeSOArray;
    [SerializeField] private BurnedRecipeSO[] burnedRecipeSOArray;
    private float friedTimer;
    private float burningTimer;
    private FriedRecipeSO friedRecipeSO;
    private State state;

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
                    friedTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventAargs
                    { progressNormalized = friedTimer / friedRecipeSO.friedProgressMax });
                    if (friedTimer > friedRecipeSO.friedProgressMax)
                    {
                        friedTimer = 0;
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(friedRecipeSO.output, this);
                        state = State.Fried;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                    }
                   

                    break;
            case State.Fried:
                    BurnedRecipeSO burnedRecipeSO = GetBurnedRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());
                    burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventAargs
                    { progressNormalized = burningTimer / burnedRecipeSO.burnedProgressMax });
                    if (burningTimer > burnedRecipeSO.burnedProgressMax)
                    {
                        burningTimer = 0;
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burnedRecipeSO.output, this);
                        state= State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventAargs
                        { progressNormalized =0});
                    }
                    
                   
                    break;
            case State.Burned:
                break;
            default:
                break;
        }
        
            
        }
        else
        {
            friedTimer = 0;
            burningTimer = 0;
        }
    }
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // Counter hasn't object
            if (player.HasKitchenObject())
            {
                // Player has object
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    friedRecipeSO = GetFriedRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{ state = state});
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventAargs
                    { progressNormalized = friedTimer / friedRecipeSO.friedProgressMax });
                }

            }
            else
            {

            }
        }
        else
        {
            // Counter has object
            if (player.HasKitchenObject())
            {
                // Player has object
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // Player has plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                    state = State.Idle;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventAargs
                    { progressNormalized = 0 });
                }
            }
            else
            {
                // Player hasn't object
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventAargs
                { progressNormalized = 0});
            }
        }

    }
    private FriedRecipeSO GetFriedRecipeSOFromInput(KitchenObjectSO kitchenObjectInput)
    {
        foreach (FriedRecipeSO recipe in friedRecipeSOArray)
        {
            if (recipe.input == kitchenObjectInput)
            {
                return recipe;
            }
        }
        return null;
    }
    private BurnedRecipeSO GetBurnedRecipeSOFromInput(KitchenObjectSO kitchenObjectInput)
    {
        foreach (BurnedRecipeSO recipe in burnedRecipeSOArray)
        {
            if (recipe.input == kitchenObjectInput)
            {
                return recipe;
            }
        }
        return null;
    }
    private KitchenObjectSO GetOutputFromInput(KitchenObjectSO kitchenObjectInput)
    {
        FriedRecipeSO friedRecipeSO = GetFriedRecipeSOFromInput(kitchenObjectInput);
        if (friedRecipeSO != null)
        {
            return friedRecipeSO.output;
        }
        return null;
    }
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FriedRecipeSO friedRecipeSO = GetFriedRecipeSOFromInput(inputKitchenObjectSO);

        return friedRecipeSO != null;
    }
    public bool IsFried()
    {
        return state == State.Fried;
    }
}
