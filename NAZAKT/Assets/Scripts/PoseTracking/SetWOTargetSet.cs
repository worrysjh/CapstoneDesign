using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class SetWOTargetSet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        setWOTargetSet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // WOStatus에 담긴 정보 가져오기...
    void setWOTargetSet(){

        // test
        if(GameObject.Find("WOStatus") == null){ 
            gameObject.GetComponent<TextMeshProUGUI>().text = "5";
            return;
        }

        WOStatus WOStatus = GameObject.Find("WOStatus").GetComponent<WOStatus>();
        gameObject.GetComponent<TextMeshProUGUI>().text = WOStatus.setNum.ToString();
    }
}
