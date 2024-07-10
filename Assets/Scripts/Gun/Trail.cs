using UnityEngine;

public class Trail : MonoBehaviour
{

    private TrailRenderer _trail;
    private void Awake()
    {
        _trail = GetComponent<TrailRenderer>();
    }

    public void SetMaterial(Material material)
    {
        _trail.material = material;
    }
}
