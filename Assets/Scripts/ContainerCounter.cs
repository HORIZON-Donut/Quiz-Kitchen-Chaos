using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ContainerCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObject;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Interact(Player player)
    {
        if (player.HasKitchenObject()) { Debug.Log("Has item"); }

        if (kitchenObject == null)
        {
            animator.SetTrigger("OpenClose");
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.localPosition = Vector3.zero;
            kitchenObject = kitchenObjectTransform.transform.GetComponent<KitchenObject>();
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                kitchenObject.transform.SetParent(player.transform);
                kitchenObject.transform.parent = player.GetKitchenObjectFollowTransform();
                kitchenObject.transform.localPosition = Vector3.zero;
                ClearKitchenObject();
            }
        }
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
}
