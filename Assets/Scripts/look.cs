using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Com.Kawaiisun.SimpleHostile{
    public class look : MonoBehaviour
    {
        #region Variables

        public static bool cursorLocked = true;
        public Transform player;
        public Transform camera;
        public Transform weapon;
        public float x_sensitivity;
        public float y_sensitivity;
        public float maxAngle;
        private Quaternion camCenter;

        #endregion

        #region MonoBehaviour Callbacks

        // Start is called before the first frame update
        void Start()
        {
            camCenter = camera.localRotation; // set rotation origin for camera to camCenter
        }

        // Update is called once per frame
        void Update()
        {
            SetY();
            SetX();
            UpdateCursorLock();
        }

        #endregion
        
        #region Private Methods

        void SetY(){
            float t_input = Input.GetAxis("Mouse Y")*y_sensitivity*Time.deltaTime;
            Quaternion t_adj = Quaternion.AngleAxis(t_input, -Vector3.right);
            Quaternion t_delta = camera.localRotation*t_adj;

            if (Quaternion.Angle(camCenter, t_delta) < maxAngle){
                camera.localRotation = t_delta;
                weapon.localRotation = t_delta;
            }
        }

        void SetX(){
            float t_input = Input.GetAxis("Mouse X")*x_sensitivity*Time.deltaTime;
            Quaternion t_adj = Quaternion.AngleAxis(t_input, Vector3.up);
            Quaternion t_delta = player.localRotation*t_adj;
            player.localRotation = t_delta;
        }

        void UpdateCursorLock(){
            if(cursorLocked){
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                
                if(Input.GetKeyDown(KeyCode.Escape)){
                    cursorLocked = false;
                }
            } 
            else{
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                if(Input.GetKeyDown(KeyCode.Escape)){
                    cursorLocked = true;
                }
            }
        }
        
        #endregion
        
    }
}
