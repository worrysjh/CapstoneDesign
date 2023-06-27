using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI min;
    public TextMeshProUGUI sec;
    Timerstatus timerstatus;

    GameObject WOInfo;

    GameObject curSet;
    GameObject targetSet;
    GameObject woManager;

    TextMeshProUGUI countDownTxt;

    float setTime;

    float time; // 시간

    void Awake(){
        timerstatus = GameObject.Find("Timerstatus").GetComponent<Timerstatus>();
        WOInfo = GameObject.Find("WOInfo");

        countDownTxt = GameObject.Find("CountDownTxt").GetComponent<TextMeshProUGUI>();
        countDownTxt.color = new Color(255, 255, 255, 0);

        // 세트 휴식 시간 20초
        setTime = 10;

        // 시간 초기화...
        time = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        curSet = GameObject.Find("CurSet");
        targetSet = GameObject.Find("TargetSet");
        woManager = GameObject.Find("WorkoutManager");
    }

    // Update is called once per frame
    void Update()
    {
        if(timerstatus.isStart){
            time -= Time.deltaTime;

            // 시간이 다 지나면
            if(time <= 0) {
                countDownTxt.color = new Color(255, 0, 0, 0);
                timerStop();
            } else {
                sec.text = ((int)time % 60 + 1).ToString();
                min.text = ((int)time / 60%60).ToString();

                // 시간 설정
                if((int)time % 60 < 5){
                    countDownTxt.color = new Color(255, 0, 0, 255);

                    if(time % 1 < 0.5f){
                        countDownTxt.color = new Color(1, 0, 0, (time % 1) * 2);
                    } else{
                        countDownTxt.color = new Color(1, 0, 0, 1);
                    }
                    countDownTxt.text = "" + (Int32.Parse(sec.text));
                }
            }           
        }
    }

    public void timerStart(){
        // 타이머 시작..
        if(int.Parse(curSet.GetComponent<TextMeshProUGUI>().text) == int.Parse(targetSet.GetComponent<TextMeshProUGUI>().text) - 1 ){
            curSet.GetComponent<TextMeshProUGUI>().text = "" + (int.Parse(curSet.GetComponent<TextMeshProUGUI>().text) + 1);
            woManager.SendMessage("checkEnd");
            return;
        }

        timerstatus.isStart = true;

        // 타이머 시간 설정..
        time = setTime;

        sec.text = ((int)time % 60 ).ToString();
        min.text = ((int)time / 60%60).ToString();
    }


    public void timerStop(){
        Debug.Log("timerStop");

        // 타이머 정지..
        timerstatus.isStart = false;

        // 타이머 시간 설정..
        time = 0;

        sec.text = "--";
        min.text = "--";

        // 현재 세트 운동 정보 초기화...
        WOInfo.SendMessage("endOfSet");
    }
}
