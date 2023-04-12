using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoginBtn : MonoBehaviour
{
    public TMP_InputField IDInput;
    public TMP_InputField PWInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sceneChange(){
        
        // 로그인 함수 호출 부분...
       
        Debug.Log("ID : " + IDInput.text);
        Debug.Log("PW : " + PWInput.text); 
        SceneManager.LoadScene("Nazakt");
    }

    // id, pw 일치 시
    void logIn(string id, string pw){
         if(IDInput.text == id && PWInput.text == pw){
            SceneManager.LoadScene("Nazakt");
         }
         else{

         }
    }
}
