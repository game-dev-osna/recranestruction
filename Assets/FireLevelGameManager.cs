using UnityEngine;
using UnityEngine.UI;

public class FireLevelGameManager: MonoBehaviour
{
    public GameObject _successScreen;
    public Text _remainingText;
    public int fires;

    private void Start()
    {
        UpdateTexts();
    }

    public void FireRemoved()
    {
        fires--;
        UpdateTexts();

        if(fires == 0)
        {
            Instantiate(_successScreen);
        }
    }

    private void UpdateTexts()
    {
        _remainingText.text = "Remaining: " + fires;
    }
}
