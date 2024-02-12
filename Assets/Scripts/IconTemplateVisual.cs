using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconTemplateVisual : MonoBehaviour
{
    [SerializeField] private Image iconImage; 
   public void SetIconVisual(KitchenObjectSO kitchenObjectSO)
    {
        iconImage.sprite = kitchenObjectSO.sprite;
    }
}
