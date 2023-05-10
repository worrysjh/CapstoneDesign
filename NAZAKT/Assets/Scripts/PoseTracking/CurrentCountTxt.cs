using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class CurrentCountTxt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setCurrentCountTxt(){
        WOStatus WOStatus = GameObject.Find("WOStatus").GetComponent<WOStatus>();
        gameObject.GetComponent<TextMeshProUGUI>().text = WOStatus.WOname;
    }
}
