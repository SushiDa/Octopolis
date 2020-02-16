using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAbilityEffect : MonoBehaviour
{

    protected float duration = 5f;
    protected float currentTimer = 5f;
    bool started = false;

    protected GameController controller;
    protected PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        currentTimer = duration;

    }

    // Update is called once per frame
    void Update()
    {
        if(started)
        {
            currentTimer -= Time.deltaTime;
            if(currentTimer <= 0)
            {
                removeEffect();
                Destroy(this.gameObject);
            }
        }
    }

    public void BeginEffect(GameController controller, PlayerMovement player)
    {
        this.controller = controller;
        this.player = player;
        applyEffect();
        started = true;
    }
    protected virtual void applyEffect()
    {

    }

    protected virtual void removeEffect()
    {

    }
}
