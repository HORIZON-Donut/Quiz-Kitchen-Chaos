using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    [SerializeField] private float movespeed = 5f;
    [SerializeField] private float rotatespeed = 8f;
    [SerializeField] private Transform holdPoint;
    private bool isWalking = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);
        inputVector.x = Input.GetAxis("Horizontal"); // Horizontal axis handles A/D or Left/Right arrow keys
        inputVector.y = Input.GetAxis("Vertical");   // Vertical axis handles W/S or Up/Down arrow keys

        inputVector = inputVector.normalized;

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        transform.position += moveDir * movespeed * Time.deltaTime;

        isWalking = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotatespeed);
        //Debug.Log(inputVector);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
    private void OnCollisionEnter(Collision collision)
    {
		Transform selectedCounter = collision.gameObject.transform.Find("Selected");
		if(selectedCounter != null) {selectedCounter.gameObject.SetActive(true);}
		switch(collision.gameObject.tag)
		{
			case "Counter":
				ClearCounter clearCounter = collision.gameObject.GetComponent<ClearCounter>();
				clearCounter.Interact(this);
				break;

			case "Container":
				ContainerCounter container = collision.gameObject.GetComponent<ContainerCounter>();
				container.Interact(this);
				break;

			case "Cutting":
				CuttingCounter cutting = collision.gameObject.GetComponent<CuttingCounter>();
				cutting.Interact(this);
				break;

			case "PlatesCounter":
				PlateCounter plate = collision.gameObject.GetComponent<PlateCounter>();
				plate.Interact(this);
				break;

            case "TrashCounter":
                TrashBin trash = collision.gameObject.GetComponent<TrashBin>();
                trash.Interact(this);
                break;

            case "StoveCounter":
                StoveCounter stove = collision.gameObject.GetComponent<StoveCounter>();
                stove.Interact(this);
                break;

            default:
				Debug.Log("Not in counter list");
				break;
		}
    }
    private void OnCollisionExit(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Counter":
            case "Container":
            case "Cutting":
            case "PlatesCounter":
            case "TrashCounter":
            case "StoveCounter":
                Transform selectedCounter = collision.gameObject.transform.Find("Selected");
                selectedCounter.gameObject.SetActive(false);
                break;

            default:
                break;
        }
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return holdPoint;
    }
    public string[] HasKitchenObject()
    {
        KitchenObject[] playerKitchenObject = this.GetComponentsInChildren<KitchenObject>();
        string[] listkitchenObject = {};
        if (playerKitchenObject.Length > 0)
        {
            foreach (KitchenObject obj in playerKitchenObject)
            {
                System.Array.Resize(ref listkitchenObject, listkitchenObject.Length + 1);
                listkitchenObject[listkitchenObject.Length - 1] = obj.GetKitchenObjectname();
            }
        }

        return listkitchenObject;
 
    }
    public bool HasPlate()
    {
        KitchenObject playerPlate = this.GetComponentInChildren<KitchenObject>();
        bool isPlate = false;
        if (playerPlate != null)
        {
            if (playerPlate.GetKitchenObjectname() == "Plate")
            {
                isPlate = true;
            }
        }
        return isPlate;
    }
}
