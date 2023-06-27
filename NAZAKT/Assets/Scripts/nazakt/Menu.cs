using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Menu : MonoBehaviour
{
    List<GameObject> btnList;

    void init(){
        btnList = new List<GameObject>();
        btnList.Add(transform.Find("StartBtn").gameObject);
        btnList.Add(transform.Find("WOLogBtn").gameObject);
        btnList.Add(transform.Find("UserInfoBtn").gameObject);
    }

    public void btnClicked(int num){
        Debug.Log(btnList[num] + " is clicked");
        
        GameObject obj = btnList[num];

        obj.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().fontSize = 58;

        for(int i = 0; i < btnList.Count; i++){
            if(i == num) continue;
            else{
                btnList[i].transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().fontSize = 50;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
