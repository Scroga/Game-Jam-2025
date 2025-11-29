using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightsController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> lights = new();
    [SerializeField]
    private float duration = 0.8f;
    public bool IsBlinkingMode = false;
    void Start()
    {
        foreach (var light in lights) {
            light.SetActive(true);
        }      
    }

    // Update is called once per frame
    void Update()
    {
        if (IsBlinkingMode)
        {
            StartCoroutine( BlinkingMode());
        }
        else {
            StartCoroutine(AlternatingFlashing());
        }
    }

    private IEnumerator AlternatingFlashing() {
        foreach (var light in lights)
        {
            light.SetActive(true);
        }
        yield return new WaitForSecondsRealtime(duration);

        foreach (var light in lights)
        {
            light.SetActive(false);
        }
    }

    private IEnumerator BlinkingMode() {
        for (int i = 0; i < lights.Count; i++) {
            int j = i - 1;
            j = j < 0 ? lights.Count - j : j;

            lights[i].SetActive(false);
            lights[j].SetActive(true);

            yield return new WaitForSecondsRealtime(duration);
        }
    }
}
