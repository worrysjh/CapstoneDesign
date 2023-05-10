using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class SignUpSucBtn : MonoBehaviour
{
    public TextMeshProUGUI message;
    public GameObject SignUpSucFrame;
    public bool isSuc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void signUpSucBtnOnClicked(){
        if(message.text == "회원 가입 성공"){
            SignUpSucFrame.SetActive(false);
        }
    }
}
