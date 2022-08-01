using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Com.Kawaiisun.SimpleHostile{
    public class motion : MonoBehaviour
    {
        
        public float speed;
        private Rigidbody body;
        public float sprintModifier;
        public Camera normal_camera;
        private float baseFOV;
        private float sprintFOVModifier = 1.5f;
        public float jumpForce;

        public float gravity = 1; //gravity
        private CharacterController controller;

        //private Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            baseFOV = normal_camera.fieldOfView;

            Camera.main.enabled = false;
            body = GetComponent<Rigidbody>();

            controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {   
            // Axis
            float t_hmoved = Input.GetAxisRaw("Horizontal");
            float t_vmoved = Input.GetAxisRaw("Vertical");

            // Controls
            bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            bool jump = Input.GetKey(KeyCode.Space);

            //States
            bool isJumping = jump;
            bool isSprinting = sprint && t_vmoved > 0 && !isJumping;
            
            //Jumping
            if(isJumping){
                body.AddForce(Vector3.up*jumpForce);
            }

            //Movement
            Vector3 t_direction = new Vector3(t_hmoved, 0, t_vmoved);
            t_direction.Normalize();

            float t_adjustedSpeed = speed;

            if (isSprinting) t_adjustedSpeed*=sprintModifier; 

            Vector3 t_target_velocity = transform.TransformDirection(t_direction)*t_adjustedSpeed*Time.deltaTime;
            t_target_velocity.y = body.velocity.y;
            body.velocity = t_target_velocity;

            // Field of View
            if (isSprinting) {normal_camera.fieldOfView = Mathf.Lerp(normal_camera.fieldOfView, baseFOV*sprintFOVModifier, Time.deltaTime*8f);}
            else{normal_camera.fieldOfView = Mathf.Lerp(normal_camera.fieldOfView, baseFOV, Time.deltaTime*8f);}



        }
    }
}