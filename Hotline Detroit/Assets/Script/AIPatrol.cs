using UnityEngine;
using System.Collections;
using Pathfinding;

public class AIPatrol : MonoBehaviour
{
    public float radius = 20;
    public Transform levelTransform;

    IAstarAI ai;

    void Start()
    {
        ai = GetComponent<IAstarAI>();
        //levelTransform = GetComponent<Transform>();
    }

    Vector2 PickRandomPoint()
    {
        var point = Random.insideUnitSphere * radius;

        point.z = 0;
        point += levelTransform.position;
        return point;
    }

    void Update()
    {
        // Update the destination of the AI if
        // the AI is not already calculating a path and
        // the ai has reached the end of the path or it has no path at all
        if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
        {
            ai.destination = PickRandomPoint();
            ai.SearchPath();

        }
    }

    void OnDrawGizmos()
    {
        // Take transform of the level so the level is the middle of the gizmos so you can resize the sphere as you wish
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(levelTransform.position, radius);
    }
}