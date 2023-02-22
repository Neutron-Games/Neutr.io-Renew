using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    CharacterController controller;
    public Animator animator;
    public Transform playerBody;

    // GameObject ForkCharacters;


    private float SmoothSpeed = 0.125f;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity = 1f;


    public FixedJoystick joystick;


    [SerializeField] public float speed;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private float joystickSensitivity;
    [SerializeField] private Vector3 offset;


    public static PlayerController instance;

    // GameManager gameManager;


    private void Awake()
    {
        instance = this;

        // cam = GameObject.Find("Camera").GetComponent<Camera>();
        animator = GetComponent<Animator>();
        // joystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
    }

    private void Start()
    {
        speed = 5f;
        joystickSensitivity = 10f;
        playerBody = this.gameObject.transform;
        // animator.SetBool("isGameStart", true);
        offset = new Vector3(0, 9.4f, -4.14f);
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 move = transform.forward;
        controller.Move(move * speed * Time.deltaTime);

        if (controller.isGrounded)
        {
            transform.position += new Vector3(0, -1f, 0);
        }

        // Joystick Control
        float x = joystick.Horizontal;
        float z = joystick.Vertical;

        Vector3 direction = new Vector3(x, 0f, z).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }

    private void LateUpdate()
    {
        if (cam)
        {
            Vector3 desiredPos = transform.position + offset;
            Vector3 smoothedPos = Vector3.Lerp(cam.transform.position, desiredPos, SmoothSpeed);
            cam.transform.position = smoothedPos;
        }
    }
}
