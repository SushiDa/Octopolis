using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAbility : AbstractAbility
{
    // Start is called before the first frame update
    protected override void PrivateDoStuff(GameController controller, PlayerMovement player)
    {
        player.controller.currentLife += 3;
    }
}
