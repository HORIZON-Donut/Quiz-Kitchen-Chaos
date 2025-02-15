using Unity.Collections;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;

    public void Interact(Player player)
    {
        string[] listKitchenObject = player.HasKitchenObject();
        if (listKitchenObject.Length == 1 && !listKitchenObject.Contains("Plate"))
        {
            KitchenObject playerKitchenObject = player.GetComponentInChildren<KitchenObject>();

            if (playerKitchenObject !=null && !this.HasKitchenObject())
            {
                Debug.Log("Place Item!");
                playerKitchenObject.transform.SetParent(this.transform);
                playerKitchenObject.transform.parent = counterTopPoint;
                playerKitchenObject.transform.localPosition = Vector3.zero;
            }
        }
        else if (listKitchenObject.Length > 1 && listKitchenObject.Contains("Plate"))
        {
            KitchenObject[] playerKitchenObject = player.GetComponentsInChildren<KitchenObject>();

            if (playerKitchenObject != null && !this.HasKitchenObject())
            {
                //int level = 0;
                for(int i = 0;  i < playerKitchenObject.Length; i++)
                {
                    playerKitchenObject[i].transform.SetParent(this.transform);
                    playerKitchenObject[i].transform.parent = counterTopPoint;
                    playerKitchenObject[i].transform.localPosition = Vector3.zero;
                }             
            }
        }
        else if(listKitchenObject.Length == 0)
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
