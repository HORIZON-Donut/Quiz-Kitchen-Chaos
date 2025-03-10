using System;
using System.Linq;
using UnityEngine;

public class StoveCounter : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private KitchenObjectSO cooked;
    [SerializeField] private KitchenObjectSO burned;
    [SerializeField] private StoveProcessBar processBar;
    [SerializeField] private AudioSource sound;
    [SerializeField] private int timeToCook;
    [SerializeField] private int timeToBurned;

    private float cookingProcess;
    private float cookingSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        if (cookingProcess > 0)
        {
            KitchenObject kitchenObject = this.GetComponentInChildren<KitchenObject>();
            cookingProcess += cookingSpeed * Time.deltaTime;

            int cookingMax = 0;
            float persent_process = 0f;

            if (kitchenObject == null)
            {
                processBar.CuttingCounter_OnProcessChanged(0.1f);
                cookingProcess = 0f;
                return;
            }

            if (kitchenObject.GetKitchenObjectname() == "Meat")
            {
                cookingMax = timeToCook;
                persent_process = (float)(cookingProcess) / cookingMax;
                processBar.CuttingCounter_OnProcessChanged(persent_process);
                if ((cookingProcess) >= cookingMax)
                {
                    Destroy(kitchenObject.gameObject);
                    Transform sliceTransform = Instantiate(cooked.prefab, counterTopPoint);
                    sliceTransform.transform.localPosition = Vector3.zero;
                    processBar.CuttingCounter_OnProcessChanged(0f);
                    cookingProcess = 1f;
                }
            }
            else if (kitchenObject.GetKitchenObjectname() == "CookedMeat")
            {
                cookingMax = timeToBurned;
                persent_process = (float)(cookingProcess) / cookingMax;
                processBar.CuttingCounter_OnProcessChanged(persent_process);
                if ((cookingProcess) >= cookingMax)
                {
                    Destroy(kitchenObject.gameObject);
                    Transform sliceTransform = Instantiate(burned.prefab, counterTopPoint);
                    sliceTransform.transform.localPosition = Vector3.zero;
                    processBar.CuttingCounter_OnProcessChanged(0f);
                    cookingProcess = 0;
                }
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
                cookingMax = timeToCook;
                playerKitchenObject.transform.parent = counterTopPoint;
                playerKitchenObject.transform.localPosition = Vector3.zero;
                persent_process = (float)cookingProcess / cookingMax;
                processBar.CuttingCounter_OnProcessChanged(persent_process);
                cookingProcess++;

                sound.Play();
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

                sound.Stop();
            }
        }
    }
    public bool HasKitchenObject()
    {
        KitchenObject playerKitchenObject = this.GetComponentInChildren<KitchenObject>();
        return playerKitchenObject != null;
    }
}
