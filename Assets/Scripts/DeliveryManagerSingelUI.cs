using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Start()
    {
        iconTemplate.gameObject.SetActive(false);
        
    }
    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;
        foreach (Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
           Transform iconTemplateTransform = Instantiate(iconTemplate,iconContainer);
            iconTemplateTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
            iconTemplateTransform.gameObject.SetActive(true); 
        }
    }
}
