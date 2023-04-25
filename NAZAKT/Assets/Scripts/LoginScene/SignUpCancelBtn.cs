using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SignUpCancelBtn : MonoBehaviour
{
    public TMP_InputField IDInput;
    public TMP_InputField PWInput;
    public TMP_InputField PWcheckInput;
    public TMP_InputField NameInput;
    public TextMeshProUGUI PWDuplicationChecker;
    public TextMeshProUGUI IDDuplicateTxt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cancelBtnOnClicked(){
        // 데이터 초기화...
        IDInput.text = "";
        PWInput.text = "";
        NameInput.text = "";
        PWcheckInput.text = "";
        PWDuplicationChecker.text = "";
        IDDuplicateTxt.text = "";
        
    }
}
