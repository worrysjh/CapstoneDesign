using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System;

public class LoginBtn : MonoBehaviour
{
    public TMP_InputField IDInput;
    public TMP_InputField PWInput;
    public GameObject LoginFail;

    public static MySqlConnection conn = new MySqlConnection("SERVER = 175.114.255.210; port = 3306; DATABASE = capstone; UID = tester; PWD = P@ssw0rd;");

    void Start()
    {
        try
        {
            conn.Open();
            Debug.Log(string.Format("conncetion is '{0}'", conn.State));
        }
        catch (Exception ex)
        {
            Debug.Log("can't open conn!");
            string line = string.Format("'{0},", ex);
            Debug.Log(line);
        }
    }

    public void sceneChange(){
        string loginquery = "select * from user_tb where user_id = '" + IDInput.text + "'&& user_pw = '" + PWInput.text + "';";
        //string loginquery = "select * from user_tb where user_id = @id and user_pw = @pw";
        

        
        int count = 0;

        try
        {
            MySqlCommand cmd = new MySqlCommand(loginquery, conn);
        //    cmd.Parameters.AddWithValue("@id", IDInput.text);
        //    cmd.Parameters.AddWithValue("@pw", PWInput.text);
            cmd.CommandText = loginquery;
            

            Debug.Log(loginquery);

            using(MySqlDataReader drd = cmd.ExecuteReader()){
                while (drd.Read())
                {
                    count = count + 1;
                }
            }

            Debug.Log(count);

            if (count == 1)
            {
                Debug.Log("ID : " + IDInput.text);
                Debug.Log("PW : " + PWInput.text);

                UserInfo.id = IDInput.text;

                SceneManager.LoadScene("Nazakt");
                conn.Close();
            }
            else
            {   
                Debug.Log("Fail to login!");

                LoginFail.SetActive(true);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            Console.WriteLine(ex.ToString());
        }
        Debug.Log(string.Format("conncetion is '{0}'", conn.State));
       // Debug.Log(loginquery);
    }
}
