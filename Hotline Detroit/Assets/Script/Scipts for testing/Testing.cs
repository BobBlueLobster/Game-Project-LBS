using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{
    private Grid<HeatMapGridObject> grid;

    private void Start()
    {
        grid = new Grid<HeatMapGridObject>(4, 2, 10f, new Vector3(20, 0), () => new HeatMapGridObject());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = UtilsClass.GetMouseWorldPosition();
            HeatMapGridObject heatMapGridObject = grid.GetGridObject(position);
            if (heatMapGridObject != null)
            {
                heatMapGridObject.AddValue(5);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetGridObject(UtilsClass.GetMouseWorldPosition()));
        }
    }
}

public class HeatMapGridObject
{
    private int MIN = 0;
    private int MAX = 100;
    public int value;
    
    public  void AddValue(int addValue)
    {
        value += addValue;
        Mathf.Clamp(value, MIN, MAX);
    }

    public float GetValueNormalized()
    {
        return (float)value / MAX;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}
