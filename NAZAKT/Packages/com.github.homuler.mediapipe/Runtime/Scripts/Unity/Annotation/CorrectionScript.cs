using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using UnityEngine.UI;

namespace Mediapipe.Unity
{
    public class CorrectionScript : MonoBehaviour
    {
        [SerializeField] private GameObject woInfo;
        [SerializeField] private GameObject connectionListAnnotation1;
        [SerializeField] private GameObject connectionListAnnotation2;
        private float time;
        public LandmarkList target;
        private WOInfo info;
        private ConnectionListAnnotation connectionList1;
        private ConnectionListAnnotation connectionList2;
        private int currentState; // ?????? ?? ???? ????(1, 2, 3)
        private bool isSquattingDown; // ????? ????? ???????? ??? ??????, State3?? ???? ?? False, State1?? ???? ?? True?? ????

        // Start is called before the first frame update
        void Start()
        {
            time = 1f;
            currentState = 1; // ?? ???? ???? ????
            connectionList1 = connectionListAnnotation1.GetComponent<ConnectionListAnnotation>();
            connectionList2 = connectionListAnnotation2.GetComponent<ConnectionListAnnotation>();
            info = woInfo.GetComponent<WOInfo>();
        }

        // Update is called once per frame
        void Update()
        {
            if (time > 0)
                time -= Time.deltaTime;
            else{
                CheckPose();
                time = 0.1f;
            }
        }

        //Get Vector3 by Landmark number
        Vector3 GetVector(int num){
            Vector3 vector = new Vector3((target?.Landmark)[num].X, (target?.Landmark)[num].Y, (target?.Landmark)[num].Z);
            return vector;
        }

        // Calculate angle with Landmark datas
        // num3 = -1 ?? ???? y??? ?????? ?????
        float CalcAngle(int num1, int num2, int num3){
            Vector3 VA, VB;
            if (num3 == -1){
                VA = GetVector(num1) - GetVector(num2);
                VB = new Vector3(0, -0.1f, 0);
            } else{
                VA = GetVector(num1) - GetVector(num2);
                VB = GetVector(num3) - GetVector(num2);
            }
            float angle = Vector3.Angle(VA, VB);
            return angle;
        }

        // num ??�� Connection?? ???? ?????????? ??????
        void ChangeToRed(int num){
            if (!this.connectionList1.wrongNumbers.Contains(num)){
                this.connectionList1.wrongNumbers.Add(num);
                this.connectionList2.wrongNumbers.Add(num);
            }
        }

        // num ??�� Connection?? ???? ??????? ??????
        void ChangeToWhite(int num){
            if (this.connectionList1.wrongNumbers.Contains(num)){
                this.connectionList1.wrongNumbers.Remove(num);
                this.connectionList2.wrongNumbers.Remove(num);
            }
        }

        void CheckPose(){
            if (target != null)
            {
                /*
                States Overview
                (0 ~ 32)
                State s1:   If the angle between the knee and the vertical falls within 32??,
                            then it is in the Normal phase, and its state is s1. It is essentially the state
                            where the counters for proper and improper squats are updated.
                (35 ~ 65)
                State s2:   If the angle between the knee and the vertical falls between 35?? and 65??,
                            it is in the Transition phase and subsequently goes to state s2.
                (75 ~ 95)
                State s3:   If the angle between the knee and the vertical lies within a specific range
                            (say, between 75?? and 95??), it is in the Pass phase and subsequently goes to state s3.
                */
                
                /*
                Incorrect Squats
                Case 1 : with knee falling over toes
                Case 2 : cyclic from state s1 to s2 and again s1
                Case 3 : deep squats
                (Case 4 : Forntal View Warning)
                */

                float left_knee_angle, right_knee_angle;

                switch (currentState){
                    case 1:
                        left_knee_angle = CalcAngle(23, 25, -1); // Calculate left_knee_angle (vertical)
                        right_knee_angle = CalcAngle(24, 26, -1); // Calculate right_knee_angle (vertical)
                        info.currentState = 1;

                        // State1 -> State2
                        if (!isSquattingDown){ // State3???? ?????? ?? ??? case?? ?????? ???
                            // Correct Count up
                            isSquattingDown = true;
                        }

                        if ((35 < left_knee_angle && left_knee_angle < 65) &&  (35 < right_knee_angle && right_knee_angle < 65)){
                            currentState = 2;
                            info.SetState(currentState);
                        }
                        break;

                    case 2:
                        left_knee_angle = CalcAngle(23, 25, -1); // Calculate left_knee_angle (vertical)
                        right_knee_angle = CalcAngle(24, 26, -1); // Calculate right_knee_angle (vertical)
                        
                        // State2 -> State1
                        if ((left_knee_angle < 32) && (right_knee_angle < 32) && (!isSquattingDown)){ // State1?? ????
                            currentState = 1;
                            info.SetState(currentState);
                        } else if ((left_knee_angle < 32) && (right_knee_angle < 32) && (isSquattingDown)){
                            // Incorrect Count up : cyclic from state s1 to s2 and again s1
                            currentState = 1;
                            info.SetState(currentState);
                        }
                        
                        // State2 -> State3
                        if ((75 < left_knee_angle && left_knee_angle < 95) && (75 < right_knee_angle && right_knee_angle < 95) && (isSquattingDown)){
                            currentState = 3;
                            info.SetState(currentState);
                            isSquattingDown = false;
                        } else if ((75 < left_knee_angle && left_knee_angle < 95) && (75 < right_knee_angle && right_knee_angle < 95) && (!isSquattingDown)){
                            // ???? ???? ?????? ??�� ???? ???
                            currentState = 3;
                            info.SetState(currentState);
                        }
                        break;

                    case 3:
                        left_knee_angle = CalcAngle(23, 25, -1); // Calculate left_knee_angle (vertical)
                        right_knee_angle = CalcAngle(24, 26, -1); // Calculate right_knee_angle (vertical)

                        // Too deep Squat
                        if ((95 < left_knee_angle) || (95 < right_knee_angle)){
                            // Print "Too Deep Squat" Message
                            ChangeToRed(30);
                        }
                        
                        // State3 -> State2
                        if ((35 < left_knee_angle && left_knee_angle < 65) &&  (35 < right_knee_angle && right_knee_angle < 65)){
                            ChangeToWhite(30);
                            currentState = 2;
                            info.SetState(currentState);
                        }
                        break;
                }
            }
        }
    }
}