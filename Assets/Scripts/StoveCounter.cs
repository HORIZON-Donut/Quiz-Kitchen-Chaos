using UnityEngine;

public class StoveCounter : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private KitchenObject cooked;
    [SerializeField] private KitchenObject buurned;
    [SerializeField] private ProcessBar processBar;
    [SerializeField] private float timeToCook;
    [SerializeField] private float timeToBurned;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(Player player)
    {
        //
    }
}
