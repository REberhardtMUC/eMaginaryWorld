using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController_cave : MonoBehaviour
{

    [SerializeField] float moveSpeed_fast = 3f;
    [SerializeField] float rotationSpeed_fast = 500f;
    private float moveSpeed;
    private float rotationSpeed;

    [Header("Ground Check Settings")]
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] Vector3 groundCheckOffset;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] ActivateFollowMe activate_FollowMe;
    Rigidbody rb_Followed;
    // [SerializeField] Rigidbody rb_Followed;
    [SerializeField] Rigidbody rb_Following;

    CameraController cameraController;
    Animator animator;
    CharacterController characterController;

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
        rb_Followed = this.gameObject.GetComponent<Rigidbody>();
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

        //bei der Berechnung stimmt was nicht?!
        float moveAmount = Mathf.Clamp01(Mathf.Abs(v) + Mathf.Abs(h));

        Vector3 moveInput = (new Vector3(h, 0, v)).normalized;

        Vector3 movDir = cameraController.PlanarRotation * moveInput;

        //To do: Abfrage wieder nutzen, wenn Grundfunktionalität gegeben ist
        /*turnAround = activate_FollowMe.get_turnAround();
        if (turnAround == false)
        {
            movDir = cameraController.PlanarRotation * moveInput;
        }
        else
        {
            rb_Followed.transform.LookAt(rb_Following.transform.position);
            movDir = new Vector3(rb_Following.transform.position.x, 0, 0).normalized;
        }*/

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

        // Makes the player jump
        // To do https://docs.unity3d.com/6000.0/Documentation/ScriptReference/CharacterController.Move.html
        /*  private CharacterController controller;
         *  private Vector3 playerVelocity;
         *  groundedPlayer = controller.isGrounded;
         *  private float gravityValue = -9.81f;
         *  if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }
        */

        Vector3 velocity = movDir * moveSpeed;
        velocity.y = ySpeed; // wichtig wegen Footstep Manager

        //Checking the NavMesh if future step is within NavMesh
        Vector3 newPosition = characterController.transform.position + movDir * Time.deltaTime * moveSpeed;
               
        NavMeshHit hit;
        bool is_Valid = NavMesh.SamplePosition(newPosition, out hit, 0.3f, NavMesh.AllAreas);
        //Debug.DrawRay(transform.position, newPosition, Color.yellow); 

        if (is_Valid)
        {
            // Mindestgröße Schritt
            if ((transform.position - hit.position).magnitude >= 0.001f)
            {
                characterController.Move(velocity * Time.deltaTime);
            }
        }


        if (moveAmount > 0)
        {
            targetRotation = Quaternion.LookRotation(movDir);
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        animator.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime);
    }
}
