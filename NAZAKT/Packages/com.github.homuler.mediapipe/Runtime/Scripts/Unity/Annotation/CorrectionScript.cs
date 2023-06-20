using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using UnityEngine.UI;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System;

namespace Mediapipe.Unity
{
    public class CorrectionScript : MonoBehaviour
    {
        [SerializeField] private GameObject woInfo;
        [SerializeField] private GameObject connectionListAnnotation1;
        [SerializeField] private GameObject connectionListAnnotation2;
        [SerializeField] private GameObject errMsgFrame;
        private float time;
        public LandmarkList target;
        private WOInfo info;
        private ConnectionListAnnotation connectionList1;
        private ConnectionListAnnotation connectionList2;
        private int currentState; // 1, 2, 3, 4
        private bool isSquattingDown;
        private bool isDeepSquat; // Deep Squats Flag

        public static MySqlConnection conn = new MySqlConnection("SERVER = 175.114.255.210; port = 3306; DATABASE = capstone; UID = tester; PWD = P@ssw0rd;");


        public void setCurrentState(int state){
            currentState = state;
        }

        // Start is called before the first frame update
        void Start()
        {
            time = 1f;
            currentState = 1;
            isSquattingDown = true;
            isDeepSquat = false;
            connectionList1 = connectionListAnnotation1.GetComponent<ConnectionListAnnotation>();
            connectionList2 = connectionListAnnotation2.GetComponent<ConnectionListAnnotation>();
            info = woInfo.GetComponent<WOInfo>();
        }

        void savelog(string error)
        {
            try {
                string loginsertquery = "insert into log_tb (user_id, wo_name, do_date, er_log) value
                ();"

                conn.Open();
                MySqlCommand cmd = new MySqlCommand(loginsertquery, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
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

        // Calculate angle with Landmark datas (num1 - num2 - num3)
        // when num3 = -1, calculate angle with y axis
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

        // Calculate angle of (num1 - middle of num2, num3 - num4)
        float CalcAngle(int num1, int num2, int num3, int num4){
            Vector3 VA, VB, Vcenter;
            Vcenter = GetVector(num2) + GetVector(num3);
            Vcenter /= 2;
            VA = GetVector(num1) - Vcenter;
            VB = GetVector(num4) - Vcenter;
            float angle = Vector3.Angle(VA, VB);
            return angle;
        }

        // Calculate angle of (middle of num1, num2 - middle of num3, num4 - y)
        // when num5 = -1, calculate angle with y axis (middle of num1, num2 - middle of num3, num4 - y)
        float CalcAngle(int num1, int num2, int num3, int num4, int num5){
            Vector3 VA, VB;
            if (num5 == -1){
                VA = ((GetVector(num1) + GetVector(num2)) - (GetVector(num3) + GetVector(num4))) / 2;
                VB = new Vector3(0, -0.1f, 0);
            } else {
                // temporary
                VA = GetVector(num1) - GetVector(num2);
                VB = GetVector(num3) - GetVector(num2);
            }
            float angle = Vector3.Angle(VA, VB);
            //Debug.Log(angle);
            return angle;
        }

        // change color to Red at numth connection
        void ChangeToRed(int num){
            if (!this.connectionList1.wrongNumbers.Contains(num)){
                this.connectionList1.wrongNumbers.Add(num);
                this.connectionList2.wrongNumbers.Add(num);
            }
        }

        // change color to White at numth connection
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
                State s1:   If the angle between the knee and the vertical falls within 32,
                            then it is in the Normal phase, and its state is s1. It is essentially the state
                            where the counters for proper and improper squats are updated.
                (35 ~ 65)
                State s2:   If the angle between the knee and the vertical falls between 35 and 65,
                            it is in the Transition phase and subsequently goes to state s2.
                (75 ~ 95)
                State s3:   If the angle between the knee and the vertical lies within a specific range
                            (say, between 75 and 95), it is in the Pass phase and subsequently goes to state s3.
                */
                
                /*
                Incorrect Squats
                Case 1 : with knee falling over toes
                Case 2 : cyclic from state s1 to s2 and again s1
                Case 3 : deep squats
                (Case 4 : Forntal View Warning)
                */

                float left_knee_angle, right_knee_angle;
                float feet_groin_angle, knees_groin_angle;
                float shoulder_feet_angle;

                // 세트 완료 체크
                if(!info.GetComponent<WOInfo>().isStarted) {
                    currentState = 4;
                }

                switch (currentState){
                    case 1:
                        left_knee_angle = CalcAngle(23, 25, -1); // Calculate left_knee_angle (vertical)
                        right_knee_angle = CalcAngle(24, 26, -1); // Calculate right_knee_angle (vertical)
                        info.currentState = 1;

                        feet_groin_angle = CalcAngle(27, 23, 24, 28);
                        knees_groin_angle = CalcAngle(25, 23, 24, 26);

                        shoulder_feet_angle = CalcAngle(11, 12, 27, 28, -1);

                        // Check knee position
                        if (knees_groin_angle < feet_groin_angle){
                            Debug.Log("Knees position Wrong!!!");
                            errMsgFrame.SendMessage("ShowError", 1);
                        }

                        // Check shoulder feet angle
                        if (15 < shoulder_feet_angle){
                            // Debug.Log("Shoulder - Feet angle Wrong!!!");
                            errMsgFrame.SendMessage("ShowError", 5);
                        }

                        // State1 -> State2
                        if (!isSquattingDown){ // If reached the case again after going down to State3
                            if (isDeepSquat){
                                // Incorrect Count up
                                info.increseIncorrectCount();
                                isDeepSquat = false;
                            } else {
                                // Correct Count up
                                info.increaseCorrectCount();
                            }
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

                        feet_groin_angle = CalcAngle(27, 23, 24, 28);
                        knees_groin_angle = CalcAngle(25, 23, 24, 26);

                        shoulder_feet_angle = CalcAngle(11, 12, 27, 28, -1);
                        
                        // Check knee position
                        if (knees_groin_angle < feet_groin_angle){
                            Debug.Log("Knees position Wrong!!!");
                            errMsgFrame.SendMessage("ShowError", 1);
                        }

                        // Check shoulder feet angle
                        if (15 < shoulder_feet_angle){
                            // Debug.Log("Shoulder - Feet angle Wrong!!!");
                            errMsgFrame.SendMessage("ShowError", 5);
                        }

                        // State2 -> State1
                        if ((left_knee_angle < 32) && (right_knee_angle < 32) && (!isSquattingDown)){ // Transition to State1
                            currentState = 1;
                            info.SetState(currentState);
                        } else if ((left_knee_angle < 32) && (right_knee_angle < 32) && (isSquattingDown)){
                            // Incorrect Count up : cyclic from state s1 to s2 and again s1
                            currentState = 1;
                            errMsgFrame.SendMessage("ShowError", 2);
                            info.increseIncorrectCount();
                            info.SetState(currentState);
                        }
                        
                        // State2 -> State3
                        if ((75 < left_knee_angle && left_knee_angle < 95) && (75 < right_knee_angle && right_knee_angle < 95) && (isSquattingDown)){
                            currentState = 3;
                            info.SetState(currentState);
                            isSquattingDown = false;
                        } else if ((75 < left_knee_angle && left_knee_angle < 95) && (75 < right_knee_angle && right_knee_angle < 95) && (!isSquattingDown)){
                            // If you go down when you should go up
                            currentState = 3;
                            info.SetState(currentState);
                        }
                        break;

                    case 3:
                        left_knee_angle = CalcAngle(23, 25, -1); // Calculate left_knee_angle (vertical)
                        right_knee_angle = CalcAngle(24, 26, -1); // Calculate right_knee_angle (vertical)

                        feet_groin_angle = CalcAngle(27, 23, 24, 28);
                        knees_groin_angle = CalcAngle(25, 23, 24, 26);
                        
                        shoulder_feet_angle = CalcAngle(11, 12, 27, 28, -1);

                        // Check knee position
                        if (knees_groin_angle < feet_groin_angle){
                            Debug.Log("Knees position Wrong!!!");
                            errMsgFrame.SendMessage("ShowError", 1);
                        }

                        // Check shoulder feet angle
                        if (15 < shoulder_feet_angle){
                            // Debug.Log("Shoulder - Feet angle Wrong!!!");
                            errMsgFrame.SendMessage("ShowError", 5);
                        }

                        // Too deep Squat
                        if ((95 < left_knee_angle) || (95 < right_knee_angle)){
                            // Print "Too Deep Squat" Message
                            isDeepSquat = true;
                            errMsgFrame.SendMessage("ShowError", 3);
                            ChangeToRed(30);
                            ChangeToRed(25);
                        }
                        
                        // State3 -> State2
                        if ((35 < left_knee_angle && left_knee_angle < 65) &&  (35 < right_knee_angle && right_knee_angle < 65)){
                            ChangeToWhite(30);
                            ChangeToWhite(25);
                            currentState = 2;
                            info.SetState(currentState);
                        }
                        break;

                    case 4:         // 세트 휴식 시간일때
                        info.SetState(currentState);
                        if(info.GetComponent<WOInfo>().isStarted) {
                            currentState = 1;
                            info.SetState(currentState);
                        }
                        break;
                }
            }
        }
    }
}