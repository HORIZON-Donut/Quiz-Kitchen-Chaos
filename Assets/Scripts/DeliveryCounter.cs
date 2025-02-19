using UnityEngine;
using System.Linq;

public class DeliveryCounter : MonoBehaviour
{
    [SerializeField] private DeliveryManager deliveryManager;

    public void Interact(Player player)
    {
        string[] listKitchenObject = player.HasKitchenObject();
        
        if (listKitchenObject.Length > 0 )
        {
            Debug.Log(listKitchenObject[0]);
            if (deliveryManager.DeliveryRecipe(listKitchenObject) == true)
            {
                Debug.Log("Destroy Item!");
                KitchenObject[] playerKitchenObject = player.GetComponentsInChildren<KitchenObject>();
                if (playerKitchenObject.Length > 0)
                {
                    foreach (KitchenObject obj in playerKitchenObject)
                    {
                        Destroy(obj.gameObject);
                    }

                }
            }
        }
    }
}
