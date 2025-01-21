using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColor : MonoBehaviour
{
    
    private Renderer _renderer;
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void ChangeColor(Color newColor, float duration)
    {
        StartCoroutine(ChangeColorCoroutine(newColor, duration));
    }

    private IEnumerator ChangeColorCoroutine(Color newColor, float duration)
    {
        var currentTime = 0f;
        while (currentTime <= duration)
        {
            _renderer.material.color = Color.Lerp(_renderer.material.color, newColor, currentTime);
            currentTime += Time.deltaTime;
            yield return null;
        }
        
    }
}
