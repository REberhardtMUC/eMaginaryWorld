using Unity.VisualScripting;
using UnityEngine;

public class PlayerController_switch : MonoBehaviour
{ 
    [SerializeField] public float moveSpeed_fast = 3f;
    [SerializeField] float rotationSpeed_fast = 500f;
    private float moveSpeed;
    private float rotationSpeed;

    [Header("Ground Check Settings")]
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] Vector3 groundCheckOffset;
    [SerializeField] LayerMask groundLayer;

    CameraController cameraController;
    Animator animator;
    CharacterController characterController;

    Vector3 movDir;

    private string surface;

    bool turnAround = false;
    bool isGrounded;
    float ySpeed;

    Quaternion targetRotation;

    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        //rb_Followed = this.gameObject.GetComponent<Rigidbody>();
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Water")
        {
            surface = "Water";
        }
        else
        {
            surface = "Grass";
        }
    }

    public void Update()
    {
        GroundCheck();
        MovePlayer(); 
    }
    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius, groundLayer);
        //isGrounded = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);

        // siehe Parameter von isGrounded
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }


    void MovePlayer()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float moveAmount = Mathf.Clamp01(Mathf.Abs(v) + Mathf.Abs(h));

        var moveInput = (new Vector3(h, 0, v)).normalized;

        movDir = cameraController.PlanarRotation * moveInput;

        if (isGrounded)
        {
            ySpeed = -0.5f;
        }
        else
        {
            // she moves and rotates slower when she runs in the water
            if (surface == "Water")
            {
                moveSpeed = moveSpeed_fast / 2;
                rotationSpeed = rotationSpeed_fast / 2;
            }
            else
            {
                moveSpeed = moveSpeed_fast;
                rotationSpeed = rotationSpeed_fast;
            }
            ySpeed += Physics.gravity.y * Time.deltaTime;
        }

        var velocity = movDir * moveSpeed;
        velocity.y = ySpeed;


        characterController.Move(velocity * Time.deltaTime);

        if (moveAmount > 0)
        {
            targetRotation = Quaternion.LookRotation(movDir);
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        animator.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime);
    }
}
