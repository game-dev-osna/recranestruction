using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class GameMaster : MonoBehaviour
{

    static string loadedScene;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStart()
    {
        Debug.Log("Clicked Start");
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
