using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWarning : MonoBehaviour, IState
{
    private Entity entity;

    //get target from entityBehavior class
    private GameObject target => entity.target;

    private float count;
    
    public IWarning(Entity entity) => this.entity = entity;

    /// <summary>
    /// Check if Target is INSIDE the Entity's(this) Observation Radius
    /// </summary>
    /// <returns></returns>
    public bool Condition() =>
        GetTargetDistance() <= entity.observationRadius &&
        entity.chase == false;


    /// <summary>
    /// Execute this state
    /// </summary>
    public void Execute()
    {
        Debug.Log("Warning");
        
        entity.waitTime = 1f;

        entity.idle = false;

        if (entity.chase == false)
            Warning();
    }

    /// <summary>
    /// Set entities state to attack after an amount of time
    /// </summary>
    private void Warning()
    {
        count += Time.deltaTime;

        //stop walking
        entity.rb.velocity = Vector3.zero;

        entity.charIconAnimator.SetInteger("iconstate", 1);
        //If target is in radius for an amount of time, entities state will change to attack
        if (count > 4f || GetTargetDistance() < entity.chaseRadius)
        {
            count = 0;
            entity.chase = true;
        }
    }

    /// <summary>
    /// Returns the Distance between
    /// <para>Entity(this) and Target</para>
    /// </summary>
    /// <returns></returns>
    private float GetTargetDistance() => Vector2.Distance(
        entity.gameObject.transform.position, target.gameObject.transform.position);
}
