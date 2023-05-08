using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using UnityEngine.UI;

namespace Mediapipe.Unity
{
    public class CorrectionScript : MonoBehaviour
    {
        [SerializeField] public GameObject ScriptTxt;
        [SerializeField] private GameObject connectionListAnnotation1;
        [SerializeField] private GameObject connectionListAnnotation2;
        private float time;
        public LandmarkList target;
        private ConnectionListAnnotation connectionList1;
        private ConnectionListAnnotation connectionList2;

        // Start is called before the first frame update
        void Start()
        {
            time = 1f;
            connectionList1 = connectionListAnnotation1.GetComponent<ConnectionListAnnotation>();
            connectionList2 = connectionListAnnotation2.GetComponent<ConnectionListAnnotation>();
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
        // num3 = -1 일 때엔 y축과 각도를 계산함
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

        // num 번째 Connection의 색을 빨간색으로 변경함
        void ChangeToRed(int num){
            if (!this.connectionList1.wrongNumbers.Contains(num)){
                this.connectionList1.wrongNumbers.Add(num);
                this.connectionList2.wrongNumbers.Add(num);
            }
        }

        // num 번째 Connection의 색을 흰색으로 변경함
        void ChangeToWhite(int num){
            if (this.connectionList1.wrongNumbers.Contains(num)){
                this.connectionList1.wrongNumbers.Remove(num);
                this.connectionList2.wrongNumbers.Remove(num);
            }
        }

        void CheckPose(){
            if (target != null)
            {
                // left_knee_angle
                float left_knee_angle = CalcAngle(23, 25, -1);

                ScriptTxt.GetComponent<Text>().text = left_knee_angle.ToString();

                if (left_knee_angle > 30){
                    ChangeToRed(25);
                } else {
                    ChangeToWhite(25);
                }
            }
        }
    }
}