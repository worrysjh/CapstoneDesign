using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class ExerciseExplainer : MonoBehaviour
{
    public GameObject img;
    public GameObject txt;
    public GameObject WOName;
    public GameObject startBtn;
    Sprite[] sprites;
    string[] explain;
    int index;

    // Start is called before the first frame update
    void Start()
    {   
        index = 0;
        sprites = Resources.LoadAll<Sprite>("Img/WOImg/squat");
        initExplain();
        img.GetComponent<Image>().sprite = sprites[index];
        txt.GetComponent<TextMeshProUGUI>().text = explain[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetExerciseExplainer(){
        index = 0;
        startBtn.SetActive(false);
        img.GetComponent<Image>().sprite = sprites[index];
        txt.GetComponent<TextMeshProUGUI>().text = explain[index];
    }

    void initExplain(){
        // 운동 이름 초기화
        transform.Find("Explaintext").GetComponent<TextMeshProUGUI>().text =  WOName.GetComponent<TextMeshProUGUI>().text + " 설명";
       
        // 운동 설명 초기화
        explain = new string[] {
            "내려가기 전 완전히 일어난 상태",
            "내려가는 도중의 상태(완전히 앉은 상태가 아님)",
            "충분히 내려가 다시 올라가기 직전의 상태"
        };
    }

    // 오른쪽 버튼 눌렀을 시
    public void FlipRightBtn(){
        if(index >= sprites.Length - 1){
            return;
        }
        else {
            // 이미지 설정
            img.GetComponent<Image>().sprite = sprites[++index];

            // 설명 설정
            txt.GetComponent<TextMeshProUGUI>().text = explain[index];

            // 마지막 페이지
            if(index >= sprites.Length - 1){
                startBtn.SetActive(true);
            }
        }
    }
    
    // 왼쪽 버튼 눌렀을 시
    public void FlipLeftBtn(){
        if(index <= 0){
            return;
        }
        else {
            // 이미지 설정
            img.GetComponent<Image>().sprite = sprites[--index];

            // 설명 설정
            txt.GetComponent<TextMeshProUGUI>().text = explain[index];

            // 마지막 페이지
            if(index <= sprites.Length - 1){
                startBtn.GetComponent<BounceAnim>().resetAnim();
                startBtn.SetActive(false);
            }
        }
    }
}
