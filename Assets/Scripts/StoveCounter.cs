using System.Linq;
using UnityEngine;

public class StoveCounter : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private KitchenObjectSO cooked;
    [SerializeField] private KitchenObjectSO burned;
    [SerializeField] private ProcessBar processBar;
    [SerializeField] private float timeToCook;
    [SerializeField] private float timeToBurned;

    private float cookingProcess;
    private float cookingSpeed = 5f;
    private Animator animator;
    private float time = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(Player player)
    {
        string[] listKitchenObject = player.HasKitchenObject();
        if (listKitchenObject.Length == 1 && !listKitchenObject.Contains("Plate") && !this.HasKitchenObject())
        {
            Debug.Log("Has item");
            KitchenObject playerKitchenObject = player.GetComponentInChildren<KitchenObject>();
            Debug.Log(playerKitchenObject.GetKitchenObjectname());
            cookingProcess = 0;
            int cuttingMax = 0;
            float persent_process = 0f;

            if (playerKitchenObject.GetKitchenObjectname() == "Meat")
            {
                //
            }
        }
        else
        {
            if (this.HasKitchenObject() && listKitchenObject.Contains("Plate"))
            {
                KitchenObject kitchenObject = this.GetComponentInChildren<KitchenObject>();
                kitchenObject.transform.SetParent(player.transform);
                kitchenObject.transform.parent = player.GetKitchenObjectFollowTransform();
                kitchenObject.transform.localPosition = Vector3.zero;
            }
        }
    }
    public bool HasKitchenObject()
    {
        KitchenObject playerKitchenObject = this.GetComponentInChildren<KitchenObject>();
        return playerKitchenObject != null;
    }
}
