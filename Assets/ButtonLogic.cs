using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    public void NextLevel()
    {
        Object.FindObjectOfType<PreviewNextLevel>().OpenNext();
    }

}
