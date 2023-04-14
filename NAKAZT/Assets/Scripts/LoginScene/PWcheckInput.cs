using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PWcheckInput : MonoBehaviour
{
    TextMeshProUGUI PWDupliChecker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 비밀 번호 체크 함수
    public void ValueChanged(TMP_InputField pw)
    {
        // PWDuplicationChecker 값 불러오기
        GameObject PWDuplicationChecker = GameObject.Find("PWDuplicationChecker");
        PWDupliChecker = PWDuplicationChecker.GetComponent<TextMeshProUGUI>();

        TMP_InputField pwCheck = gameObject.GetComponent<TMP_InputField>();
        Debug.Log(pwCheck.text);

        // 비밀 번호 체크
        if(pw.text == pwCheck.text){
            PWDupliChecker.text = "비밀 번호 같음";
            PWDupliChecker.color = new Color32(0, 0, 255, 255);
        }
        else{
            PWDupliChecker.text = "비밀 번호 다름!";
            PWDupliChecker.color = new Color32(255, 0, 0, 255);
        }
    }
}
