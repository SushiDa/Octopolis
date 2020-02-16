using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxActivator : MonoBehaviour
{

    [SerializeField] private BoxCollider2D hitbox;
    // Start is called before the first frame update

    public void ActivateHitbox()
    {
        hitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
    }

}
