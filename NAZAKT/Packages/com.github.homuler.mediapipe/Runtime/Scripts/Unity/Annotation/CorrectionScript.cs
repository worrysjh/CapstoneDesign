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

        // Start is called before the first frame update
        void Start()
        {
            time = 1f;
        }

        // Update is called once per frame
        void Update()
        {
            if (time > 0)
                time -= Time.deltaTime;
            else{
                print();
                time = 0.1f;
            }
        }

        void print()
        {
            Debug.Log("test!");
            if (target != null)
            {
                // Landmark landmark = JsonUtility.FromJson<Landmark>(target?.Landmark.ToString());
                // Debug.Log((target?.Landmark)[12].X);
                // 1 : (target?.Landmark)[12].
                // 2 : (target?.Landmark)[14].
                // 3 : (target?.Landmark)[16].
                // Vector3 v1 = new Vector3((target?.Landmark)[14].X - (target?.Landmark)[12].X, (target?.Landmark)[14].Y - (target?.Landmark)[12].Y, (target?.Landmark)[14].Z - (target?.Landmark)[12].Z) * 10.0f;
                // Vector3 v2 = new Vector3((target?.Landmark)[16].X - (target?.Landmark)[12].X, (target?.Landmark)[16].Y - (target?.Landmark)[12].Y, (target?.Landmark)[16].Z - (target?.Landmark)[12].Z) * 10.0f;
                // Vector3 v3 = new Vector3((target?.Landmark)[16].X - (target?.Landmark)[14].X, (target?.Landmark)[16].Y - (target?.Landmark)[14].Y, (target?.Landmark)[16].Z - (target?.Landmark)[14].Z) * 10.0f;
                // float angle = Vector3.Angle(v1, v2);
                // float angleInDegrees = angle * (180f / Mathf.PI);

                // Vector3 v1 = new Vector3((target?.Landmark)[14].X - (target?.Landmark)[12].X, (target?.Landmark)[14].Y - (target?.Landmark)[12].Y, (target?.Landmark)[14].Z - (target?.Landmark)[12].Z);
                // Vector3 v2 = new Vector3((target?.Landmark)[16].X - (target?.Landmark)[14].X, (target?.Landmark)[16].Y - (target?.Landmark)[14].Y, (target?.Landmark)[16].Z - (target?.Landmark)[14].Z);

                // Vector3 cross = Vector3.Cross(v1, v2);

                
                //float angle = Mathf.Acos(Vector3.Dot(v1.normalized, v2.normalized)) * Mathf.Rad2Deg;

                // float angle = Mathf.Atan2((target?.Landmark)[14].Y - (target?.Landmark)[12].Y, (target?.Landmark)[14].X - (target?.Landmark)[12].X) * Mathf.Rad2Deg;

                Vector3 v1 = new Vector3((target?.Landmark)[12].X, (target?.Landmark)[12].Y, (target?.Landmark)[12].Z);
                Vector3 v2 = new Vector3((target?.Landmark)[14].X, (target?.Landmark)[14].Y, (target?.Landmark)[14].Z);
                Vector3 v3 = new Vector3((target?.Landmark)[16].X, (target?.Landmark)[16].Y, (target?.Landmark)[16].Z);

                Vector3 VA = v1 - v2;
                Vector3 VB = v3 - v2;

                float angle = Vector3.Angle(VA, VB);
                Debug.Log(angle);
                ScriptTxt.GetComponent<Text>().text = angle.ToString();

                ConnectionListAnnotation connectionList1 = connectionListAnnotation1.GetComponent<ConnectionListAnnotation>();
                ConnectionListAnnotation connectionList2 = connectionListAnnotation2.GetComponent<ConnectionListAnnotation>();

                if (angle > 160){
                    if (!connectionList1.wrongNumbers.Contains(15)){
                        connectionList1.wrongNumbers.Add(15);
                        connectionList2.wrongNumbers.Add(15);
                    }
                    if (!connectionList1.wrongNumbers.Contains(16)){
                        connectionList1.wrongNumbers.Add(16);
                        connectionList2.wrongNumbers.Add(16);
                    }
                }
                else {
                    if (connectionList1.wrongNumbers.Contains(15)){
                        connectionList1.wrongNumbers.Remove(15);
                        connectionList2.wrongNumbers.Remove(15);
                    }
                    if (connectionList1.wrongNumbers.Contains(16)){
                        connectionList1.wrongNumbers.Remove(16);
                        connectionList2.wrongNumbers.Remove(16);
                    }
                }
            }
        }
    }
}