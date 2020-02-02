using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PreviewNextLevel : MonoBehaviour
{
    public InputAction _changeLevel;
    public string _nextSceneName;

    private bool locked;

    private void Start()
    {
        _changeLevel.Enable();
    }

    private void OnDestroy()
    {
        _changeLevel.Disable();
    }

    private void Update()
    {
        if(_changeLevel.ReadValue<float>() > 0f && !locked)
        {
            OpenNext();
            locked = true;
        }
    }

    public void OpenNext()
    {
        SceneManager.LoadScene(_nextSceneName, LoadSceneMode.Single);
    }
}
