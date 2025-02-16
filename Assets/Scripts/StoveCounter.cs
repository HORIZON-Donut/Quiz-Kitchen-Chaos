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
        
    }
    public bool HasKitchenObject()
    {
        KitchenObject playerKitchenObject = this.GetComponentInChildren<KitchenObject>();
        return playerKitchenObject != null;
    }
}
