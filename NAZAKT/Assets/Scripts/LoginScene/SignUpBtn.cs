using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System;

public class SignUpBtn : MonoBehaviour
{
    public static MySqlConnection conn = new MySqlConnection("SERVER = 221.140.207.231; port = 3306; DATABASE = capstone; UID = tester; PWD = P@ssw0rd;");

    public TMP_InputField IDInput;
    public TMP_InputField PWInput;
    public TMP_InputField PWcheckInput;
    public TMP_InputField NameInput;
    public TextMeshProUGUI PWDuplicationChecker;
    public TextMeshProUGUI IDDuplicateTxt;
    
    public void signUpBtnOnClicked(){
        // 오브젝트 가져오기
        TextMeshProUGUI SignUpSucTxt = GameObject.Find("SignUpSucTxt").GetComponent<TextMeshProUGUI>();
        string signupquery = "insert into user_tb (user_id, user_pw, user_nm) value ('" + IDInput.text + "', '" + PWInput.text + "', '" + NameInput.text +"');";

        if(PWDuplicationChecker.text == "비밀 번호 같음" && IDDuplicateTxt.text == "사용 가능" && NameInput.text != "")
        {
            try
            {
                conn.Open();
                Debug.Log(signupquery);
                MySqlCommand cmd = new MySqlCommand(signupquery, conn);
                cmd.ExecuteNonQuery();
                SignUpSucTxt.text = "회원 가입 성공";
                conn.Close();
                
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }
        }
        else
        {
            SignUpSucTxt.text = "회원 가입 실패";   
        }

        // 데이터 초기화...
        IDInput.text = "";
        PWInput.text = "";
        PWcheckInput.text = "";
        NameInput.text = "";
        PWDuplicationChecker.text = "";
        IDDuplicateTxt.text = "";
    }
}
