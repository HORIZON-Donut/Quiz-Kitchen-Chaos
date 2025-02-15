using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;

    void Update()
    {
        //
    }

    public void Interact(Player player)
    {
        if (player.HasKitchenObject().Length > 0)
        {
            Debug.Log("Has item");
            KitchenObject[] playerKitchenObject = player.GetComponentsInChildren<KitchenObject>();

            foreach(KitchenObject playerObject in playerKitchenObject)
            {
                Destroy(playerObject);
            }
        }

    }
    public Transform GetKitchenObjectFollowTransform() 
    {
        return counterTopPoint;
    }
}
