using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class FlickeringLight : MonoBehaviour
{
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
