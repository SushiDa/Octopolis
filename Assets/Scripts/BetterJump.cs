using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{

    public int PlayerNumber = 1;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float velocityThreshold = 0f;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        } 
        else if (rb.velocity.y > 0 & !Input.GetButton("Jump_" + PlayerNumber) || Input.GetButton("Jump_" + PlayerNumber) && rb.velocity.y < velocityThreshold)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

}
