using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{

    //variables
    public ParticleSystem lootEffect;
    [SerializeField] GameObject footSteps;
    [SerializeField] float moveSpeed = 2;
    [SerializeField] float rotationSpeed;
    Vector3 movementDir;
    float xDir, zDir;
    float inputMagnitude;
    Vector3 velocity;
    float gravity = 90.8f;


    // references
    [SerializeField] PlayerData data;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;
    [SerializeField] Transform cameraTransform;
    
    public HandStack handStack;
    [SerializeField] CharacterController controller;
    [HideInInspector] public bool canMove;
    public static PlayerMovements Instance;
    private Vector3 _input;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
      //  velocity = transform.position;
        SetAnimationMoveSpeed(data.animationMoveSpeed);
        SetPlayerMoveSpeed(data.playerMoveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Move();
            RotatePlayerFace();
            if (handStack.CanRemoveStack())
            {
                anim.SetLayerWeight(1, 1);
            }
            else anim.SetLayerWeight(1, 0);


        }
    }

    public void RotatePlayerFace()
    {
        if (_input != Vector3.zero)
        {
            SetMovingAnimationState(true);
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(_input.x, 0, _input.z)).normalized;

            // Smoothly rotate the player's transform towards the target rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    
        else
        {
            SetMovingAnimationState(false);
            footSteps.SetActive(false);
        }
    }


    private void Move()
    {
        xDir = Joystick.Instance.Horizontal + Input.GetAxisRaw("Horizontal");
        zDir = Joystick.Instance.Vertical + Input.GetAxisRaw("Vertical");

        _input = new Vector3(xDir, 0, zDir);
        inputMagnitude = Mathf.Clamp01(_input.magnitude);
        
        if (GameManager.sfx)
            footSteps.SetActive(true);

        _input = Quaternion.Euler(0, 45, 0) * _input;
        _input.Normalize();

        _input.y -= gravity * Time.deltaTime;

        _input *= inputMagnitude * moveSpeed * Time.deltaTime;


        controller.Move(_input);

    }
 
    public void SetMovingAnimationState(bool state) => anim.SetBool("isRunning", state);



    public void SetAnimationMoveSpeed(float ms) => anim.SetFloat("moveSpeed", ms);
    public void SetPlayerMoveSpeed(float ms) => moveSpeed = ms;


}
