using UnityEngine;
using UnityEngine.UI;

public class FireLevelGameManager: MonoBehaviour
{
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
    }

    private void UpdateTexts()
    {
        _remainingText.text = "Remaining: " + fires;
    }
}
