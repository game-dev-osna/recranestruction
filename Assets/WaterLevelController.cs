using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterLevelController : MonoBehaviour
{
    public GameObject _successView;
    public Text _lostText;
    public Text _movedText;

    private int _instanceCount;
    private int _moved;
    private int _lost;

    private void Start()
    {
        UpdateTexts();
    }

    public void OnObjectWasLost(Transform other)
    {
        _lost++;
        UpdateTexts();
        Destroy(other.gameObject);
        CheckLevelEnd();
    }

    public void OnObjectWasMoved(Transform other)
    {
        _moved++;
        UpdateTexts();
        Destroy(other.gameObject);
        CheckLevelEnd();
    }

    private void UpdateTexts()
    {
        _movedText.text = "Moved: " + _moved;
        _lostText.text = "Lost: " + _lost;
    }

    private void CheckLevelEnd()
    {
        if(_moved + _lost == 3)
        {
            Instantiate(_successView);
        }
    }
}
