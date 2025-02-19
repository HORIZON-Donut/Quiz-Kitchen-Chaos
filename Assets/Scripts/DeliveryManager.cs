using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private DeliveryUI deliveryUI;
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 3;

    private void Awake()
    {
            waitRecipeSOList = new List<RecipeSO>();    
    }
    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitRecipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);
                waitRecipeSOList.Add(waitingRecipeSO);
                deliveryUI.UpdateVisual(waitRecipeSOList);
            }
                
        }
    }

    public bool DeliveryRecipe(string[] deliveryRecipe) {
        bool isDeliverySuccess = false;
        for (int i = 0; i < waitRecipeSOList.Count; ++i)
        {
            string[] deliveryCheck = deliveryRecipe;
            deliveryCheck = deliveryCheck.Where(item => item != "Plate").ToArray();
            //Debug.Log("L"+deliveryCheck.Length);
            //Debug.Log("W"+waitRecipeSOList[i].kitchenObjectSOList.Count);
            // Check length of order and delivery
            if (deliveryCheck.Length == waitRecipeSOList[i].kitchenObjectSOList.Count)
            {
                foreach (KitchenObjectSO obj in waitRecipeSOList[i].kitchenObjectSOList)
                {
                    //Debug.Log("L"+obj.objectname);
                    //foreach(string sstr in deliveryCheck)
                    //    Debug.Log("W"+sstr);
                    deliveryCheck = deliveryCheck.Where(item => item != obj.objectname).ToArray();
                }
                //Debug.Log("L" + deliveryCheck.Length);
                if (deliveryCheck.Length == 0)
                {
                    //Debug.Log("match");
                    isDeliverySuccess = true;
                    waitRecipeSOList.RemoveAt(i);
                    deliveryUI.UpdateVisual(waitRecipeSOList);
                    break;
                }
            }
        }
        return isDeliverySuccess;
    }


}
