using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMovements : MonoBehaviour
{

    //variables
    public ParticleSystem lootEffect;
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

    public static PlayerMovements Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        velocity = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (StartGame.Instance.startGame)
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
        movementDir.Normalize();
        velocity.y += gravity * Time.deltaTime * 10f;
        controller.Move(velocity * Time.deltaTime);
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
        if (!handStack.StackIsEmpty())
            velocity = anim.deltaPosition * animationMoveSpeed;
        else
            velocity = anim.deltaPosition * animationMoveSpeed;

        //velocity = anim.deltaPosition;
    }

    public void SetAnimationMoveSpeed(float ms) => anim.SetFloat("moveSpeed", ms);

}
