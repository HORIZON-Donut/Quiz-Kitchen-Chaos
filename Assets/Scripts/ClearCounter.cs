using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;


    public void Interact(Player player)
    {
        if (player.HasKitchenObject())
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
        else
        {
            KitchenObject kitchenObject = this.GetComponentInChildren<KitchenObject>();
            Debug.Log("Pick up!");
            if (kitchenObject != null)
            {

                kitchenObject.transform.SetParent(player.transform);
                kitchenObject.transform.parent = player.GetKitchenObjectFollowTransform();
                kitchenObject.transform.localPosition = Vector3.zero;
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
