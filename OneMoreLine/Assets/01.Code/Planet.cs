#region Header
/*	============================================
 *	작성자 : Strix
 *	작성일 : 2019-07-11 오후 12:48:11
 *	개요 : 
   ============================================ */
#endregion Header

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GameEventTrigger))]
[RequireComponent(typeof(DistanceJoint2D))]
public class Planet : CObjectBase
{
    [GetComponent]
    public DistanceJoint2D p_pDistanceJoint { get; private set; }

    [GetComponent]
    public Rigidbody2D p_pRigidbody { get; private set; }

    protected override void OnAwake()
    {
        base.OnAwake();

        p_pRigidbody.mass = 100000;
        p_pRigidbody.gravityScale = 0f;
    }
}