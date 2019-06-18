using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTrigger : MonoBehaviour
{
    public GameObject m_target;
    public CameraController m_camera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_camera.m_lookAt = m_target.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_camera.m_lookAt = null;
        }
    }
}
