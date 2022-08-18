using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


namespace Com.Kawaiisun.SimpleHostile{
    
    public class motion : MonoBehaviour
    {
        
        #region Variables

        public float speed;
        private Rigidbody body;
        public float sprintModifier;
        public Camera normal_camera;
        private float baseFOV;
        private float sprintFOVModifier = 1.5f;
        public float jumpForce;
        public float gravity = 1; //gravity       
        public LayerMask ground;
        public Transform groundDetector;
        private Animator animator;
        private PhotonView _pv;

        #endregion
        
        #region MonoBehaviour Callbacks
        // Start is called before the first frame update
        void Start()
        {
            baseFOV = normal_camera.fieldOfView;
            animator = GetComponent<Animator>();
            //Camera.main.enabled = false;
            body = GetComponent<Rigidbody>();
            _pv = GetComponent<PhotonView>();

            if(!_pv.IsMine){
                GetComponentInChildren<Camera>().enabled = false;
                Destroy(body);
            }
            else{
                GetComponentInChildren<Camera>().enabled = true;
            }
            
        }

        private void Update()
        {
            if(_pv.IsMine){
                // Axis
                float t_hmoved = Input.GetAxisRaw("Horizontal");
                float t_vmoved = Input.GetAxisRaw("Vertical");

                //Controls
                bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
                bool jump = Input.GetKeyDown(KeyCode.Space);

                //States
                bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, ground);
                bool isJumping = jump && isGrounded;
                bool isSprinting = sprint && t_vmoved > 0 && !isJumping && isGrounded;

                //Jumping 
                if (isJumping)
                {
                    animator.SetBool("walkFlag", false);
                    animator.SetBool("jumpFlag", true);
                    body.AddForce(Vector3.up * jumpForce);
                }
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {   
            if(_pv.IsMine){
                // Axis
                float t_hmoved = Input.GetAxisRaw("Horizontal");
                float t_vmoved = Input.GetAxisRaw("Vertical");

                //Controls
                bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
                bool jump = Input.GetKeyDown(KeyCode.Space);

                //States
                bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, ground);
                bool isJumping = jump && isGrounded;
                bool isSprinting = sprint && t_vmoved > 0 && !isJumping && isGrounded;
                            
                //Movement
                Vector3 t_direction = new Vector3(t_hmoved, 0, t_vmoved);
                t_direction.Normalize();

                float t_adjustedSpeed = speed;

                if (isSprinting) t_adjustedSpeed*=sprintModifier;

                Vector3 t_target_velocity = transform.TransformDirection(t_direction)*t_adjustedSpeed*Time.deltaTime;
                t_target_velocity.y = body.velocity.y;
                body.velocity = t_target_velocity;

                if(isGrounded){
                    animator.SetBool("walkFlag", true);
                    animator.SetBool("jumpFlag", false);
                }

                //Field of View
                if (isSprinting) {normal_camera.fieldOfView = Mathf.Lerp(normal_camera.fieldOfView, baseFOV*sprintFOVModifier, Time.deltaTime*8f);}
                else{normal_camera.fieldOfView = Mathf.Lerp(normal_camera.fieldOfView, baseFOV, Time.deltaTime*8f);}
            }   
        }

        #endregion
        
    }
}