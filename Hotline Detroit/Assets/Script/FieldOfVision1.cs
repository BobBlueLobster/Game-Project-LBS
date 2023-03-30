using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

//Check the original FieldOfVision if you need to revert back to the old code

namespace Pathfinding
{
    public class FieldOfVision1 : VersionedMonoBehaviour
    {
        public float radius = 5;
        [Range(1, 360)] public float angle = 45;
        public LayerMask targetLayer;
        public LayerMask obstructionLayer;
        public Collider2D[] rangeCheck { get; private set; }


        //playerRef and playerTransform possibly only for drawing gizmos
        public AIPath aipath;
        //public GameObject playerRef;
        //public Transform playerTransform;
        public AIDestinationSetter destSet;
        public Patrol patrol;

        private GameObject grid;
        public Gun gun;

        public bool CanSeePlayer { get; private set; }
        static public bool FOVOn { get; private set; }
        private bool waitForSwitch;
        //canShoot bool for when enemy is chasing player
        private bool canShoot;
        public bool heardPlayer;
        

        //Check the original FieldOfVision if you need to revert back to the old code
        void Start()
        {
            //playerRef = GameObject.FindGameObjectWithTag("Player");
            //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            //StartCoroutine(FOVCheck());
            grid = GameObject.Find("Grid");
            gun = GameObject.Find("Gun").GetComponent<Gun>();
            FOVOn = true;
        }

        IEnumerator CoroutineExample()
        {
            yield return new WaitForSeconds(5);
            Debug.Log("nyeow");
            if (CanSeePlayer == false)
            {
                Debug.Log("SWITCH");
                waitForSwitch = false;
                heardPlayer = false;
            }
            else
            {
                StopCoroutine("CoroutineExample");
                waitForSwitch = true;
                FOVOn = true;
            }
        }

        //in case of need to go back, remove fixed update and uncomment StartCoroutineFOVCheck and IEnumeratorFOVCheck
        //and let FOV be out of the if statement, just in the FixedUpdate
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
                aipath.maxSpeed = 2;
                waitForSwitch = true;
                canShoot = true;
            }

            if (heardPlayer == true)
            {
                destSet.enabled = true;
                patrol.enabled = false;
                destSet.target = gun.temporaryGunObject.transform;
                waitForSwitch = true;
            }
        }

        

        private void ForgetIt()
        {
            FOVOn = true;
            destSet.enabled = false;
            patrol.enabled = true;
            aipath.maxSpeed = 6;
            canShoot = false;
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

                    //give the desired the Layer that the enemy should follow/trigger (in this case: Player)
                    if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                    {
                        CanSeePlayer = true;
                        FOVOn = false;
                        destSet.target = target;
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
        //        //Gizmos.DrawLine(transform.position, playerTransform.position);
        //    }
        //}

        private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;

            return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}
