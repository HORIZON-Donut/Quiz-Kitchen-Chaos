using System;
using System.Linq;
using UnityEngine;

public class StoveCounter : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private KitchenObjectSO cooked;
    [SerializeField] private KitchenObjectSO burned;
    [SerializeField] private ProcessBar processBar;
    [SerializeField] private int timeToCook;
    [SerializeField] private int timeToBurned;

    private float cookingProcess;
    private float cookingSpeed = 5f;
    private float timer = 0f;

    private bool isBurning = false;

    //private Animator animator;
    private void Awake()
    {
        isBurning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cuttingProcess > 0)
        {
            KitchenObject kitchenObject = this.GetComponentInChildren<KitchenObject>();
            cuttingProcess += cuttingSpeed * Time.deltaTime;
            Cutting_FX(Time.deltaTime);

            int cuttingMax = 0;
            float persent_process = 0f;

            switch (kitchenObject.GetKitchenObjectname())
            {
                case "Tomato":
                    CuttingProcess(0, cuttingMax, persent_process, kitchenObject);
                    break;

                case "Cheese":
                    CuttingProcess(1, cuttingMax, persent_process, kitchenObject);
                    break;

                case "Cabbage":
                    CuttingProcess(2, cuttingMax, persent_process, kitchenObject);
                    break;

                default:
                    Debug.Log("Not in list");
                    break;
            }
        }
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
            int cookingMax = 0;
            float persent_process = 0f;

            if (playerKitchenObject.GetKitchenObjectname() == "Meat")
            {
                isBurning = false;
                cookingMax = timeToCook;
                playerKitchenObject.transform.parent = counterTopPoint;
                playerKitchenObject.transform.localPosition = Vector3.zero;
                persent_process = (float)cookingProcess / cookingMax;
                processBar.CuttingCounter_OnProcessChanged(persent_process);
                cookingProcess++;
                timer = 0f;
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
