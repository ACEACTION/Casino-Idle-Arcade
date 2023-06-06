using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMovements : MonoBehaviour
{

    //variables
    public ParticleSystem lootEffect;
    [SerializeField] GameObject footSteps;
    [SerializeField] float animationMoveSpeed = 2;
    [SerializeField] float animationStackMoveSpeed = 4;
    [SerializeField] float rotationSpeed;
    Vector3 movementDir;
    float xDir, zDir;
    float inputMagnitude;
    Vector3 velocity;
    float gravity = -19.8f;
    float speed;


    // references
    [SerializeField] PlayerData data;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;
    [SerializeField] Transform cameraTransform;
    public HandStack handStack;
    [SerializeField] CharacterController controller;
    [HideInInspector] public bool canMove;
    public static PlayerMovements Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
      //  velocity = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Move();
            RotatePlayerFace();
        }
    }
    

    private void Move()
    {

        xDir = Joystick.Instance.Horizontal + Input.GetAxis("Horizontal");
        zDir = Joystick.Instance.Vertical + Input.GetAxis("Vertical");

        movementDir = new Vector3(xDir, 0, zDir);
        inputMagnitude = Mathf.Clamp01(movementDir.magnitude);

        anim.SetFloat("InputMagnitude", inputMagnitude, 0.05f, Time.deltaTime);
        //velocity.y += gravity * Time.deltaTime * 10f;
        movementDir.Normalize();
        movementDir *= inputMagnitude * animationMoveSpeed * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);
        controller.Move(movementDir);
        footSteps.SetActive(true);
    }

    public void RotatePlayerFace()
    {
        if (movementDir != Vector3.zero)
        {
            SetMovingAnimationState(true);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDir.normalized), Time.deltaTime * rotationSpeed);
        }
        else
        {
            SetMovingAnimationState(false);
            footSteps.SetActive(false);

        }
    }
            
    public void SetMovingAnimationState(bool state) => anim.SetBool("isMoving", state);

    //private void OnAnimatorMove()
    //{
    //    if (!handStack.StackIsEmpty())
    //        velocity = anim.deltaPosition * animationMoveSpeed;
    //    else
    //        velocity = anim.deltaPosition * animationMoveSpeed;

    //    //velocity = anim.deltaPosition;
    //}

    public void SetAnimationMoveSpeed(float ms) => anim.SetFloat("moveSpeed", ms);

}
