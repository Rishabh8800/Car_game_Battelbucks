using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Settings")]
    public GameObject[] obstaclePrefabs;
    public int poolSize = 15;

    [Header("Spawn Settings")]
    public float spawnDistance = 50f;
    public float laneOffset = 2.5f;
    public float segmentSpacing = 10f;

    [Header("Recycle Settings")]
    public float offCameraZ = -20f;

    private Transform[] obstacles;

    private void Start()
    {
        if (obstaclePrefabs == null || obstaclePrefabs.Length == 0)
        {
            Debug.LogError("No obstacle prefabs assigned!");
            return;
        }

        obstacles = new Transform[poolSize];

        // Initial spawn in WORLD SPACE
        for (int i = 0; i < poolSize; i++)
        {
            SpawnAtIndex(i);
        }
    }

    private void Update()
    {
        if (obstacles == null) return;

        RecycleObstacles();
    }

    void SpawnAtIndex(int index)
    {
        int lane = Random.Range(-1, 2);
        int randomObstacle = Random.Range(0, obstaclePrefabs.Length);

        Vector3 pos = new Vector3(
            lane * laneOffset,
            0f,
            spawnDistance + (index * segmentSpacing)
        );

        GameObject obj = Instantiate(obstaclePrefabs[randomObstacle], pos, Quaternion.identity);

        // Ensure movement script exists
        if (obj.GetComponent<MovingBody>() == null)
        {
            obj.AddComponent<MovingBody>();
        }

        obstacles[index] = obj.transform;
    }

    void RecycleObstacles()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (obstacles[i] == null) continue;

            if (obstacles[i].position.z <= offCameraZ)
            {
                int lane = Random.Range(-1, 2);
                int randomObstacle = Random.Range(0, obstaclePrefabs.Length);

                Vector3 newPos = new Vector3(
                    lane * laneOffset,
                    0f,
                    GetFarthestZ() + segmentSpacing
                );

                Destroy(obstacles[i].gameObject);

                GameObject newObj = Instantiate(obstaclePrefabs[randomObstacle], newPos, Quaternion.identity);

                // Ensure movement script exists
                if (newObj.GetComponent<MovingBody>() == null)
                {
                    newObj.AddComponent<MovingBody>();
                }

                obstacles[i] = newObj.transform;
            }
        }
    }

    float GetFarthestZ()
    {
        float maxZ = float.MinValue;

        foreach (Transform t in obstacles)
        {
            if (t == null) continue;

            if (t.position.z > maxZ)
                maxZ = t.position.z;
        }

        return maxZ;
    }
}