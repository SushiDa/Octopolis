using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "CollectibleItem", menuName = "Cephalopolis/CollectibleItem")]
public class CollectibleItemData : ScriptableObject
{
    
    public CollectibleItemPower ItemPower = CollectibleItemPower.WEAK;
    public CollectibleItemType ItemType = CollectibleItemType.FORCE;

    public Sprite sprite;

    // Animator and other stuff ?

}