using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

using TMPro;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System;

public class WOStartBtn : MonoBehaviour
{
    public static MySqlConnection conn = new MySqlConnection("SERVER = 175.114.255.210; port = 3306; DATABASE = capstone; UID = tester; PWD = P@ssw0rd;");

    public TextMeshProUGUI WOnameTxt;
    public TMP_Dropdown setDrop;
    public TMP_Dropdown cntDrop;
    //public string userID;
    
    public WOStatus WOStatus;

    public string todate;

  // Start is called before the first frame update
  void Start()
    {
        WOStatus = GameObject.Find("WOStatus").GetComponent<WOStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sceneChange(){
        SetWOInfo();
        DontDestroyOnLoad(WOStatus); // 운동 정보 담은 오브젝트 넘기기
        todate = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss"));

        try {
            string startwoquery = "insert into user_tb (user_id, wo_name, set_set, set_count, do_set, do_count, do_date) value (WOStatus.userId, WOStatus.WOname, WOStatus.setNum, WOStatus.cntNum, 0, 0, todate);";
            Debug.Log(startwoquery);

            conn.Open();
            MySqlCommand cmd = new MySqlCommand(startwoquery, conn);
            cmd.ExecuteNonQuery();
            //SceneManager.LoadScene(0);
            //SetWOInfo();
            //DontDestroyOnLoad(WOStatus); // 운동 정보 담은 오브젝트 넘기기
            SceneManager.LoadScene(0);
            conn.Close();
        }
        catch (Exception ex) { 
            Debug.Log(ex.ToString());
        }
    }

    // 운동 정보 세팅 메소드
    public void SetWOInfo(){
        Debug.Log("ID : " + UserInfo.id);

        WOStatus.userID = UserInfo.id;
        WOStatus.WOname = WOnameTxt.text;
        WOStatus.setNum = setDrop.value;
        WOStatus.cntNum = cntDrop.value;
        WOStatus.date = todate;

        // Debug.Log("운동명 : " + WOStatus.WOname);
        // Debug.Log("세트 수 : " + WOStatus.setNum);
        // Debug.Log("횟수 : " + WOStatus.cntNum);
    }
}
