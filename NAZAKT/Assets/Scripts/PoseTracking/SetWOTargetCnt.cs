using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class SetWOTargetCnt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        setWOTargetCnt();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // WOStatus에 담긴 정보 가져오기...
    void setWOTargetCnt(){
        WOStatus WOStatus = GameObject.Find("WOStatus").GetComponent<WOStatus>();
        gameObject.GetComponent<TextMeshProUGUI>().text = WOStatus.cntNum.ToString();
    }
}
