using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject pivotLift;
    Vector3 targetPos;
    [SerializeField] GameObject trigger_closeDoor;

    private bool player_insideLift;

    [SerializeField] float moveSpeed_fast = 3f;
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

    private string surface;

    bool isGrounded;
    float ySpeed;

    Quaternion targetRotation;

    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
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
        //Debug.Log("Oberfläche: " + surface);
    }

    public void Update()
    {
        GroundCheck();

        player_insideLift = trigger_closeDoor.GetComponent<ParentCharacter>().is_insideLift;

        if (player_insideLift)
        {
            targetPos = pivotLift.gameObject.transform.position;
            transform.position = targetPos;
        }
        else
        {
            MovePlayer();
        }
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
        float moveAmount = Mathf.Clamp01(Mathf.Abs(v) + Mathf.Abs(h));

        Vector3 moveInput = (new Vector3(h, 0, v)).normalized;
        Vector3 movDir = cameraController.PlanarRotation * moveInput;

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

        Vector3 velocity = movDir * moveSpeed;
        velocity.y = ySpeed;


        //Checking the NavMesh if future step is within NavMesh
        Vector3 newPosition = characterController.transform.position + movDir * Time.deltaTime * moveSpeed;

        NavMeshHit hit;
        bool is_Valid = NavMesh.SamplePosition(newPosition, out hit, 0.3f, NavMesh.AllAreas);

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
