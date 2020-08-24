using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] float secondsBetweenSpawns = 3f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Text scoreText;

    int score = 0;
    Waypoint startWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        //scoreText.text = score.ToString();
        PathFinder pathfinder = FindObjectOfType<PathFinder>();
        startWaypoint = pathfinder.GetWaypoint();
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        IncrementScore();
        InstantiatEnemy();

        yield return new WaitForSeconds(secondsBetweenSpawns);
        StartCoroutine(SpawnEnemy());
    }

    private void InstantiatEnemy()
    {
        var newEnemy = Instantiate<EnemyMovement>(enemyPrefab);
        newEnemy.transform.position = startWaypoint.transform.position;
        newEnemy.transform.SetParent(gameObject.transform);
    }

    private void IncrementScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
