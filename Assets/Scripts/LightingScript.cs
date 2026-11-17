using UnityEngine;

public class LightingScript : MonoBehaviour
{
    //different zones
    public enum Zone
    {
        Sunny,
        Foggy,
        Boss
    }

    public Zone zone;
    public Light sun;
    public bool applyStart = false;

    void Start()
    {
        if (applyStart)
        {
            ApplyLighting();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyLighting();
        }
    }

    void ApplyLighting()
    {
        if(zone == Zone.Sunny)
        {
            RenderSettings.fog = false;
            RenderSettings.ambientLight = new Color(1f, 1f, 1f);
            sun.intensity = 0.9f;
        }
        else if(zone == Zone.Foggy)
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.5f, 0.5f, 0.5f);
            RenderSettings.fogDensity = 0.034f;
            RenderSettings.fogMode = FogMode.ExponentialSquared;

            RenderSettings.ambientIntensity = 0.2f;
            sun.intensity = 0.15f;
        }
        else if(zone == Zone.Boss)
        {
            RenderSettings.fog = false;
            RenderSettings.ambientIntensity = 0.2f;
            sun.intensity = 0.3f;
        }
        Debug.Log("Entered: " + zone);
    }

}
