using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "HitboxInfo", menuName = "Cephalopolis/HitboxInfo")]
public class HitboxInfo : ScriptableObject
{
    public float OffsetX;
    public float OffsetY;
    public float X;
    public float Y;

    public int Damage;
    public Vector2 BumpDirection;
    public float BumpPower;
    public float KnockdownTime;
    public float AttackDuration;
    public bool BlockMovement;
}