using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuicktimeResult
{
    Success,
    Continue,
    Failure
}


public class QuicktimeTrigger : MonoBehaviour
{
    protected bool m_inQuicktime;
    protected QuicktimeResponse[] m_responses;

    // Start is called before the first frame update
    public virtual void Start()
    {
        m_responses = GetComponents<QuicktimeResponse>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_inQuicktime)
        {
            QuicktimeResult result = QuicktimeUpdate();
            switch (result)
            {
                case QuicktimeResult.Success:
                    QuicktimeSuccess();
                    m_inQuicktime = false;
                    break;
                case QuicktimeResult.Failure:
                    QuicktimeFailure();
                    m_inQuicktime = false;
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_inQuicktime = true;
            QuicktimeStart();
        }
    }

    protected virtual void QuicktimeStart()
    {

    }

    protected virtual QuicktimeResult QuicktimeUpdate()
    {
        return QuicktimeResult.Continue;
    }

    protected virtual void QuicktimeSuccess()
    {

    }

    protected virtual void QuicktimeFailure()
    {

    }
}
