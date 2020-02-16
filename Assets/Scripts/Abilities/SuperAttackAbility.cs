using UnityEngine;
using System.Collections;

public class SuperAttackAbility : AbstractAttackAbility
{
    protected override void PrivateDoStuff(GameController controller, PlayerMovement player)
    {
        player.controller.SuperAttack(HitboxInfo);
    }
}
