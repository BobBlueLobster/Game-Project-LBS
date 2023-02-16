using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;

public class Testingg : MonoBehaviour
{
    [SerializeField] private CharacterPathfindingMovementHandler characterPathfinding;
    private Pathfindingg pathfindingg;
    private void Start()
    {
        pathfindingg = new Pathfindingg(20, 20);

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseworldPosition = UtilsClass.GetMouseWorldPosition();
            pathfindingg.GetGrid().GetXY(mouseworldPosition, out int x, out int y);
            List<PathNode> path = pathfindingg.FindPath(0, 0, x, y);
            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 3f + Vector3.one /** 5f*/, new Vector3(path[i + 1].x, path[i + 1].y) * 3f + Vector3.one /** 5f*/, Color.green, .5f);
                }
            }
            characterPathfinding.SetTargetPosition(mouseworldPosition);
        }
    }
}