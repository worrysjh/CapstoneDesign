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

    float setTime;

    float time; // 시간

    void Awake(){
        timerstatus = GameObject.Find("Timerstatus").GetComponent<Timerstatus>();
        WOInfo = GameObject.Find("WOInfo");

        // 휴식 시간 20초
        setTime = 5;

        // 시간 초기화...
        time = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if(timerstatus.isStart){
            time -= Time.deltaTime;

            // 시간이 다 지나면
            if(time <= 0) {
                timerStop();
            } else {
                sec.text = ((int)time % 60 ).ToString();
                min.text = ((int)time / 60%60).ToString();
            }           
        }
    }

    public void timerStart(){
        // 타이머 시작..
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
