using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class FlickeringLight : MonoBehaviour
{
    public float intensityRange;
    public bool flickIntensity;
    public float _baseIntensity;
    public UnityEngine.Rendering.Universal.Light2D _light;
    public float intensityTimeMin;
    public float intensityTimeMax;

    [SerializeField] float firstValue = 0f;
    [SerializeField] float secondValue = 0.36f;
    [SerializeField] float secondsBetweenFlickers = 2f;

    private bool lightTrue = true;

    public Light2D renderLight;

    void Start()
    {
        renderLight.intensity = renderLight.intensity;
        StartCoroutine(TimerLight());
    }
 main

    private void Awake()
    {
        renderLight = GetComponent​<UnityEngine.Rendering.Universal.Light2D>();
    }

    IEnumerator TimerLight()
    {
        while (lightTrue == true)
        {
            renderLight.intensity = Random.Range(firstValue, secondValue);
            var randomTime = Random.Range(0, secondsBetweenFlickers);
            yield return new WaitForSeconds(randomTime);
        }
    }
}
