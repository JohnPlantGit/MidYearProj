using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuicktimeButtonPress : QuicktimeTrigger
{
    public KeyCode m_button;
    public float m_length;

    float m_timer;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        m_timer = m_length;
    }

    protected override void QuicktimeStart()
    {
        foreach (QuicktimeResponse response in m_responses)
        {
            response.OnStart();
        }
    }

    protected override QuicktimeResult QuicktimeUpdate()
    {
        m_timer -= Time.deltaTime;

        if (m_timer <= 0)
        {
            return QuicktimeResult.Failure;
        }
        else
        {
            if (Input.GetKeyDown(m_button))
            {
                return QuicktimeResult.Success;
            }
        }

        return QuicktimeResult.Continue;
    }

    protected override void QuicktimeSuccess()
    {
        foreach(QuicktimeResponse response in m_responses)
        {
            response.OnSuccess();
        }

        m_timer = m_length;
    }

    protected override void QuicktimeFailure()
    {
        foreach (QuicktimeResponse response in m_responses)
        {
            response.OnFailure();
        }

        m_timer = m_length;
    }
}
