using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;
    public float delay = 0.2f;
    public bool canSeePlayer;
    public GameObject playerRef;

    //Declaration of layermasks to create obstructions
    public LayerMask targetMask;
    public LayerMask obstructionMask;
   


    private void Start()
    {
        //Set playerRef to be playercharacter
        playerRef = GameObject.FindGameObjectWithTag("Player");
        //Set Coroutine for delay to not call the function every frame (Performance saving)
        StartCoroutine(FOVRoutine());
     
    }

    private IEnumerator FOVRoutine()
    {
        //Feeds delay through amount of seconds
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            //Continue calling functionality of Coroutine
            yield return wait;
            FieldOfViewCheck();

        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if(rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if(Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;

                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if(canSeePlayer)
        {
            canSeePlayer = false;
        }
    }

    
}