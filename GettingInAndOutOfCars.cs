
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

    [Space, Header("Car Stuff")]
    [SerializeField] GameObject car = null;
    CarUserControl userController = null;
    [SerializeField] CarController carEngine = null;

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

        if (human.activeSelf) //If the character is active, we expect ALL carcontrollers to be disabled (Though we could just make sure vehicles don't have UserControl scripts on them)
        {
            CarUserControl[] controllers = FindObjectsOfType<CarUserControl>();
            for (int i = 0; i < controllers.Length; i++)
            {
                if (controllers[i].enabled)
                    Debug.LogWarning(controllers[i].name + " has an enabled controller and may be receiving player input. Click this warning to see the vehicle.", controllers[i]);
            }
        }
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

        human.transform.position = car.transform.position + car.transform.TransformDirection(Vector3.left);


        if (humanCameraObj)
        {
            autoCamera.gameObject.SetActive(false);
            humanCameraObj.SetActive(true);
        }
        else
            autoCamera.SetTarget(human.transform);

        

        userController.enabled = false;

        carEngine.Move(0, 0, 1, 1);

        userController = null;

        Destroy(car.GetComponent<CarUserControl>());

    }

    void GetIntoCar()
    {
        inCar = true;

        human.SetActive(false);

        userController = car.AddComponent<CarUserControl>();

        userController.enabled = true;

        carEngine.enabled = true;

        if (humanCameraObj)
        {
            humanCameraObj.SetActive(false);
            autoCamera.gameObject.SetActive(true);
        }

        autoCamera.SetTarget(car.transform);
    }
}
