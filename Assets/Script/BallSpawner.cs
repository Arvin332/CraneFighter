using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BallEntry
{
    public BallType type;
    public GameObject prefab;
    public int weight;
}

public class BallSpawner : MonoBehaviour
{
    public List<BallEntry> ballTable;
    public Transform spawnPoint;
    public int batchSize = 20;
    public float spawnDelay = 0.05f;
    public float xRandomRange = 0.3f;
    public float yOffsetPerBall = 0.5f;
    public bool RefreshBall = false;


    private List<GameObject> activeBalls = new();

    public void SpawnBatch()
{
    StartCoroutine(SpawnBatchRoutine());
}


    public void RefreshBatch()
    {
        RefreshBall = true;
    }

    void SpawnBall(BallType type)
    {
        var entry = ballTable.Find(b => b.type == type);
        var ball = Instantiate(entry.prefab, spawnPoint.position, Quaternion.identity);
        activeBalls.Add(ball);
    }

    BallType GetRandomBallType()
    {
        int totalWeight = 0;
        foreach (var b in ballTable) totalWeight += b.weight;

        int roll = Random.Range(0, totalWeight);
        int current = 0;

        foreach (var b in ballTable)
        {
            current += b.weight;
            if (roll < current)
                return b.type;
        }

        return ballTable[0].type;
    }

    void ClearBatch()
    {
        foreach (var b in activeBalls)
            if (b) Destroy(b);

        activeBalls.Clear();
    }

IEnumerator SpawnBallRoutine(BallType type, int index)
{
    var entry = ballTable.Find(b => b.type == type);

    Vector3 pos = spawnPoint.position;
    pos.y += index * yOffsetPerBall;
    pos.x += Random.Range(-xRandomRange, xRandomRange);

    GameObject ball = Instantiate(entry.prefab, pos, Quaternion.identity);
    activeBalls.Add(ball);

    yield return new WaitForSeconds(spawnDelay);
}


    IEnumerator SpawnBatchRoutine()
{
    ClearBatch();

    int spawned = 0;

    // 1 Refresh dijamin
    yield return SpawnBallRoutine(BallType.Refresh, spawned++);

    while (spawned < batchSize)
    {
        BallType type = GetRandomBallType();
        yield return SpawnBallRoutine(type, spawned++);
    }
}

}


