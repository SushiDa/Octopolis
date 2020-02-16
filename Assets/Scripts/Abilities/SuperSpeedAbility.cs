using UnityEngine;
using System.Collections;

public class SuperSpeedAbility : AbstractAbility
{

    [SerializeField] private GameObject EffectPrefab;
    // Use this for initialization

    protected override void PrivateDoStuff(GameController controller, PlayerMovement player)
    {
        var obj = Instantiate(EffectPrefab);
        obj.GetComponent<AbstractAbilityEffect>().BeginEffect(controller,player);
    }

}
