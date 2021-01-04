using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(20, 20, 200, 100), "button"))
        {
            SceneManager.LoadScene("Gamemain");
        }
    }
}
