using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HomeBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void homeBtnisClicked(){
        SceneManager.LoadScene("Scenes/Nazakt", LoadSceneMode.Single);
        Application.Quit();
    }
}
