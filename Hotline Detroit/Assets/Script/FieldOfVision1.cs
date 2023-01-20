using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


namespace Pathfinding
{
    public class FieldOfVision1 : VersionedMonoBehaviour
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
        private bool waitForSwitch;

        public bool startCou;


        //private IEnumerator coroutine;

        
        void Start()
        {
            playerRef = GameObject.FindGameObjectWithTag("Player");
            //StartCoroutine(FOVCheck());
            FOVOn = true;

            //// - After 0 seconds, prints "Starting 0.0"
            //// - After 0 seconds, prints "Before WaitAndPrint Finishes 0.0"
            //// - After 2 seconds, prints "WaitAndPrint 2.0"
            //print("Starting " + Time.time);

            //// Start function WaitAndPrint as a coroutine.

            //coroutine = WaitAndPrint(3.0f);
            //StartCoroutine(coroutine);

            //Debug.Log("Before WaitAndPrint Finishes " + Time.time);
        }

        IEnumerator CoroutineExample()
        {
            yield return new WaitForSeconds(2);
            Debug.Log("nyeow");

            if (CanSeePlayer == false)
            {
                Debug.Log("SWITCH");
                waitForSwitch = false;
            }
            else
            {
                StopCoroutine("CoroutineExample");
                waitForSwitch = true;
                FOVOn = true;
            }
        }

        private void ForgetIt()
        {
            FOVOn = true;
            destSet.enabled = false;
            patrol.enabled = true;
            //aipath.canMove = true;
            aipath.maxSpeed = 6;
        }
        //private IEnumerator WaitAndPrint(float waitTime)
        //{
        //    if (CanSeePlayer == false)
        //    {
        //        yield return new WaitForSeconds(waitTime);
        //        Debug.Log("WaitAndPrint " + Time.time);
        //    }
        //    else
        //    {

        //    }
        //}

        //in case of need to go back, remove fixed update and uncomment StartCoroutine and IEnumerator
        private void FixedUpdate()
        {
            if (waitForSwitch == true)
            {
                StartCoroutine("CoroutineExample");
            }
            else
            {
                StopCoroutine("CoroutineExample");
            }

            if (waitForSwitch == false)
            {
                ForgetIt();
            }

            if (FOVOn == true)
            {
                FOV();
            }

            if (CanSeePlayer == true)
            {
                destSet.enabled = true;
                patrol.enabled = false;
                //aipath.canMove = false;
                aipath.maxSpeed = 2;
                waitForSwitch = true;
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
                    {
                        CanSeePlayer = true;
                        FOVOn = false;
                    }
                    else
                    {
                        CanSeePlayer = false;
                        FOVOn = true;
                    }
                }
                else
                {
                    CanSeePlayer = false;
                    FOVOn = true;
                }
            }
            else if (CanSeePlayer)
            {
                CanSeePlayer = false;
                FOVOn = true;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

            Vector3 angle01 = DirectionFromAngle(-transform.eulerAngles.z, -angle / 2);
            Vector3 angle02 = DirectionFromAngle(-transform.eulerAngles.z, angle / 2);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + angle01 * radius);
            Gizmos.DrawLine(transform.position, transform.position + angle02 * radius);

            if (CanSeePlayer)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, playerRef.transform.position);
            }
        }

        private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;

            return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}
