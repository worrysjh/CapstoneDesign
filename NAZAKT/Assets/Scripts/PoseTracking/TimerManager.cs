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

    float time; // 시간

    void Awake(){
        timerstatus = GameObject.Find("Timerstatus").GetComponent<Timerstatus>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timerstatus.isStart){
            time += Time.deltaTime;
            sec.text = ((int)time % 60 ).ToString();
            min.text = ((int)time / 60%60).ToString();
        }
    }

    public void timerStartBtnOnclicked(){

        // 시작 버튼...
        Button start = gameObject.GetComponent<Button>();
        start.interactable = false; // 시작버튼 비활성화

        // 시작버튼 색 변경
        ColorBlock col = start.colors;
        col.normalColor = new Color(200, 200, 200, 255); // 회색
        start.colors = col;

        // 정지 버튼...
        Button stop = GameObject.Find("TimerStopBtn").GetComponent<Button>();
        stop.interactable = true; // 정지버튼 활성화

        // 정지버튼 색 변경
        col = stop.colors;
        col.normalColor = new Color(0, 0, 0, 255); // 검정색
        stop.colors = col;

        // 타이머 시작..
        timerstatus.isStart = true;
        Debug.Log("Timer Starts!");
    }

    public void timerStopBtnOnclicked(){
        min.text = "00";
        sec.text = "00";

        // 정지 버튼...
        Button stop = gameObject.GetComponent<Button>();
        stop.interactable = false; // 정지버튼 비활성화

        // 정지버튼 색 변경
        ColorBlock col = stop.colors;
        col.normalColor = new Color(200, 200, 200, 255); // 회색
        stop.colors = col;

        // 시작 버튼...
        Button start = GameObject.Find("TimerStartBtn").GetComponent<Button>();
        start.interactable = true; // 시작버튼 활성화

        // 시작버튼 색 변경
        col = start.colors;
        col.normalColor = new Color(0, 0, 0, 255); // 검정색
        start.colors = col;

        // 타이머 종료..
        timerstatus.isStart = false;

        // 시간 초기화
        time = 0;

        Debug.Log("Timer Stops!");
    }
}
