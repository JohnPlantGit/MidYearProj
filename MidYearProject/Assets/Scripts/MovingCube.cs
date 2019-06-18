using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public float m_start;
    public float m_end;
    public float m_lerpLength;

    float m_lerpTimer;
    bool m_direction;

	// Use this for initialization
	void Start ()
    {
        m_start += transform.position.x;
        m_end += transform.position.x;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_direction)
            m_lerpTimer += Time.deltaTime;
        else
            m_lerpTimer -= Time.deltaTime;

        if (m_lerpTimer >= m_lerpLength)
        {
            m_direction = false;
            m_lerpTimer = m_lerpLength;
        }
        if (m_lerpTimer <= 0)
        {
            m_direction = true;
            m_lerpTimer = 0;
        }

        transform.position = new Vector3(Mathf.Lerp(m_start, m_end, m_lerpTimer), transform.position.y, transform.position.z);
	}
}
