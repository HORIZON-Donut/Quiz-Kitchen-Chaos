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
            KitchenObject[] playerKitchenObject = player.GetComponentsInChildren<KitchenObject>();

            if (playerKitchenObject != null)
            {
                //int level = 0;
                for (int i = 0; i < playerKitchenObject.Length; i++)
                {
                    playerKitchenObject[i].transform.SetParent(this.transform);
                    playerKitchenObject[i].transform.parent = counterTopPoint;
                    playerKitchenObject[i].transform.localPosition = Vector3.zero;
                }
            }
        }

    }
    public Transform GetKitchenObjectFollowTransform() 
    {
        return counterTopPoint;
    }
}
