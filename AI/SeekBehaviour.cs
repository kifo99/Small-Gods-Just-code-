using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeekBehaviour : SteeringBehaviour
{
    [SerializeField]
    private float targetRechedTrashold = 0.5F;

    [SerializeField]
    private bool showGizmo = true;


    bool reachedLastTarget = true;


    private Vector2 targetPositionCached;
    private float[] interestTemp;




    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aIData)
    {
        if(reachedLastTarget)
        {
            if(aIData.targets == null || aIData.targets.Count <= 0)
            {
                aIData.currentTarget = null;
                return(danger,interest);
            }
            else
            {
                reachedLastTarget = false;
                aIData.currentTarget = aIData.targets.OrderBy(target => Vector2.Distance(target.position, transform.position)).First();
            }
        }

        if(aIData.currentTarget != null && aIData.targets != null && aIData.targets.Contains(aIData.currentTarget))
        {
            targetPositionCached = aIData.currentTarget.position;
        }


        if(Vector2.Distance(transform.position, targetPositionCached) < targetRechedTrashold)
        {
            reachedLastTarget = true;
            aIData.currentTarget = null;
            return (danger,interest);
        }

        Vector2 directionToTarget = (targetPositionCached - (Vector2)transform.position);

        for(int i = 0; i < interest.Length; i++)
        {
            float result = Vector2.Dot(directionToTarget.normalized, Directions.eightDirections[i]);


            if(result > 0)
            {
                float valueToPutIn = result;
                if(valueToPutIn > interest[i])
                {
                    interest[i] = valueToPutIn;
                }
            }
        }

        interestTemp = interest;
        return (danger, interest);
    }

    private void OnDrawGizmos() 
    {
        if(showGizmo == false)
        {
            return;
        }

        Gizmos.DrawSphere(targetPositionCached, 0.2f);


        if(Application.isPlaying && interestTemp != null)
        {
            if(interestTemp != null)
            {
                Gizmos.color = Color.green;

                for(int i = 0; i < interestTemp.Length; i++)
                {
                    Gizmos.DrawRay(transform.position, Directions.eightDirections[i] * interestTemp[i]);
                }

                if(reachedLastTarget == false)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(targetPositionCached, 0.1f);
                }
            }
        }

    }
}
