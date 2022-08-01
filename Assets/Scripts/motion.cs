using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Com.Kawaiisun.SimpleHostile{
    public class motion : MonoBehaviour
    {
        
        public float speed;
        private Rigidbody body;

        // Start is called before the first frame update
        void Start()
        {
            Camera.main.enabled = false;
            body = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float t_hmoved = Input.GetAxisRaw("Horizontal");
            float t_vmoved = Input.GetAxisRaw("Vertical");

            Vector3 t_direction = new Vector3(t_hmoved, 0, t_vmoved);
            t_direction.Normalize();

            body.velocity = transform.TransformDirection(t_direction)*speed*Time.deltaTime;




        }
    }
}