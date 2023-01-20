using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


namespace Pathfinding
{
    public class FieldOfVision : VersionedMonoBehaviour
    {
        public float radius = 5;
        [Range(1, 360)] public float angle = 45;
        public LayerMask targetLayer;
        public LayerMask obstructionLayer;
        public Collider2D[] rangeCheck { get; private set; }

        public AIPath aipath;
        public GameObject playerRef;
        public AIDestinationSetter destSet;
        public Patrol patrol;

        static public bool CanSeePlayer { get; private set; }
        static public bool FOVOn { get; private set; }
        void Start()
        {
            playerRef = GameObject.FindGameObjectWithTag("Player");
            //StartCoroutine(FOVCheck());
        }

        //in case of need to go back, remove fixed update and uncomment StartCoroutine and IEnumerator
        private void FixedUpdate()
        {

            FOV();

            if (CanSeePlayer == true)
            {
                destSet.enabled = true;
                patrol.enabled = false;
                //aipath.canMove = false;
                aipath.maxSpeed = 2;
            }
            else
            {
                destSet.enabled = false;
                patrol.enabled = true;
                //aipath.canMove = true;
                aipath.maxSpeed = 6;
            }
        }



        //private IEnumerator FOVCheck()
        //{
        //    WaitForSeconds wait = new WaitForSeconds(0.2f);

        //    while (true)
        //    {
        //        yield return wait;
        //        FOV();
        //    }
        //}

        private void FOV()
        {
            rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

            if (rangeCheck.Length > 0)
            {
                Transform target = rangeCheck[0].transform;
                Vector2 directionToTarget = (target.position - transform.position).normalized;

                if (Vector2.Angle(transform.up, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector2.Distance(transform.position, target.position);

                    if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                        CanSeePlayer = true;
                    else
                        CanSeePlayer = false;
                }
                else
                    CanSeePlayer = false;
            }
            else if (CanSeePlayer)
                CanSeePlayer = false;
        }

        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.white;
        //    UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

        //    Vector3 angle01 = DirectionFromAngle(-transform.eulerAngles.z, -angle / 2);
        //    Vector3 angle02 = DirectionFromAngle(-transform.eulerAngles.z, angle / 2);

        //    Gizmos.color = Color.yellow;
        //    Gizmos.DrawLine(transform.position, transform.position + angle01 * radius);
        //    Gizmos.DrawLine(transform.position, transform.position + angle02 * radius);

        //    if (CanSeePlayer)
        //    {
        //        Gizmos.color = Color.green;
        //        Gizmos.DrawLine(transform.position, playerRef.transform.position);
        //    }
        //}

        //private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
        //{
        //    angleInDegrees += eulerY;

        //    return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        //}
    }
}
