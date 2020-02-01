using UnityEngine;

public class CarGameController : MonoBehaviour
{
    public JurassicCar _car;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartCar()
    {
        _car.MakeTheCarMoveIntoTheDirectionItIsCurrentlyLookingAtWithAPredefinedVelocityPlease(13.37f);
    }

    public void ResetCar()
    {
        _car.ResetCar();
    }

    public void OnWin()
    {

    }
}
