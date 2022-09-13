
[RequireComponent(typeof(CarUserControl))] //Puts usercontroller directly on here since it only needs ref to car to work
public class GettingInAndOutOfCars : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] AutoCam autoCamera = null;
    [Header("Camera Attached to or following a Character")]
    [Header("Don't put anything here if you only use one camera")]
    [SerializeField] GameObject humanCameraObj = null; //If using a different camera for your character than for the car.

    [Header("Human")]
    [SerializeField] GameObject human = null;

    [SerializeField] float closeDistance = 15f;

    GameObject car = null;
    CarUserControl userController = null; //PLEASE UPDATE CARUSERCONTROL SCRIPT WITH the following method:
    /*
    *(This goes in CarUserControl script)
    *
    * public void SetCar(CarController car) => m_Car = car;
    */
    
    CarController carEngine = null;

    [Header("Input")]
    [SerializeField] KeyCode enterExitKey = KeyCode.E;

    bool inCar = false;

    void Start()
    {
        if (!human)
            ShowError("a character assigned");

        if (!humanCameraObj)
            Debug.Log("No camera for character is assigned to the GetIntoAndOutOfCars script (auto camera will be used).");

        if (!autoCamera)
            ShowError("an auto camera for the car/character assigned");

        userController = GetComponent<CarUserControl>();
        //If the character is active, we expect car controller to be inactive (the inverse of character's active state)
        userController.enabled = !human.activeSelf;
    }

    private void ShowError(string errorMessage)
    {
        Debug.LogError("You need " + errorMessage + " to the GTA script in order to get into and out of cars");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(enterExitKey))
        {
            if (inCar)
                GetOutOfCar();
            else if (CarNearby())//if out of car
                GetIntoCar();
        }
    }

    private bool CarNearby()
    {
        Collider[] cols = Physics.OverlapSphere(human.transform.position
            + human.transform.InverseTransformDirection(Vector3.forward * (closeDistance * .5f)), closeDistance); //do the check in front of the player

        for (int i = 0; i < cols.Length; i++)
        {
            //This doesn't work if the vehicle is the child of something.
            if (cols[i].transform.root.TryGetComponent(out carEngine))
            {
                car = cols[i].transform.root.gameObject;
                return true;
            }
        }
        return false;
    }

    void GetOutOfCar()
    {
        inCar = false;

        human.SetActive(true);

        human.transform.position = car.transform.position + car.transform.TransformDirection(Vector3.left) + Vector3.up; //try not to fall through ground


        if (humanCameraObj)
        {
            autoCamera.gameObject.SetActive(false);
            humanCameraObj.SetActive(true);
        }
        else
            autoCamera.SetTarget(human.transform);



        userController.enabled = false;
        userController.SetCar(null);

        carEngine.Move(0, 0, 1, 1);

    }

    void GetIntoCar()
    {
        inCar = true;

        human.SetActive(false);

        carEngine.enabled = true;

        userController.enabled = true;
        userController.SetCar(carEngine);


        if (humanCameraObj)
        {
            humanCameraObj.SetActive(false);
            autoCamera.gameObject.SetActive(true);
        }

        autoCamera.SetTarget(car.transform);
    }
}
