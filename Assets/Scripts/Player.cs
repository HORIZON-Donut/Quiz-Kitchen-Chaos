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
        if (collision.gameObject.tag == "Counter") 
        {
            Transform selectedCounter = collision.gameObject.transform.Find("Selected");
            selectedCounter.gameObject.SetActive(true);
            ClearCounter clearcounter = collision.gameObject.GetComponent<ClearCounter>();
            clearcounter.Interact(this);
        }
        else if (collision.gameObject.tag == "Container")
        {
            Transform selectedCounter = collision.gameObject.transform.Find("Selected");
            selectedCounter.gameObject.SetActive(true);
            ContainerCounter containercounter = collision.gameObject.GetComponent<ContainerCounter>();
            containercounter.Interact(this);
            Debug.Log(collision.gameObject.name);
        }
        else if (collision.gameObject.tag == "Cutting")
        {
            Transform selectedCounter = collision.gameObject.transform.Find("Selected");
            selectedCounter.gameObject.SetActive(true);
            CuttingCounter containercounter = collision.gameObject.GetComponent<CuttingCounter>();
            containercounter.Interact(this);
            Debug.Log(collision.gameObject.name);
        }
        else if (collision.gameObject.tag == "PlatesCounter")
        {
            Transform selectedCounter = collision.gameObject.transform.Find("Selected");
            selectedCounter.gameObject.SetActive(true);
            PlateCounter platecounter = collision.gameObject.GetComponent<PlateCounter>();
            platecounter.Interact(this);
            Debug.Log(collision.gameObject.name);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Counter" || collision.gameObject.tag == "Container" || collision.gameObject.tag == "Cutting" || collision.gameObject.tag == "PlatesCounter")
        {
            Transform selectedCounter = collision.gameObject.transform.Find("Selected");
            selectedCounter.gameObject.SetActive(false);
            Debug.Log(collision.gameObject.name);
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
