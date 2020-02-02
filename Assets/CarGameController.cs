using UnityEngine;

public class CarGameController : MonoBehaviour
{
    public GameObject _successView;
    public JurassicCar _car;

    // Start is called before the first frame update
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
        Instantiate(_successView);
    }
}
