using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class WorkOutManager : MonoBehaviour
{
    [SerializeField] private GameObject WOInfomation;
    [SerializeField] private GameObject stateTxt;
    [SerializeField] private GameObject countTxt;

    TextMeshProUGUI stateText;
    TextMeshProUGUI countText;
    TextMeshProUGUI CurSet;
    TextMeshProUGUI TargetSet;
    TextMeshProUGUI TargetCnt;

    int curstate;
    int curCount;

    // Start is called before the first frame update
    void Start()
    {
        stateText = stateTxt.GetComponent<TextMeshProUGUI>();
        countText = countTxt.GetComponent<TextMeshProUGUI>();
        TargetSet = GameObject.Find("TargetSet").GetComponent<TextMeshProUGUI>();
        TargetCnt = GameObject.Find("TargetCnt").GetComponent<TextMeshProUGUI>();
        CurSet = GameObject.Find("CurSet").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        setState();
        setCount();

        setWOInfo();
    }

    void setState(){
        curstate = WOInfomation.GetComponent<WOInfo>().currentState;
        stateText.text = "상태 : " + curstate.ToString();
    }

    void setCount(){
        curCount = WOInfomation.GetComponent<WOInfo>().correctCount + WOInfomation.GetComponent<WOInfo>().incorrectCount;
        countText.text = curCount.ToString();
    }
    
    void setWOInfo(){
        WOInfomation.GetComponent<WOInfo>().SetNum = int.Parse(TargetSet.text);
        WOInfomation.GetComponent<WOInfo>().CntNum = int.Parse(TargetCnt.text);
        CurSet.text = WOInfomation.GetComponent<WOInfo>().curSetNum.ToString();
    }
}