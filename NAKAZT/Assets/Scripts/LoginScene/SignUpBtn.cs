using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SignUpBtn : MonoBehaviour
{
    public TMP_InputField IDInput;
    public TMP_InputField PWInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void signUpBtnOnClicked(){
        // 오브젝트 가져오기
        // TMP_InputField IDInput = GameObject.Find("IDInput").GetComponent<TMP_InputField>();
        // TMP_InputField PWInput = GameObject.Find("PWInput").GetComponent<TMP_InputField>();
        TMP_InputField HeightInput = GameObject.Find("HeightInput").GetComponent<TMP_InputField>();
        TMP_InputField WeightInput = GameObject.Find("WeightInput").GetComponent<TMP_InputField>();

        Debug.Log("ID : " + IDInput.text + ", PW : " + PWInput.text + ", 키 : " + HeightInput.text + ", 몸무게 : " + WeightInput.text);
    }
}
