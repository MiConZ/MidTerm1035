using UnityEngine;
using UnityEngine.Rendering.Universal; 

public class AutoLightPulse : MonoBehaviour
{
    private Light2D myLight;

    [Header("Settings")]
    public float minIntensity = 0.5f; 
    public float maxIntensity = 1.5f; 
    public float speed = 1.0f;        

    void Start()
    {
        myLight = GetComponent<Light2D>();
    }

    void Update()
    {

        float noise = Mathf.PerlinNoise(Time.time * speed, 0);
       
        float displayIntensity = Mathf.Lerp(minIntensity, maxIntensity, (noise + 1f) / 2f);

       
        myLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
       
    }
}