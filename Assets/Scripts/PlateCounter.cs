using UnityEngine;

public class PlateCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    private Transform PlateObject;

    private void Start()
    {
        PlateObject = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        PlateObject.localPosition = Vector3.zero;
    }

    public void Interact(Player player)
    {
        if (player.HasPlate() == false && PlateObject != null) {
            //Pick Plate
            PlateObject.transform.SetParent(player.transform);
            PlateObject.transform.parent = player.GetKitchenObjectFollowTransform();
            PlateObject.transform.localPosition = new Vector3(0f,-0.1f,0.2f);
            //Create Plate
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.localPosition = Vector3.zero;
            PlateObject = kitchenObjectTransform;

        }
    }

}
