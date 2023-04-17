using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System;
using UnityEngine.SceneManagement;

public class SignUpBtn : MonoBehaviour
{
    public TMP_InputField IDInput;
    public TMP_InputField PWInput;
    public TMP_InputField PWcheckInput;

    public static MySqlConnection conn = new MySqlConnection("");

    public void signUpBtnOnClicked(){     
        
        string signupquery = "insert into user_tb (user_id, user_pw) value ('" + IDInput.text + "', '" + PWInput.text + "');";

        if (IDInput.text == "" ||  PWInput.text == "")
        {
            Debug.Log("there are some empty space!");
        }
        else
        {
            if (PWInput.text != PWcheckInput.text)
            {
                Debug.Log("password doesn't match!");
            }
            else
            {
                Debug.Log("ID : " + IDInput.text);
                Debug.Log("PW : " + PWInput.text);

                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(signupquery, conn);
                    cmd.CommandText = signupquery;
                    cmd.ExecuteNonQuery();

                    Debug.Log(signupquery);
                    Debug.Log("success SignUp!");
                    //action 코드 필요(로그인 화면으로 전환)
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                conn.Close();
            }   
        }
    }
}
