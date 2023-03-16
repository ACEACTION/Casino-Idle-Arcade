using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{

    //variables
    [SerializeField] float rotationSpeed;
    [SerializeField] CharacterController cc;
    float speed;
    Vector3 velocity;
    [SerializeField] Animator anim;
    [SerializeField] Transform cameraTransform;
    
    Vector3 movementDir;
    float xDir, zDir;
    float inputMagnitude;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        RotatePlayerFace();

    }

    private void FixedUpdate()
    {
        cc.Move(velocity);
        velocity = Vector3.zero;
    }
    private void Move()
    {
        xDir = Input.GetAxis("Horizontal");
        zDir = Input.GetAxis("Vertical");

        movementDir = new Vector3(xDir, 0, zDir);
        inputMagnitude = Mathf.Clamp01(movementDir.magnitude);

        anim.SetFloat("InputMagnitude", inputMagnitude, 0.05f, Time.deltaTime);
        movementDir.Normalize();


    }
    public void RotatePlayerFace()
    {
        if (movementDir != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDir.normalized), Time.deltaTime * rotationSpeed);
            
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    private void OnAnimatorMove()
    {

        velocity += anim.deltaPosition * 2f;
    }
}
