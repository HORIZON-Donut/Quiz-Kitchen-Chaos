using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kitchenobject;

    public string GetKitchenObjectname() {
        return kitchenobject.objectname;
    }
}
