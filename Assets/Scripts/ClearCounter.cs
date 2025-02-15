using Unity.Collections;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;

    public void Interact(Player player)
    {
        KitchenObject playerKitchenObject = player.GetComponentInChildren<KitchenObject>();
        KitchenObject[] playerKitchenObjects = player.GetComponentsInChildren<KitchenObject>();
        KitchenObject[] kitchenObjectOnCounter = this.GetComponentsInChildren<KitchenObject>();

        // Player is holding a single object (not a plate)
        if (playerKitchenObjects.Length == 1 && playerKitchenObject != null && playerKitchenObject.GetKitchenObjectname() != "Plate")
        {
            if (!this.HasKitchenObject()) // Counter must be empty
            {
                Debug.Log("Placing item on counter!");
                playerKitchenObject.transform.SetParent(counterTopPoint);
                playerKitchenObject.transform.localPosition = Vector3.zero;
            }
        }
        // Player is holding multiple objects and one of them is a plate
        else if (playerKitchenObjects.Length > 1 && playerKitchenObjects.Any(obj => obj.GetKitchenObjectname() == "Plate"))
        {
            if (!this.HasKitchenObject()) // Counter must be empty
            {
                Debug.Log("Placing multiple items on counter!");
                foreach (KitchenObject obj in playerKitchenObjects)
                {
                    obj.transform.SetParent(counterTopPoint);
                    obj.transform.localPosition = Vector3.zero;
                }
            }
        }
        // Player has no items and wants to pick up an object
        else if (playerKitchenObjects.Length == 0 && kitchenObjectOnCounter != null)
        {
            Debug.Log("Picking up an item from the counter!");
            foreach(KitchenObject obj in kitchenObjectOnCounter)
            {
               obj.transform.SetParent(player.GetKitchenObjectFollowTransform());
                obj.transform.localPosition = Vector3.zero;
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
