using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

using TMPro;


public class WOStartBtn : MonoBehaviour
{

    public TextMeshProUGUI WOnameTxt;
    public TMP_Dropdown setDrop;
    public TMP_Dropdown cntDrop;
    public string userID;

    public WOStatus WOStatus;



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
        SceneManager.LoadScene(0);
        SetWOInfo();
        DontDestroyOnLoad(WOStatus); // 운동 정보 담은 오브젝트 넘기기
    }

    // 운동 정보 세팅 메소드
    public void SetWOInfo(){
        Debug.Log("ID : " + UserInfo.id);

        WOStatus.userID = UserInfo.id;
        WOStatus.WOname = WOnameTxt.text;
        WOStatus.setNum = setDrop.value + 1;
        WOStatus.cntNum = cntDrop.value + 1;
        WOStatus.date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


        // Debug.Log("운동명 : " + WOStatus.WOname);
        // Debug.Log("세트 수 : " + WOStatus.setNum);
        // Debug.Log("횟수 : " + WOStatus.cntNum);
    }
}
