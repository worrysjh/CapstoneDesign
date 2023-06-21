using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class SetWONameTxt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        setWONameTxt();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // WOStatus에 담긴 정보 가져오기... 
    void setWONameTxt(){

        // test
        if(GameObject.Find("WOStatus") == null){ 
            gameObject.GetComponent<TextMeshProUGUI>().text = "debug";
            return;
        }

        WOStatus WOStatus = GameObject.Find("WOStatus").GetComponent<WOStatus>();
        gameObject.GetComponent<TextMeshProUGUI>().text = WOStatus.WOname;
    }
}
