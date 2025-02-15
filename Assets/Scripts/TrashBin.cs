using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;


    public void Interact(Player player)
    {
        if (player.HasKitchenObject().Length > 0)
        {
            Debug.Log("Has item");
            KitchenObject playerKitchenObject = player.GetComponentInChildren<KitchenObject>();

            if (playerKitchenObject !=null && !this.HasKitchenObject())
            {
                Debug.Log("Place Item!");
                playerKitchenObject.transform.SetParent(this.transform);
                playerKitchenObject.transform.parent = counterTopPoint;
                playerKitchenObject.transform.localPosition = Vector3.zero;
            }
        }

    }
    public Transform GetKitchenObjectFollowTransform() 
    {
        return counterTopPoint;
    }
    public bool HasKitchenObject()
    {
        KitchenObject playerKitchenObject = this.GetComponentInChildren<KitchenObject>();
        return playerKitchenObject != null;
    }

}
