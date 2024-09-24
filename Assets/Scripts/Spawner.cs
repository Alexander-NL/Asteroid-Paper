using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid asteroidPrefab;
    public Asteroid specialAsteroidPrefab;
    public Asteroid totallySpecialAsteroidPrefab;

    public float spawnDistance = 12f;
    public float spawnRate = 1f;
    public int amountPerSpawn = 1;
    public float trajectoryVariance = 15f;

    private bool specialAsteroidSpawned = false;
    private bool totallySpecialAsteroidSpawned = false;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    public void Spawn()
    {
        for (int i = 0; i < amountPerSpawn; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPoint = transform.position + (spawnDirection * spawnDistance);

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);

            Vector2 trajectory = rotation * -spawnDirection;
            asteroid.SetTrajectory(trajectory);
        }
    }

    public void SpawnSpecialAsteroid()
    {
        if (specialAsteroidSpawned) return;

        Vector3 spawnDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPoint = transform.position + (spawnDirection * spawnDistance);

        float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
        Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

        Asteroid specialAsteroid = Instantiate(specialAsteroidPrefab, spawnPoint, rotation);
        specialAsteroid.size = specialAsteroid.maxSize;

        Vector2 trajectory = rotation * -spawnDirection;
        specialAsteroid.SetTrajectory(trajectory);
    }

    public void SpawnTotallySpecialAsteroid()
    {
        if (totallySpecialAsteroidSpawned) return;

        Vector3 spawnDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPoint = transform.position + (spawnDirection * spawnDistance);

        float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
        Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

        Asteroid specialAsteroid = Instantiate(totallySpecialAsteroidPrefab, spawnPoint, rotation);
        specialAsteroid.size = specialAsteroid.maxSize;

        Vector2 trajectory = rotation * -spawnDirection;
        specialAsteroid.SetTrajectory(trajectory);
    }

    public void StopSpawning(){
        CancelInvoke();
    }
}