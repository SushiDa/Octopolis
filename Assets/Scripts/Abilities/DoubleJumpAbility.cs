using UnityEngine;
using UnityEditor;

public class DoubleJumpAbility : AbstractAbility
{
    protected override void PrivateDoStuff(GameController controller, PlayerMovement player)
    {
        player.controller.DoubleJump();
    }
}