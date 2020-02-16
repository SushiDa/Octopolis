using UnityEngine;
using System.Collections;

public class SuperSpeedEffect : AbstractAbilityEffect
{

    private float originalPlayerSpeed;
    private float originalJumpForce;
    private float originalDJumpForce;
    protected override void applyEffect()
    {
        originalJumpForce = player.controller.m_JumpForce;
        originalDJumpForce = player.controller.m_DoubleJumpForce;
        originalPlayerSpeed = player.controller.m_RunSpeed;


        player.controller.m_JumpForce = originalJumpForce * 1.3f;
        player.controller.m_DoubleJumpForce = originalDJumpForce * 1.3f;
        player.controller.m_RunSpeed = originalPlayerSpeed * 1.55f;
    }

    protected override void removeEffect()
    {
        player.controller.m_JumpForce = originalJumpForce;
        player.controller.m_DoubleJumpForce = originalDJumpForce;
        player.controller.m_RunSpeed = originalPlayerSpeed;
    }
}
