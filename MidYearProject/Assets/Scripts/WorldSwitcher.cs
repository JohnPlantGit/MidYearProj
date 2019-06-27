using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSwitcher : MonoBehaviour
{
    public GameObject[] m_state1;
    public GameObject[] m_state2;
    public CustomCharacterController m_character;
    public Material m_defaultMaterial;
    public Material m_phasedMaterial;

    private bool m_switch = true;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Q))
        {
            //if (Physics.CheckCapsule(m_character.m_collider.center + new Vector3(0, m_character.m_collider.height / 2, 0) + m_character.transform.position, m_character.m_collider.center - new Vector3(0, m_character.m_collider.height / 2, 0) + m_character.transform.position, m_character.m_collider.radius))
            if (!m_character.m_grounded)
            {
                m_switch = !m_switch;
                foreach (GameObject current in m_state1)
                {
                    //current.SetActive(m_switch);
                    current.GetComponent<MeshRenderer>().material = m_switch ? m_defaultMaterial : m_phasedMaterial;
                    current.GetComponent<Collider>().enabled = m_switch;
                }
                foreach (GameObject current in m_state2)
                {
                    //current.SetActive(!m_switch);
                    current.GetComponent<MeshRenderer>().material = !m_switch ? m_defaultMaterial : m_phasedMaterial;
                    current.GetComponent<Collider>().enabled = !m_switch;
                }
            }
        }
	}
}
