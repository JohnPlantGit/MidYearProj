using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject m_parent;
    public GameObject m_camera;
    public Transform m_lookAt;
    public float m_armLength;
    public float m_pitchUpperLimit;
    public float m_pitchLowerLimit;
    public float m_rotationSpeed;
    public float m_rotationFriction;
    public bool m_useVelocity;
    public float m_movementSpeed;

    private float m_yaw = 0.0f;
    private float m_pitch = 0.0f;
    private float m_yawVelocity = 0.0f;
    private float m_pitchVelocity = 0.0f;
    private Vector3 m_offset;
    private Vector3 m_velocity;

    public float Yaw
    {
        get { return m_yaw; }
    }
    public float Pitch
    {
        get { return m_pitch; }
    }
    public Ray DirectionRay
    {
        get
        {
            Ray output = new Ray();
            output.origin = m_camera.transform.position;
            output.direction = m_camera.transform.forward;
            return output;
        }
    }

    // Use this for initialization
    void Start ()
    {
        m_offset = transform.position - m_parent.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_useVelocity = !m_useVelocity;
        }

        if (!m_lookAt)
        {
            if (m_useVelocity) // CAMERA VELOCITY MOVEMENT
            {
                m_velocity = (m_parent.transform.position + m_offset) - transform.position;
                transform.position += m_velocity * m_movementSpeed * Time.deltaTime;
            }
            else // CAMERA SNAP MOVEMENT
            {
                transform.position = (m_parent.transform.position + m_offset);
            }

            m_yawVelocity += Input.GetAxis("Mouse X") * m_rotationSpeed;
            m_yaw += m_yawVelocity * Time.deltaTime;
            m_yawVelocity -= m_yawVelocity * m_rotationFriction * Time.deltaTime;
            if (m_yaw > 180)
            {
                m_yaw -= 360;
            }
            if (m_yaw < -180)
            {
                m_yaw += 360;
            }

            m_pitchVelocity -= Input.GetAxis("Mouse Y") * m_rotationSpeed;
            m_pitch += m_pitchVelocity * Time.deltaTime;
            m_pitchVelocity -= m_pitchVelocity * m_rotationFriction * Time.deltaTime;
            if (m_pitch > m_pitchUpperLimit)
            {
                m_pitch = m_pitchUpperLimit;
            }
            if (m_pitch < m_pitchLowerLimit)
            {
                m_pitch = m_pitchLowerLimit;
            }

            transform.eulerAngles = new Vector3(m_pitch, m_yaw, 0);
        }
        else
        {
            transform.forward = (m_lookAt.position - transform.position).normalized;
        }
    }
}
