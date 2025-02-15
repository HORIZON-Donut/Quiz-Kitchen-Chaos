using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class CuttingCounter : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
	[SerializeField] private ProcessBar processBar;

    private float cuttingProcess;
    public float cuttingSpeed = 5f;
    public float knifeSpeed = 0.2f;
    private Animator animator;
    private float timer = 0f;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (cuttingProcess > 0)
        {
            KitchenObject kitchenObject = this.GetComponentInChildren<KitchenObject>();
            cuttingProcess += cuttingSpeed * Time.deltaTime;
            Cutting_FX(Time.deltaTime);

			int cuttingMax = 0;
			float persent_process = 0f;

			switch(kitchenObject.GetKitchenObjectname())
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
    private void Cutting_FX(float duration) {
        timer += duration;
        if (timer >= knifeSpeed)
        {
            animator.SetTrigger("Cut");
            timer = 0f;
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
            cuttingProcess = 0;
			int cuttingMax = 0;
			float persent_process = 0f;

			switch(playerKitchenObject.GetKitchenObjectname())
			{
				case "Tomato":
					CuttingObject(0, cuttingMax, persent_process, playerKitchenObject);
					break;

				case "Cheese":
					CuttingObject(1, cuttingMax, persent_process, playerKitchenObject);
					break;

				case "Cobbage":
					CuttingObject(2, cuttingMax, persent_process, playerKitchenObject);
					break;

				default:
					Debug.Log("Not in list");
					break;
			}
        }
        else {
            if (this.HasKitchenObject() && listKitchenObject.Contains("Plate"))
            {
                KitchenObject kitchenObject = this.GetComponentInChildren<KitchenObject>();
                kitchenObject.transform.SetParent(player.transform);
                kitchenObject.transform.parent = player.GetKitchenObjectFollowTransform();
                kitchenObject.transform.localPosition = Vector3.zero;
            }
        }

    }

    public void InterActAlter(Player player) 
    { 

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

	private void CuttingObject(int index, int cuttingMax, float persent_process, KitchenObject playerKitchenObject)
	{
		cuttingMax = cuttingRecipeSOArray[index].cutCount;
		playerKitchenObject.transform.parent = counterTopPoint;
		playerKitchenObject.transform.localPosition = Vector3.zero;
		persent_process = (float)cuttingProcess/cuttingMax;
		processBar.CuttingCounter_OnProcessChanged(persent_process);
		cuttingProcess++;
		animator.SetTrigger("Cut");
		timer = 0f;
	}

    private void CuttingProcess(int index,  float cuttingMax, float persent_process, KitchenObject kitchenObject)
    {
        cuttingMax = cuttingRecipeSOArray[index].cutCount;
        persent_process = (float)(cuttingProcess) / cuttingMax;
        processBar.CuttingCounter_OnProcessChanged(persent_process);
        if ((cuttingProcess) >= cuttingMax)
        {
            Destroy(kitchenObject.gameObject);
            Transform sliceTransform = Instantiate(cuttingRecipeSOArray[index].to.prefab, counterTopPoint);
            sliceTransform.transform.localPosition = Vector3.zero;
            processBar.CuttingCounter_OnProcessChanged(0f);
            cuttingProcess = 0;
        }
    }

}
