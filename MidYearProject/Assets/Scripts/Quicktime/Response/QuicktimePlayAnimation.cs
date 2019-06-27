using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuicktimePlayAnimation : QuicktimeResponse
{
    public Player m_player;
    public string m_animationName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnStart()
    {
        m_player.m_movementState = MovementState.Disabled;
    }

    public override void OnSuccess()
    {
        m_player.m_animator.Play(m_animationName);
        m_player.m_characterController.enabled = false;
    }

    public override void OnFailure()
    {
        m_player.m_movementState = MovementState.Walking;
    }
}
