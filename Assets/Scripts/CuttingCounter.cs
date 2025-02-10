using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingCounter : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

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
			int cuttingMax = 0;
			float persent_process = 0f;

            KitchenObject kitchenObject = this.GetComponentInChildren<KitchenObject>();
            cuttingProcess += cuttingSpeed * Time.deltaTime;
            Cutting_FX(Time.deltaTime);

			ProcessBar processBar = this.GetComponentInChildren<ProcessBar>();
			
			switch(kitchenObject.GetKitchenObjectname())
			{
				case "Tomato":
					cuttingMax = cuttingRecipeSOArray[0].cutCount;
                	persent_process = (float)(cuttingProcess) / cuttingMax;
                	processBar.CuttingCounter_OnProcessChanged(persent_process);
                	if ((cuttingProcess) >= cuttingMax)
                	{
                    	Destroy(kitchenObject.gameObject);
                    	Transform sliceTransform = Instantiate(cuttingRecipeSOArray[0].to.prefab, counterTopPoint);
                    	sliceTransform.transform.localPosition = Vector3.zero;
                    	processBar.CuttingCounter_OnProcessChanged(0f);
                    	cuttingProcess = 0;
                	}
					break;

				case "Cheese":
					cuttingMax = cuttingRecipeSOArray[1].cutCount;
                	persent_process = (float)(cuttingProcess) / cuttingMax;
                	processBar.CuttingCounter_OnProcessChanged(persent_process);
                	if ((cuttingProcess) >= cuttingMax)
                	{
                    	Destroy(kitchenObject.gameObject);
                    	Transform sliceTransform = Instantiate(cuttingRecipeSOArray[1].to.prefab, counterTopPoint);
                    	sliceTransform.transform.localPosition = Vector3.zero;
                    	processBar.CuttingCounter_OnProcessChanged(0f);
                    	cuttingProcess = 0;
                	}

					break;

				case "Cabbage":
					cuttingMax = cuttingRecipeSOArray[2].cutCount;
                	persent_process = (float)(cuttingProcess) / cuttingMax;
                	processBar.CuttingCounter_OnProcessChanged(persent_process);
                	if ((cuttingProcess) >= cuttingMax)
                	{
                    	Destroy(kitchenObject.gameObject);
                    	Transform sliceTransform = Instantiate(cuttingRecipeSOArray[2].to.prefab, counterTopPoint);
                    	sliceTransform.transform.localPosition = Vector3.zero;
                    	processBar.CuttingCounter_OnProcessChanged(0f);
                    	cuttingProcess = 0;
                	}

					break;

				default:

					Debug.Log("Not in the List");
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
        if (player.HasKitchenObject() && !this.HasKitchenObject())
        {
            Debug.Log("Has item");
			
			int cuttingMax = 0;
			float persent_process = 0f;
			ProcessBar processBar;

            KitchenObject playerKitchenObject = player.GetComponentInChildren<KitchenObject>();
            Debug.Log(playerKitchenObject.GetKitchenObjectname());
            cuttingProcess = 0;

			switch(playerKitchenObject.GetKitchenObjectname())
			{
				case "Tomato":
					cuttingMax = cuttingRecipeSOArray[0].cutCount;
                	playerKitchenObject.transform.parent = counterTopPoint;
                	playerKitchenObject.transform.localPosition = Vector3.zero;
                	Debug.Log(cuttingMax);
                	processBar = this.GetComponentInChildren<ProcessBar>();
                	persent_process= (float)cuttingProcess / cuttingMax;
                	processBar.CuttingCounter_OnProcessChanged(persent_process);
                	cuttingProcess++;
                	animator.SetTrigger("Cut");
                	timer = 0f;

					break;

				case "Cheese":
					cuttingMax = cuttingRecipeSOArray[1].cutCount;
                	playerKitchenObject.transform.parent = counterTopPoint;
                	playerKitchenObject.transform.localPosition = Vector3.zero;
                	Debug.Log(cuttingMax);
                	processBar = this.GetComponentInChildren<ProcessBar>();
                	persent_process= (float)cuttingProcess / cuttingMax;
                	processBar.CuttingCounter_OnProcessChanged(persent_process);
                	cuttingProcess++;
                	animator.SetTrigger("Cut");
                	timer = 0f;

					break;

				case "Cabbage":
					cuttingMax = cuttingRecipeSOArray[2].cutCount;
                	playerKitchenObject.transform.parent = counterTopPoint;
                	playerKitchenObject.transform.localPosition = Vector3.zero;
                	Debug.Log(cuttingMax);
                	processBar = this.GetComponentInChildren<ProcessBar>();
                	persent_process= (float)cuttingProcess / cuttingMax;
                	processBar.CuttingCounter_OnProcessChanged(persent_process);
                	cuttingProcess++;
                	animator.SetTrigger("Cut");
                	timer = 0f;

					break;

				default:
					Debug.Log("Not in the List");
					break;
			}
        }
        else {
            if (this.HasKitchenObject())
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

}
