using UnityEngine;
using System.Collections;

public class BasicAttackAbility: AbstractAttackAbility
{
    protected override void PrivateDoStuff(GameController controller, PlayerMovement player)
    {
        player.controller.Attack(HitboxInfo);
    }
}
