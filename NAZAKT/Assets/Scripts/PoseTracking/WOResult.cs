using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WOResult : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Find("WONameTxt").GetComponent<TextMeshProUGUI>().text = GameObject.Find("WONameTxt").GetComponent<TextMeshProUGUI>().text;

        // 목표 세트 수...
        gameObject.transform.Find("WOSetTxt").GetComponent<TextMeshProUGUI>().text = GameObject.Find("TargetSet").GetComponent<TextMeshProUGUI>().text;

        // 목표 카운트 수...
        gameObject.transform.Find("WOCntTxt").GetComponent<TextMeshProUGUI>().text = (int.Parse(GameObject.Find("TargetSet").GetComponent<TextMeshProUGUI>().text) * int.Parse(GameObject.Find("TargetCnt").GetComponent<TextMeshProUGUI>().text)).ToString();
        
        // 수행 세트 수...
        gameObject.transform.Find("WODoSetCntTxt").GetComponent<TextMeshProUGUI>().text = (int.Parse(GameObject.Find("CurSet").GetComponent<TextMeshProUGUI>().text) - 1).ToString();
        
        // 수행 카운트 수...
        gameObject.transform.Find("WODoCntTxt").GetComponent<TextMeshProUGUI>().text = ((int.Parse(GameObject.Find("CurSet").GetComponent<TextMeshProUGUI>().text) - 1) * int.Parse(GameObject.Find("TargetCnt").GetComponent<TextMeshProUGUI>().text) + int.Parse(GameObject.Find("CurCnt").GetComponent<TextMeshProUGUI>().text)).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void homeBtnisClicked(){
        SceneManager.LoadScene("Scenes/Nazakt", LoadSceneMode.Single);
        Application.Quit();
    }
}
