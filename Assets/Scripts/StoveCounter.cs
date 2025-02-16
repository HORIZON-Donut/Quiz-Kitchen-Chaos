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
        if (cookingProcess > 0)
        {
            KitchenObject kitchenObject = this.GetComponentInChildren<KitchenObject>();
            cookingProcess += cookingSpeed * Time.deltaTime;

            int cookingMax = 0;
            float persent_process = 0f;

            if(kitchenObject.GetKitchenObjectname() == "Meat")
            {
                cookingMax = isBurning ? timeToBurned : timeToCook;
                persent_process = (float)(cookingProcess) / cookingMax;
                processBar.CuttingCounter_OnProcessChanged(persent_process);
                if ((cookingProcess) >= cookingMax)
                {
                    Destroy(kitchenObject.gameObject);
                    Transform sliceTransform = Instantiate(cooked.prefab, counterTopPoint);
                    sliceTransform.transform.localPosition = Vector3.zero;
                    processBar.CuttingCounter_OnProcessChanged(0f);
                    cookingProcess = 0;

                    sound.Stop();
                    //isBurning = true;
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
                isBurning = false;
                cookingMax = timeToCook;
                playerKitchenObject.transform.parent = counterTopPoint;
                playerKitchenObject.transform.localPosition = Vector3.zero;
                persent_process = (float)cookingProcess / cookingMax;
                processBar.CuttingCounter_OnProcessChanged(persent_process);
                cookingProcess++;
                timer = 0f;

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
            }
        }
    }
    public bool HasKitchenObject()
    {
        KitchenObject playerKitchenObject = this.GetComponentInChildren<KitchenObject>();
        return playerKitchenObject != null;
    }
}
