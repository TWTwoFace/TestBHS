using UnityEngine;

public class DecalPlacer : MonoBehaviour, IHaveProjectileReaction
{
    [SerializeField] private ImpactScript[] _impactPrefabs;

    public void React(Collision collision)
    {
        Instantiate(_impactPrefabs[Random.Range
                (0, _impactPrefabs.Length)], collision.contacts[0].point,
                Quaternion.LookRotation(collision.contacts[0].normal));
    }
}
