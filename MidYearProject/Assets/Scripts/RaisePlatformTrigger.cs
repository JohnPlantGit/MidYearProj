using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisePlatformTrigger : MonoBehaviour
{
    public Animator m_platform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            m_platform.SetTrigger("Raise");
    }
}
