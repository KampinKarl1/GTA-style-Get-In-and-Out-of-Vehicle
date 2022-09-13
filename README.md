THERE IS NOW A TINY BIT OF EFFORT REQUIRED FROM THE USER!

THE GETTING IN AND OUT OF CAR SCRIPT REQUIRES THAT YOU ADD THE METHOD
public void SetCar(CarController car) => m_Car = car;
to the Unity Standard Assets script "CarUserControl"

The above allows for caching the CarUserControl script once at Start - the CarUserControl script doesn't have to be on the vehicle you're using,
it just has to have a reference to the vehicle it is controlling.



# GTA-style-Get-In-and-Out-of-Vehicle
A manager for getting into and out of vehicles in Unity

Video Tutorial: https://www.youtube.com/watch?v=wPd61RvDDR0
Written Tutorial: https://wordpress.com/post/anothertexaninspain.wordpress.com/517

Import the following (free) Unity Standard Assets packages into your scene:
  Cameras
  Characters
  MultiplatformInput
  Vehicles
 
Create an object in the scene upon which you can put the GetInAndOutOfCars script.

I've made it so you're using separate cameras for the character and the vehicle so you'll want to put two cameras into the scene and disable one.
  Place the cameras into the camera fields on the GTA script.

If you start controlling the car, you should disable the character and its corresponding camera and visa versa.

Place your character into the character object field in the GTA script.



Using a manager to enable and disable the character and controller and vehicle and controller is tantamount to creating an object that follows the player around and changes controls based on state (inCar, !inCar). This requires less code and I think that makes it easier to understand.
