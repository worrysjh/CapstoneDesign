using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable IDE0065

namespace Mediapipe.Unity
{
    public class Test : MonoBehaviour
    {
        public Text ScriptTxt;
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("text changed");
            ScriptTxt.text = "Text is Changed";
        }

        public void DrawNow(NormalizedLandmarkList poseLandmarks){
            Debug.Log("poseLandmarks Get!");
        }

        public void Draw(LandmarkList targets, Vector3 scale, bool visualizeZ = true)
        {
            Debug.Log("test444444");
        }

        void Draw(Landmark target, Vector3 scale, bool visualizeZ = true)
        {
            Debug.Log("test2!!!!");
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}


// public class test : MonoBehaviour
// {
//     public Text ScriptTxt;
//     // Start is called before the first frame update
//     void Start()
//     {
//         Debug.Log("text changed");
//         ScriptTxt.text = "Text is Changed";
//     }

//     void Draw(LandmarkList targets, Vector3 scale, bool visualizeZ = true)
//     {
//         Debug.Log("test2");
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }
