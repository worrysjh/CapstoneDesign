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
    public int correctCount;
    public int incorrectCount;

    public void SetState(int state){
        currentState = state;
    }

    public void increaseCorrectCount(){
        correctCount++;
        Debug.Log("correctCount : " + correctCount.ToString());
    }

    public void increseIncorrectCount(){
        incorrectCount++;
        Debug.Log("incorrectCount : " + incorrectCount.ToString());
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

    // Start is called before the first frame update
    void Start()
    {
        time = 1f;
        ErrorTime = new int[Constants.MAX_ERROR_COUNT];
        correctCount = 0;
        incorrectCount = 0;
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
