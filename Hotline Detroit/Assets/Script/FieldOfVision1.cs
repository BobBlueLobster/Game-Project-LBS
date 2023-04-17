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

        public AIPath aipath;
        public AIDestinationSetter destSet;
        public Patrol patrol;
        public Gun gun;
        public Enemy enemy;

        public bool CanSeePlayer { get; private set; }
        [SerializeField]static public bool FOVOn { get; private set; }
        private bool waitForSwitch;

        //Check the original FieldOfVision if you need to revert back to the old code
        void Start()
        {
            //playerRef = GameObject.FindGameObjectWithTag("Player");
            //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            //StartCoroutine(FOVCheck());
            gun = GameObject.Find("Gun").GetComponent<Gun>();
            FOVOn = true;
            if (patrol.targets.Length == 0)
            {
                destSet.enabled = true;
                patrol.enabled = false;
            }
            else
            {
                patrol.enabled = true;
                destSet.enabled = false;
            }
        }

        IEnumerator CoroutineExample()
        {
            yield return new WaitForSeconds(10);
            Debug.Log("nyeow");
            if (CanSeePlayer == false)
            {
                Debug.Log("SWITCH");
                waitForSwitch = false;
                gun.heardPlayer = false;
            }
            else
            {
                StopCoroutine("CoroutineExample");
                waitForSwitch = true;
                FOVOn = true;
            }
        }

        IEnumerator waitPlease()
        {
            yield return new WaitForSeconds(10);
        }

        //in case of need to go back, remove fixed update and uncomment StartCoroutineFOVCheck and IEnumeratorFOVCheck
        //and let FOV be out of the if statement, just in the FixedUpdate
        private void FixedUpdate()
        {
            gun = GameObject.Find("Gun").GetComponent<Gun>();

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
                Debug.Log("SeX");
                FOV();
            }

            if (CanSeePlayer == true)
            {
                destSet.enabled = true;
                patrol.enabled = false;
                aipath.maxSpeed = 2;
                waitForSwitch = true;
            }
            
            
            if(CanSeePlayer == false)
            {
                FOVOn = true;
                Debug.Log(gun.heardPlayer);
                if (gun.heardPlayer == true /*&& !gun.temporaryGunObject*/)
                {

                    destSet.enabled = true;
                    patrol.enabled = false;
                    destSet.target = gun.temporaryGunObject.transform;
                    Debug.Log(destSet.target);
                    waitForSwitch = true;
                }
            }

            


            //else
            //{
            //    destSet.target = enemy.temporaryEnemyTransform.transform;
            //}

            //if (gun.temporaryGunObject == null)
            //{
            //    destSet.enabled = true;
            //    destSet.target = enemy.temporaryEnemyTransform.transform;
            //}
        }

        

        private void ForgetIt()
        {
            FOVOn = true;
            aipath.maxSpeed = 6;
            if (patrol.targets.Length == 0)
            {
                destSet.enabled = true;
                destSet.target = enemy.temporaryEnemyTransform.transform;
                //StartCoroutine("waitPlease");
                //destSet.target = null;
            }
            else
            {
                patrol.enabled = true;
                destSet.enabled = false;
            }
        }

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
                        FOVOn = true;
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

        private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;

            return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}
