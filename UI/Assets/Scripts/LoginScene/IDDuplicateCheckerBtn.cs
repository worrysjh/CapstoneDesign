using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IDDuplicateCheckerBtn : MonoBehaviour
{
    string UserId = null;                        // 사용자가 입력한 ID
    TextMeshProUGUI AvailabilityTxt = null;      // 사용 가능 여부 TXT

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        
    }

    public void IDDuplicateCheckerBtnOnClicked(TMP_InputField ip){
        //입력 필드값 가져오기...
        UserId = ip.text;
        Debug.Log(ip.text);
        
        // IDDuplicateTxt(중복 확인 텍스트) 정보 불러오기...
        GameObject IDDuplicateTxt = GameObject.Find("IDDuplicateTxt");
        AvailabilityTxt = IDDuplicateTxt.GetComponent<TextMeshProUGUI>();

        //DB에서 아이디 중복 체크 기능
        if(UserId == "aaa"){
            AvailabilityTxt.color = new Color32(255, 0, 0, 255);
            AvailabilityTxt.text = "사용 불가";
            Debug.Log(AvailabilityTxt.text);
        }
        else{
            AvailabilityTxt.color = new Color32(0, 0, 255, 255);
            AvailabilityTxt.text = "사용 가능";
            Debug.Log(AvailabilityTxt.text);
        }
    }
}
