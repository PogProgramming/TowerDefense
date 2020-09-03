using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject playerCam;
    private MoveCamera mc;

    private Rigidbody playerRb;
    private CapsuleCollider playerCol;

    public LayerMask groundLayer;

    public float movementSpeed = 10F;
    public float jumpForce;

    public float maxVelocityChange = 10f;
    public float gravity = 10f;

    public bool ClearAbove;
    public bool Standing;

    void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
        playerCol = transform.GetComponent<CapsuleCollider>();

        playerRb = transform.GetComponent<Rigidbody>();
        playerRb.freezeRotation = true;
        playerRb.useGravity = false;

        mc = playerCam.GetComponent<MoveCamera>();
    }

    void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= movementSpeed;

        Vector3 velocity = playerRb.velocity;
        Vector3 velocityChange = targetVelocity - velocity;
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);

        playerRb.AddForce(velocityChange, ForceMode.VelocityChange);

        playerRb.AddForce(new Vector3(0, -gravity * playerRb.mass, 0));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
    }
    private void Jump()
    {
        if (IsGrounded())
        {
            playerRb.AddForce(Vector3.up * 5f, ForceMode.Impulse);

        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(playerCol.bounds.center,
            new Vector3(playerCol.bounds.center.x, playerCol.bounds.min.y, playerCol.bounds.center.z),
            playerCol.radius * .9f, groundLayer);
    }
}