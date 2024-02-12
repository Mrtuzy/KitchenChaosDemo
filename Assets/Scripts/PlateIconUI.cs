using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;
    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
       UpdateIcons();
    }

    private void UpdateIcons()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach ( KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetIngredients())
        {
           Transform iconTransform = Instantiate(iconTemplate,transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<IconTemplateVisual>().SetIconVisual(kitchenObjectSO);
        }
    }
}
