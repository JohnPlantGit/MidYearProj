using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState
{
    Walking,
    Sprinting,
    Crouching,
    Disabled
}

public class Player : MonoBehaviour
{
    public CharacterController m_characterController;
    public Animator m_animator;
    public float m_acceleration;
    public float m_crouchedAcceleration;
    public float m_sprintAcceleration;
    public float m_friction;
    public Vector3 m_gravity;
    public float m_crouchLength;
    public CameraController m_cameraController = null;

    //public CapsuleCollider m_collider = null;
    private Vector3 m_velocity;
    public bool m_grounded = false;
    public bool m_crouching = false;
    private float m_colliderHeight;
    private float m_colliderCentre;
    private float m_crouchTimer;
    public MovementState m_movementState = MovementState.Walking;
    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();

        m_colliderHeight = m_characterController.height;
        m_colliderCentre = m_characterController.center.y;

        Cursor.lockState = CursorLockMode.Locked;

        m_crouchTimer = m_crouchLength;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_movementState != MovementState.Disabled)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (m_crouching)
                {
                    if (!Physics.Raycast(transform.position, Vector3.up, 2.0f, LayerMask.NameToLayer("Player")))
                    {
                        m_crouching = false;
                        m_movementState = MovementState.Walking;
                    }

                }
                else if (m_movementState == MovementState.Walking)
                {
                    m_crouching = true;
                    m_movementState = MovementState.Crouching;
                }
            }
            if (m_crouching)
                m_crouchTimer -= Time.deltaTime;
            else
                m_crouchTimer += Time.deltaTime;
            m_crouchTimer = Mathf.Clamp(m_crouchTimer, 0, m_crouchLength);

            m_characterController.height = Mathf.Lerp(m_colliderHeight / 2, m_colliderHeight, m_crouchTimer / m_crouchLength);
            m_characterController.center = new Vector3(0, Mathf.Lerp(m_colliderCentre / 2, m_colliderCentre, m_crouchTimer / m_crouchLength), 0);

            if (Input.GetKeyDown(KeyCode.LeftShift) && m_movementState == MovementState.Walking)
            {
                m_movementState = MovementState.Sprinting;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift) && m_movementState == MovementState.Sprinting)
            {
                m_movementState = MovementState.Walking;
            }

            Vector3 movementVector = Quaternion.Euler(0, m_cameraController.Yaw, 0) * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            float acceleration = 0;
            switch (m_movementState)
            {
                case MovementState.Walking:
                    acceleration = m_acceleration;
                    break;
                case MovementState.Sprinting:
                    acceleration = m_sprintAcceleration;
                    break;
                case MovementState.Crouching:
                    acceleration = m_crouchedAcceleration;
                    break;
            }

            m_velocity += movementVector * acceleration * Time.deltaTime;
            if (!m_characterController.isGrounded)
                m_velocity += m_gravity * Time.deltaTime;

            m_characterController.Move(m_velocity * Time.deltaTime);

            m_velocity -= new Vector3(m_velocity.x, 0, m_velocity.z) * m_friction * Time.deltaTime;

            transform.rotation = Quaternion.Euler(0, m_cameraController.Yaw, 0);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // THIS MAKES THE PLAYER SLIDE DOWN SLOPES... AND ALSO UP...
        //Vector3 velocityProjected = Vector3.Project(m_velocity, -hit.normal);
        //m_velocity -= velocityProjected;
    }
}

