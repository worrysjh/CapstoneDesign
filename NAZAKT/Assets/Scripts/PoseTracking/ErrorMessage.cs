using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ErrorMessage : MonoBehaviour
{
    [SerializeField] private GameObject TxtBox;
    TextMeshProUGUI txtBox;
    public bool showing = false;
    public int errorCode;
    public float time = 0;
    public float _size = 5;
    public float _hold_time = 3f;
    public float _upSizeTime = 0.2f;
    public float _fadeTime = 1f;

    void Bounce() {
        if(time <= _upSizeTime){
            transform.localScale = Vector3.one * (1 + _size * time);
        }
        else if (time <= _upSizeTime*2)
        {
            transform.localScale = Vector3.one * (2*_size * _upSizeTime + 1 - time * _size);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
        time += Time.deltaTime;
    }

    public void FadeOut() {
        if(time < (_fadeTime + _hold_time) && time > _hold_time)
        {
            GetComponent<Image>().color = new Color(1, 1, 1, 1f - (time - _hold_time) / (_fadeTime));
            transform.Find("ErrMsgTxt").gameObject.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f - (time - _hold_time) / (_fadeTime));
        }
        else
        {
            restart();
            showing = false;
            this.gameObject.SetActive(false);
        }
        time += Time.deltaTime;
    }

    public void ShowMsg(int errorCode, string message) {
        showing = true;
        this.errorCode = errorCode;
        txtBox.text = message;
        time = 0;
    }

    public void restart() {
        if (time < 0.4f) {
            return;
        } else {
            time = 0.4f;
            GetComponent<Image>().color = Color.white;
            transform.Find("ErrMsgTxt").gameObject.GetComponent<TextMeshProUGUI>().color = Color.black;
        }
    }

    void Start()
    {
        txtBox = TxtBox.GetComponent<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }

    void Update()
    {
        if(time < _upSizeTime*2){
            Bounce();
        } else if(time >= _hold_time){
            FadeOut();
        }
        else {
            time += Time.deltaTime;
        }
    }

}
