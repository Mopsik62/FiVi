using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FirstLevelDark : MonoBehaviour
{
    public Light2D lightScene;

        public void Dark()
    {
        StartCoroutine(FadeLight(lightScene, 1f));
    }
    private IEnumerator FadeLight(Light2D light, float duration)
    {
        float startIntensity = light.intensity;
        float time = 0f;

        while (time < duration)
        {
            light.intensity = Mathf.Lerp(startIntensity, 0f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        GameManager.instance.MakePlayerMove();
        light.intensity = 1f;
    }
}
