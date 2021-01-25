using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public GameObject cannon;
    public GameObject bullet;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float force = 40f;
    float turnSmoothVelocity;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    // Rigidbody rb;
    // Transform t;
 
    // Start is called before the first frame update
    void Start()
    {
        // rb = GetComponent<Rigidbody>();
        // t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        if(direction.magnitude >= 0.1f){
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        // https://docs.unity3d.com/ScriptReference/Input.html
        if (Input.GetButtonDown("Fire1")){
            GameObject newBullet = GameObject.Instantiate(bullet, cannon.transform.position, cannon.transform.rotation) as GameObject;            
            newBullet.GetComponent<Rigidbody>().velocity += Vector3.up * 2;
            newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.up * 1500);
        }

        // https://answers.unity.com/questions/574328/jumping-with-a-character-controller.html
        if (controller.isGrounded && Input.GetButton("Jump")) {
            moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
