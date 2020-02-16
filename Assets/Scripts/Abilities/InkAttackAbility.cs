using UnityEngine;
using System.Collections;

public class InkAttackAbility : AbstractAttackAbility
{
    protected override void PrivateDoStuff(GameController controller, PlayerMovement player)
    {
        player.controller.InkAttack(HitboxInfo);
    }
}
