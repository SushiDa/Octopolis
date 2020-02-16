using UnityEngine;
using UnityEditor;

public class DashAbility : AbstractAbility
{
    protected override void PrivateDoStuff(GameController controller, PlayerMovement player)
    {
        player.controller.Dash();
    }
}