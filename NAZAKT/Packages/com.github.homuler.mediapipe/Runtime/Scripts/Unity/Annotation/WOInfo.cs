using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Constants
{
    public const int MAX_ERROR_COUNT = 3; // 최대 에러 개수
    public const int KNEE_FALLING_OVER_TOES = 0;
    public const int CYCLIC_FROM_S2_S1 = 1;
    public const int DEEP_SQUATS = 2;
}

public class WOInfo : MonoBehaviour
{
    public float time;
    public int currentState;
    public int inccorectPoseCount;
    public int[] ErrorTime;

    public int CntNum;              // 사용자가 설정한 카운트 수
    public int SetNum;              // 사용자가 설정한 세트 수

    public int curSetNum;           // 현재 세트 수
    
    public int countSum;            // 현재 카운트 수 (correctCount + incorrectCount)
    public int correctCount;
    public int incorrectCount;

    public bool isStarted;

    public GameObject TimerManager;             // 타이머

    public void SetState(int state){
        currentState = state;
    }

    public bool checkSetDone() {
        if(countSum >= CntNum) {
            return true;
        }
        else {
            return false;
        }
    }

    public void increaseCorrectCount(){
        correctCount++;
        Debug.Log("correctCount : " + correctCount.ToString());
        
        countSum++;

        Debug.Log("countSum : " + countSum.ToString());

        // 갯수 다 채웠는지 확인...
        if(checkSetDone()){
            TimerManager.SendMessage("timerStart");

            // 정지
            isStarted = false;

            Debug.Log("Timer Starts!");
        }
    }

    public void increseIncorrectCount(){
        incorrectCount++;
        Debug.Log("incorrectCount : " + incorrectCount.ToString());

        countSum++;

        // 갯수 다 채웠는지 확인...
        if(checkSetDone()){
            TimerManager.SendMessage("timerStart");

            // 시작
            isStarted = false;

            Debug.Log("Timer Starts!");
        }
    }

    // 세트 종료...
    public void endOfSet(){
        countSum = 0;
        correctCount = 0;
        incorrectCount = 0;
        curSetNum++;

        isStarted = true;
    }

    public void DecreaswErrorCount(){
        for(int i = 0; i < Constants.MAX_ERROR_COUNT; i++)
            if(ErrorTime[i] > 0) 
                ErrorTime[i]--;
    }

    public void ErrorState(int code){
        switch(code){
            case 0:
                ErrorTime[Constants.KNEE_FALLING_OVER_TOES] = 10;
                inccorectPoseCount++;
                break;
            case 1:
                ErrorTime[Constants.CYCLIC_FROM_S2_S1] = 10;
                inccorectPoseCount++;
                break;
            case 2:
                ErrorTime[Constants.DEEP_SQUATS] = 10;
                inccorectPoseCount++;
                break;

            default:
                break;
        }
    }

    // 운동 종료
    public void isEnd(){
        isStarted = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        time = 1f;
        ErrorTime = new int[Constants.MAX_ERROR_COUNT];
        correctCount = 0;
        incorrectCount = 0;
        countSum = 0;

        curSetNum = 1;
        
        isStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
                time -= Time.deltaTime;
            else{
                DecreaswErrorCount();
                time = 0.1f;
            }
    }
}
