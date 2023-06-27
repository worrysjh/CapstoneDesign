using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System;

using TMPro;
using JetBrains.Annotations;

public class LogLoader : MonoBehaviour
{
    public static MySqlConnection conn;
    private Slider slider;
    private TextMeshProUGUI txt;

    private GameObject woLog;
    private GameObject content;
	GameObject flatCalendar;


    string id;
    string today = "2023-06-23";

    public void getLogByDate(string dateString)
    {

        
        int allc = 0, doa = 0, acc = 0;
        int yValue = 0;
        string getlog =
            "select wo_name, set_set, set_count, do_set, do_count, cor_count from user_wo where user_id = '"
            + UserInfo.id
            + "' and date(do_date) = '"
            + dateString
            + "';";
        

        Transform[] childList = GameObject.Find("LogContent").GetComponentsInChildren<Transform>();
        if (childList != null) {
            for (int i = 1; i < childList.Length; i++) {
                if (childList[i] != transform) Destroy(childList[i].gameObject);
            }
        }

        try
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(getlog, conn);
            
            int cnt = 0;

            using (MySqlDataReader drd = cmd.ExecuteReader())
            {
                
                while (drd.Read())
                {   
                    cnt++; 

                    // instantiate
                    GameObject item = MonoBehaviour.Instantiate(woLog, content.transform);
                    item.name = "clone";
                    item.transform.position = new Vector3(0, yValue, 0);

                    //필요 값 계산
                    string woname = drd.GetString(0);
                    //실제 수행 횟수
                    if(drd.GetInt32(3) == 0){
                        allc = drd.GetInt32(4);
                    }
                    else {
                        allc = (drd.GetInt32(3) - 1) * drd.GetInt32(2) + drd.GetInt32(4);
                    }
                    //목표 수행 횟수
                    int arc = drd.GetInt32(1) * drd.GetInt32(2);
                    //정확한 수행 횟수
                    int coc = drd.GetInt32(5);
                    //운동 정확도
                    if(allc == 0){
                        acc = 0;
                    }
                    else{
                        if(coc == 0){
                            acc = 0;
                        }
                        else{
                            acc = (coc * 100) / allc;
                        }
                    }
                    
                    //운동 달성도
                    if(allc == 0){
                        doa = 0;
                    }
                    else{
                        doa = (allc * 100) / arc;
                    }

                    Debug.Log(woname);

                    // 값 저장
                    item.transform.Find("WOInfo/WONameTxt").GetComponent<TextMeshProUGUI>().text = woname;
                    item.transform.Find("WOInfo/WOAcuFrame/WOAcSlider").GetComponent<Slider>().value = acc;
                    item.transform.Find("WOInfo/WOGoalFrame/WOGoalSlider").GetComponent<Slider>().value = doa;

                    yValue -= 650;
                }
            }
            conn.Close();
            flatCalendar.SendMessage("setEventNum", cnt);
        } catch (Exception ex) {
            Debug.Log(ex.ToString());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        conn = new MySqlConnection(
            "SERVER = 175.114.255.210; port = 3306; DATABASE = capstone; UID = tester; PWD = P@ssw0rd;"
        );
        content = GameObject.Find("LogContent");
        woLog = Resources.Load("Prefabs/WOLog") as GameObject;
        flatCalendar = GameObject.Find("FlatCalendar");
        if(content == null){
            Debug.Log("content is null");
        } if(woLog == null){
            Debug.Log("woLog is null");
        }

        //getLogByDate("2023-06-23");
    }

    // Update is called once per frame
    void Update() {

    }
}
