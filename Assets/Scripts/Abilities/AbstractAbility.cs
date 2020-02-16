using UnityEngine;
using UnityEditor;

public abstract class AbstractAbility : MonoBehaviour
{

    public CollectibleItemPower ItemPower = CollectibleItemPower.WEAK;
    public CollectibleItemType ItemType = CollectibleItemType.FORCE;

    public int RemainingUsages = 3;
    public bool Lock = false;

    public int ReparValue = 0;

    public Sprite Icon;

    public string name;
    public string description;

    
    public bool DoStuff(GameController controller, PlayerMovement player)
    {
        PrivateDoStuff(controller, player);
        RemainingUsages--;
        return RemainingUsages <= 0;
    }
    protected virtual void PrivateDoStuff(GameController controller, PlayerMovement player)
    {

    }
}