using UnityEngine;
using UnityEditor;

public class ShieldAbility : AbstractAbility
{
    public float ShieldDuration = 5f;

    protected override void PrivateDoStuff(GameController controller, PlayerMovement player)
    {
        player.controller.Shield(ShieldDuration);
    }
}