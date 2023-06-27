using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class test : MonoBehaviour
{
    public GameObject cnt;
    public TextMeshProUGUI TargetSet;
    public TextMeshProUGUI TargetCnt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cntUp(){
        cnt.SendMessage("increaseCorrectCount");
    }

    public void incntUp(){
        cnt.SendMessage("increseIncorrectCount");
    }

    public void initAll(){
        TargetSet.text = "5";
        TargetCnt.text = "5";
    }

    public void showWOResult(){
        GameObject.Find("WOResultFrame").SetActive(true);
    }
}
