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
using UnityEngine.Windows;

public class IDDuplicateCheckerBtn : MonoBehaviour
{
    string UserId = null;                        // 사용자가 입력한 ID
    TextMeshProUGUI AvailabilityTxt = null;      // 사용 가능 여부 TXT

    //DB 연결
    public static MySqlConnection conn = new MySqlConnection("");
    

    public void IDDuplicateCheckerBtnOnClicked(TMP_InputField ip){
        //입력 필드값 가져오기...
        UserId = ip.text;
        Debug.Log("중복 확인 ID : " + ip.text);
        
        // IDDuplicateTxt(중복 확인 텍스트) 정보 불러오기...
        GameObject IDDuplicateTxt = GameObject.Find("IDDuplicateTxt");
        AvailabilityTxt = IDDuplicateTxt.GetComponent<TextMeshProUGUI>();

        string checkidquery = "select count(*) from user_tb where user_id = '" + UserId + "';";
        int count = 0;

        try
        {
            MySqlCommand cmd = new MySqlCommand(checkidquery, conn);
            cmd.CommandText = checkidquery;
            MySqlDataReader drd = cmd.ExecuteReader();

            Debug.Log(checkidquery);

            while (drd.Read())
            {
                count = count + 1;
            }

            if (count != 1)
            {
                
                //이미 존재하는 아이디
                AvailabilityTxt.color = new Color32(255, 0, 0, 255);
                AvailabilityTxt.text = "사용 불가";
            }
            else
            {
                //사용가능한 아이디
                AvailabilityTxt.color = new Color32(0, 0, 255, 255);
                AvailabilityTxt.text = "사용 가능";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        conn.Close();
    }
}
