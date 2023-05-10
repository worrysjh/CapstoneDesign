using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class WorkOutManager : MonoBehaviour
{
    [SerializeField] private GameObject WOInfomation;
    [SerializeField] private GameObject stateTxt;

    TextMeshProUGUI stateText;

    int curstate;

    // Start is called before the first frame update
    void Start()
    {
        stateText = stateTxt.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        setState();
    }

    void setState(){
        curstate = WOInfomation.GetComponent<WOInfo>().currentState;
        stateText.text = "상태 : " + curstate.ToString();
    }
}
