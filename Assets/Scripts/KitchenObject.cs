using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParents kitchenObjectParent;

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }
    public void SetKitchenObjectParent(IKitchenObjectParents kitchenObjectParent)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        this.kitchenObjectParent = kitchenObjectParent;
        if (this.kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("Hata");
        }
        this.kitchenObjectParent.SetKitchenObject(this);
        
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectParents GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
           
        
    }
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParents kitchenObjectParents)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParents);
        return kitchenObject;
    }
}
