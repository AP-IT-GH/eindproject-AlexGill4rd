using UnityEngine;

public class DustGenerator : MonoBehaviour
{
    public GameObject dustParticlePrefab; // Prefab of the dust particle
    public GameObject dustBonusPrefab; // Prefab of the DustBon,us particle
    public int numberOfParticles = 100; // Number of dust particles to spawn
    public int numberOfBonusParticles = 5; // Number of DustBon,us particles to spawn
    public bool spawnOnlyInAir = true; // Whether to spawn particles only in air

    private Collider airVolumeCollider;

    private void Start()
    {
        airVolumeCollider = GetComponent<Collider>();

        // Spawn dust particles and DustBon,us particles
        SpawnAllDustParticles();
        SpawnAllBonusParticles();
    }

    private void SpawnAllDustParticles()
    {
        for (int i = 0; i < numberOfParticles; i++)
        {
            Vector3 randomPoint = RandomPointInBounds();
            if (!spawnOnlyInAir || IsPointInAirVolume(randomPoint))
            {
                // Instantiate the dust particle without setting parent and then adjust scale
                GameObject dustParticle = Instantiate(dustParticlePrefab, randomPoint, Quaternion.identity);
                dustParticle.transform.localScale = dustParticlePrefab.transform.localScale;
            }
        }
    }

    private void SpawnAllBonusParticles()
    {
        for (int i = 0; i < numberOfBonusParticles; i++)
        {
            Vector3 randomPoint = RandomPointInBounds();
            if (!spawnOnlyInAir || IsPointInAirVolume(randomPoint))
            {
                // Instantiate the DustBon,us particle without setting parent and then adjust scale
                GameObject dustBonus = Instantiate(dustBonusPrefab, randomPoint, Quaternion.identity);
                dustBonus.transform.localScale = dustBonusPrefab.transform.localScale;
            }
        }
    }

    private Vector3 RandomPointInBounds()
    {
        Vector3 center = airVolumeCollider.bounds.center;
        float x = Random.Range(center.x - airVolumeCollider.bounds.extents.x, center.x + airVolumeCollider.bounds.extents.x);
        float y = Random.Range(center.y - airVolumeCollider.bounds.extents.y, center.y + airVolumeCollider.bounds.extents.y);
        float z = Random.Range(center.z - airVolumeCollider.bounds.extents.z, center.z + airVolumeCollider.bounds.extents.z);
        return new Vector3(x, y, z);
    }

    private bool IsPointInAirVolume(Vector3 point)
    {
        return airVolumeCollider.bounds.Contains(point);
    }
}
