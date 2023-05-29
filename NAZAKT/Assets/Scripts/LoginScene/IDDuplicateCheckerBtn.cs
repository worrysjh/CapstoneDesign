using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System;


public class IDDuplicateCheckerBtn : MonoBehaviour
{
    string UserId = null;                        // 사용자가 입력한 ID
    TextMeshProUGUI AvailabilityTxt = null;      // 사용 가능 여부 TXT

    public static MySqlConnection conn = new MySqlConnection("SERVER = 175.114.255.210; port = 3306; DATABASE = capstone; UID = tester; PWD = P@ssw0rd;");


    public void IDDuplicateCheckerBtnOnClicked(TMP_InputField ip){
        //입력 필드값 가져오기...
        UserId = ip.text;
        Debug.Log("중복 확인 ID : " + ip.text);
        
        // IDDuplicateTxt(중복 확인 텍스트) 정보 불러오기...
        GameObject IDDuplicateTxt = GameObject.Find("IDDuplicateTxt");
        AvailabilityTxt = IDDuplicateTxt.GetComponent<TextMeshProUGUI>();

        string idcheckquery = "select * from user_tb where user_id = '" + UserId + "';";
        int count = 0;

        try
        {
            conn.Open();
            Debug.Log(idcheckquery);
            MySqlCommand cmd = new MySqlCommand(idcheckquery, conn);
            cmd.CommandText = idcheckquery;
            MySqlDataReader drd;

            Debug.Log(string.Format("conncetion is '{0}'", conn.State));

            using (drd = cmd.ExecuteReader()) {
                while (drd.Read())
                {
                    count = count + 1;
                }

                Debug.Log(count);

                if (count == 0)
                {
                    AvailabilityTxt.color = new Color32(0, 0, 255, 255);
                    AvailabilityTxt.text = "사용 가능";
                }
                else
                {
                    AvailabilityTxt.color = new Color32(255, 0, 0, 255);
                    AvailabilityTxt.text = "사용 불가";
                }
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
    }
}
