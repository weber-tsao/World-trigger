using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Com.Kawaiisun.SimpleHostile{
    public class look : MonoBehaviour
    {
        public Transform player;
        public Transform camera;

        public float x_sensitivity;
        public float y_sensitivity;
        public float maxAngle;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            SetY();
        }

        void SetY(){
            float t_input = Input.GetAxis("Mouse Y")*y_sensitivity*-Time.deltaTime;
            Quaternion t_adj = Quaternion.AngleAxis(t_input, Vector3.right);
            Quaternion t_delta = camera.localRotation*t_adj;
            camera.localRotation = t_delta;
        }
    }
}
