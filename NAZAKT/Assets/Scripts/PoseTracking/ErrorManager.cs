using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorManager : MonoBehaviour
{
    // error message 
    private string[] ERR = {
        "무릎이 발 너머로 나감",
        "너무 살짝 앉음",
        "너무 깊게 앉음",
        "에러4",
        "에러5"
        };

    [SerializeField] private GameObject ErrMsg1;
    [SerializeField] private GameObject ErrMsg2;
    [SerializeField] private GameObject ErrMsg3;
    [SerializeField] private GameObject ErrMsg4;

    private ErrorMessage[] ErrorMsgList;
    int[] msgField = new int[4];

    bool isShowing(int errorCode) {
        for (int i = 0; i < ErrorMsgList.Length; i++) {
            if (ErrorMsgList[i].showing && ErrorMsgList[i].errorCode == errorCode) {
                ErrorMsgList[i].restart();
                return true;
            }
        }
        return false;
    }

    int AvilableMsg(int errorCode) {
        for (int i = 0; i < ErrorMsgList.Length; i++) {
            if (!ErrorMsgList[i].showing) {
                return i;
            }
        }
        return 0;
    }

    public void ShowError(int errorCode) {

        if (isShowing(errorCode)) {
            return;
        }

        switch (AvilableMsg(errorCode)) {
            case 0:
                ErrMsg1.SetActive(true);
                break;
            case 1:
                ErrMsg2.SetActive(true);
                break;
            case 2:
                ErrMsg3.SetActive(true);
                break;
            case 3:
                ErrMsg4.SetActive(true);
                break;
        }

        ErrorMsgList[AvilableMsg(errorCode)].ShowMsg(errorCode, ERR[errorCode]);
    }
    
    void Start()
    {
        // connect ErrorMessage gameobject
        ErrorMsgList = new ErrorMessage[4];
        ErrorMsgList[0] = ErrMsg1.GetComponent<ErrorMessage>();
        ErrorMsgList[1] = ErrMsg2.GetComponent<ErrorMessage>();
        ErrorMsgList[2] = ErrMsg3.GetComponent<ErrorMessage>();
        ErrorMsgList[3] = ErrMsg4.GetComponent<ErrorMessage>();
    }

    void Update()
    {
        
    }
}
