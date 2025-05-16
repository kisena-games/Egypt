
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class LightManager : MonoBehaviour
{
    [SerializeField] private List<Light> _lightPoints;

    [SerializeField] private float _intensity,_range;
    private void Update()
    {  
       
        foreach (Light light in _lightPoints)
        {
            light.range = _range;
            light.intensity=_intensity;
        }
    }
}
