using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInfoBtn : MonoBehaviour
{
    string UserId;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UserInfoBtnOnClicked(){
        
        // 내 정보 UserInfoFrame의 속성 선언
        TMP_InputField idInput = GameObject.Find("IDInput").GetComponent<TMP_InputField>(); // id input 필드
        TMP_InputField weightInput = GameObject.Find("WeightInput").GetComponent<TMP_InputField>(); // 몸무게 input 필드
        TMP_InputField heightInput = GameObject.Find("HeightInput").GetComponent<TMP_InputField>(); // 키 input 필드



        // test
        if(UserInfo.id == ""){
            UserInfo.id = "test";
            Debug.Log("id: " + UserInfo.id);
        }
        //

        // UserInfoFrame의 속성 변경
        idInput.text = UserInfo.id;

        // Debug.Log(UserInfo.id);
        // Debug.Log(idInput);
    }
}
