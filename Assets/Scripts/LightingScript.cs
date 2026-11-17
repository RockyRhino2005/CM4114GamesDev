using UnityEngine;
using System.Collections;

public class LightingScript : MonoBehaviour
{
    //different zones
    //updated to have fading fog
    public enum Zone
    {
        Sunny,
        Foggy,
        Boss
    }

    public Zone zone;
    public Light sun;
    public bool applyStart = false;

    public float fadeDuration = 3f;
    private Coroutine fadeRoutine;
    private static LightingScript instance;

    //makes instance of the lighting script
    void Start()
    {
        if (applyStart)
        {
            GetSettings(out float fog, out float ambient,
                        out float sunlight, out Color color);

            RenderSettings.fog = true;
            RenderSettings.fogMode = FogMode.ExponentialSquared;
            RenderSettings.fogDensity = fog;
            RenderSettings.fogColor = color;
            RenderSettings.ambientIntensity = ambient;
            sun.intensity = sunlight;
        }
    }

    //when the player enters the trigger, start fading to the new lighting settings
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (instance != null &&
                instance.fadeRoutine != null)
            {
                instance.StopCoroutine(instance.fadeRoutine);
            }

            instance = this;
            fadeRoutine = StartCoroutine(FadeLighting());
        }
    }

    //different lighting settings for different zones
    void GetSettings(out float fog, out float ambient,
                     out float sunlight, out Color color)
    {
        if (zone == Zone.Foggy)
        {
            fog = 0.055f;
            ambient = 0.2f;
            sunlight = 0.15f;
            color = new Color(0.2f, 0.23f, 0.27f);
        }
        else if (zone == Zone.Boss)
        {
            fog = 0.005f;
            ambient = 0.5f;
            sunlight = 0.5f;
            color = Color.gray;
        }
        else
        {
            fog = 0f;
            ambient = 1f;
            sunlight = 1.2f;
            color = Color.white;
        }
    }

    //fade the lighting settings over time
    IEnumerator FadeLighting()
    {
        GetSettings(out float targetFog, out float targetAmbient,
                    out float targetSun, out Color targetColor);

        float startFog = RenderSettings.fogDensity;
        float startAmbient = RenderSettings.ambientIntensity;
        float startSun = sun.intensity;
        Color startColor = RenderSettings.fogColor;

        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.ExponentialSquared;

        float time = 0f;

        //fade over time
        while (time < 1f)
        {
            time += Time.deltaTime / Mathf.Max(0.01f, fadeDuration);

            float t = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(time));

            RenderSettings.fogDensity =
                Mathf.Lerp(startFog, targetFog, t);

            RenderSettings.ambientIntensity =
                Mathf.Lerp(startAmbient, targetAmbient, t);

            sun.intensity =
                Mathf.Lerp(startSun, targetSun, t);

            RenderSettings.fogColor =
                Color.Lerp(startColor, targetColor, t);

            yield return null;
        }

        fadeRoutine = null;
    }
}
