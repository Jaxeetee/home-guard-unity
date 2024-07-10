using UnityEngine;

public class Trail : MonoBehaviour
{

    TrailRenderer _trail;
    void Awake()
    {
        _trail = GetComponent<TrailRenderer>();
    }

    public void SetMaterial(Material material)
    {
        _trail.material = material;
    }
}
