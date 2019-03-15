using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject m_parent;
    public GameObject m_camera;
    public float m_armLength;
    public float m_pitchUpperLimit;
    public float m_pitchLowerLimit;
    public float m_lookingLimit;

    private float m_yaw = 0.0f;
    private float m_pitch = 0.0f;
    private Vector3 m_offset;
    private bool m_looking;
    private float m_lookingYaw;

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
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            m_looking = true;
            m_lookingYaw = m_yaw;
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            m_looking = false;
        }
        transform.position = m_parent.transform.position + m_offset;

        if (!m_looking)
        {
            m_yaw += Input.GetAxis("Mouse X") * 2;
            if (m_yaw > 180)
            {
                m_yaw -= 360;
            }
            if (m_yaw < -180)
            {
                m_yaw += 360;
            }
        }
        else
        {
            m_lookingYaw += Input.GetAxis("Mouse X") * 2;
            if (m_lookingYaw > m_yaw + m_lookingLimit)
            {
                m_lookingYaw = m_yaw + m_lookingLimit;
            }
            if (m_lookingYaw < m_yaw - m_lookingLimit)
            {
                m_lookingYaw = m_yaw - m_lookingLimit;
            }
        }
        m_pitch -= Input.GetAxis("Mouse Y") * 2;
        if (m_pitch > m_pitchUpperLimit)
        {
            m_pitch = m_pitchUpperLimit;
        }
        if (m_pitch < m_pitchLowerLimit)
        {
            m_pitch = m_pitchLowerLimit;
        }

        transform.eulerAngles = new Vector3(m_pitch, m_looking ? m_lookingYaw : m_yaw, 0);
    }
}
