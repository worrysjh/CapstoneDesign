using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System;

public class WOResult : MonoBehaviour
{

    public static MySqlConnection conn = new MySqlConnection("SERVER = 175.114.255.210; port = 3306; DATABASE = capstone; UID = tester; PWD = P@ssw0rd;");

    public GameObject woinfo;
    GameObject woManager;
    public ErrorManager errorManager;

    public void homeBtnisClicked(){

        WOStatus WOStatus = GameObject.Find("WOStatus").GetComponent<WOStatus>();
        try {            
            string startwoquery = "insert into user_wo(user_id, wo_name, set_set, set_count, do_set, do_count, cor_count, do_date) value ('" + WOStatus.userID + "', '" + WOStatus.WOname + "', " + WOStatus.setNum + ", " + WOStatus.cntNum + ", " + gameObject.transform.Find("WODoSetCntTxt").GetComponent<TextMeshProUGUI>().text + ", " + gameObject.transform.Find("WODoCntTxt").GetComponent<TextMeshProUGUI>().text + ", " + woManager.transform.Find("Corect").GetComponent<TextMeshProUGUI>().text + ", '" + WOStatus.date + "');";
            Debug.Log(startwoquery);


            conn.Open();
            MySqlCommand cmd = new MySqlCommand(startwoquery, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
            Debug.Log("성공");
        }
        catch (Exception ex) { 
            Debug.Log(ex.ToString());
        }
        SceneManager.LoadScene("Nazakt");
    }

    void init(){
        woManager = GameObject.Find("WorkoutManager");

        // 운동 이름
        gameObject.transform.Find("WONameTxt").GetComponent<TextMeshProUGUI>().text = GameObject.Find("WONameTxt").GetComponent<TextMeshProUGUI>().text;

        // 목표 세트 수...
        gameObject.transform.Find("WOSetTxt").GetComponent<TextMeshProUGUI>().text = GameObject.Find("TargetSet").GetComponent<TextMeshProUGUI>().text;

        // 목표 카운트 수...
        gameObject.transform.Find("WOCntTxt").GetComponent<TextMeshProUGUI>().text = (int.Parse(GameObject.Find("TargetSet").GetComponent<TextMeshProUGUI>().text) * int.Parse(GameObject.Find("TargetCnt").GetComponent<TextMeshProUGUI>().text)).ToString();
        
        // 수행 세트 수...
        gameObject.transform.Find("WODoSetCntTxt").GetComponent<TextMeshProUGUI>().text = (int.Parse(GameObject.Find("CurSet").GetComponent<TextMeshProUGUI>().text) + 1).ToString();
        
        // 수행 카운트 수...
        gameObject.transform.Find("WODoCntTxt").GetComponent<TextMeshProUGUI>().text = ((int.Parse(GameObject.Find("CurSet").GetComponent<TextMeshProUGUI>().text)) * int.Parse(GameObject.Find("TargetCnt").GetComponent<TextMeshProUGUI>().text) + int.Parse(GameObject.Find("CurCnt").GetComponent<TextMeshProUGUI>().text)).ToString();

        // 정확도...
        gameObject.transform.Find("WOAcTxt").GetComponent<TextMeshProUGUI>().text = ((float.Parse(woManager.transform.Find("Corect").GetComponent<TextMeshProUGUI>().text) / (float.Parse(gameObject.transform.Find("WODoCntTxt").GetComponent<TextMeshProUGUI>().text)) * 100)).ToString("#.##") + "%";
        Debug.Log("수행한 횟수 : " + float.Parse(gameObject.transform.Find("WODoCntTxt").GetComponent<TextMeshProUGUI>().text));
        Debug.Log("맞은 횟수 : " + float.Parse(woManager.transform.Find("Corect").GetComponent<TextMeshProUGUI>().text));


        // 발생한 에러
        string errorItems = "";
        errorManager = GameObject.Find("ErrMsgFrame").GetComponent<ErrorManager>();
        for (int i = 0; i < errorManager.errorFlagList.Length; i++) {
            if (errorManager.errorFlagList[i]) {
                if (errorItems != "") {
                    errorItems += ", "; 
                }
                errorItems += errorManager.ERR[i];
            }
        }
        gameObject.transform.Find("ErrItem").GetComponent<TextMeshProUGUI>().text = errorItems;
    }

    void setCorectCnt(int cnt){
        gameObject.transform.Find("Corect").GetComponent<TextMeshProUGUI>().text = "" + cnt;
    }

    void setInCorectCnt(int incnt){
        gameObject.transform.Find("InCorect").GetComponent<TextMeshProUGUI>().text = "" + incnt;
    }

    // Start is called before the first frame update
    void Start()
    {
        woinfo.SendMessage("isEnd");
        init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}