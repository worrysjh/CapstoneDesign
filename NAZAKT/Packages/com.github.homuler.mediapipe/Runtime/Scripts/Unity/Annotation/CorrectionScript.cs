using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

namespace Mediapipe.Unity
{
    public class CorrectionScript : MonoBehaviour
    {
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
                time = 1f;
            }
        }

        void print()
        {
            Debug.Log("test!");
            if (target != null)
            {
                // Landmark landmark = JsonUtility.FromJson<Landmark>(target?.Landmark.ToString());
                Debug.Log((target?.Landmark)[0].X);
            }
        }
    }
}