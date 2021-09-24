using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour {
    [SerializeField] private GameObject[] asteroidPrefabs;
    [SerializeField] private float secondsBetweenAsteroids = 1.5f;
    [SerializeField] private Vector2 forceRange;

    private float timer;

    private Camera mainCamera;

    private void Start() {
        mainCamera = Camera.main;
    }

    void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            SpawnAsteroid();
            timer += secondsBetweenAsteroids;
        }
    }

    private void SpawnAsteroid() {
        var side = Random.Range(0, 4);
        var viewportSpawnPoint = Vector2.zero;
        var direction = Vector3.zero;
        switch (side) {
            case 0:
                viewportSpawnPoint.x = 0;
                viewportSpawnPoint.y = Random.value;
                direction = new Vector3(1f, 0, Random.Range(-1f, 1f));
                break;
            case 1:
                viewportSpawnPoint.x = Random.value;
                viewportSpawnPoint.y = 1;
                direction = new Vector3(Random.Range(-1f, 1f), 0, -1f);
                break;
            case 2:
                viewportSpawnPoint.x = 1;
                viewportSpawnPoint.y = Random.value;
                direction = new Vector3(-1f, 0, Random.Range(-1f, 1f));
                break;
            case 3:
                viewportSpawnPoint.x = Random.value;
                viewportSpawnPoint.y = 0;
                direction = new Vector3(Random.Range(-1f, 1f), 0, 1f);
                break;
        }

        var spawnPoint = mainCamera.ViewportToWorldPoint(viewportSpawnPoint);
        spawnPoint.y = 0;
        var spawnedAsteroid = Instantiate(
            asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)],
            spawnPoint,
            RandomRotation());
        var asteroidRigidbody = spawnedAsteroid.GetComponent<Rigidbody>();
        asteroidRigidbody.velocity = direction.normalized * Random.Range(forceRange.x, forceRange.y);
    }
    
    private static float Random360() => Random.Range(0f, 360f);

    private static Quaternion RandomRotation() => Quaternion.Euler(Random360(), Random360(), Random360());
}
