﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IChase : MonoBehaviour, IState
{
    private Entity entity;

    //get target from entityBehavior class
    private GameObject target => entity.target;

    public IChase(Entity entity) => this.entity = entity;

    /// <summary>
    /// Check if Target is INSIDE the Entity's(this) Chase Radius
    /// <para>or is Aggressive</para>
    /// </summary>
    /// <returns></returns>
    public bool Condition() =>
        entity.chase == true &&
        entity.idle == false;
        
    /// <summary>
    /// Execute this state
    /// </summary>
    public void Execute()
    {
        entity.charIconAnimator.SetInteger("iconstate", 2);
        entity.rb.mass = 3;
        // Look At Target
        LookAtTarget();
        // Chase
        Chase();

        if (IsTargetLost()) entity.chase = false;
    }


    /// <summary>
    /// Check if Entity lost his target
    /// </summary>
    /// <returns></returns>
    private bool IsTargetLost() => GetTargetDistance() > entity.lostRadius; //check if target is outside of radius


    /// <summary>
    /// Set Entity's facing direction towards Target position
    /// </summary>
    private void LookAtTarget() =>
        entity.facingDirection.transform.LookAt(target.transform);


    /// <summary>
    /// Moving towards facing direction
    /// </summary>
    private void Chase() =>
        entity.rb.velocity = entity.facingDirection.transform.forward * entity.speed * 100 * Time.deltaTime;

    ///// <summary>
    ///// Returns the Distance between
    ///// <para>Entity(this) and Target</para>
    ///// </summary>
    ///// <returns></returns>
    private float GetTargetDistance() => Vector2.Distance(
        entity.gameObject.transform.position, target.gameObject.transform.position);
}
