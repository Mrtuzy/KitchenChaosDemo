using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);

    }
    private void Start()
    {
        DeliveryManager.Instance.OnDeliveryCompleted += DeliveryManager_OnDeliveryCompleted;
        DeliveryManager.Instance.OnDeliverySpawned += DeliveryManager_OnDeliverySpawned;
        UpdateVisual();
    }

    private void DeliveryManager_OnDeliverySpawned(object sender, System.EventArgs e)
    {
       UpdateVisual();
    }

    private void DeliveryManager_OnDeliveryCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (RecipeSO waitingRecipe in DeliveryManager.Instance.GetWaitingRecipeList())
        {
            Transform recipeTemplateTransform = Instantiate(recipeTemplate, container);
            recipeTemplateTransform.gameObject.SetActive(true);
            recipeTemplateTransform.GetComponent<DeliveryManagerSingelUI>().SetRecipeSO(waitingRecipe);
        }

    }
}
