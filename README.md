# GTA-style-Get-In-and-Out-of-Vehicle
A manager for getting into and out of vehicles in Unity

Video Tutorial: https://www.youtube.com/watch?v=wPd61RvDDR0

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
