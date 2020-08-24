using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit = 3;
    [SerializeField] Tower towerPrefab;
    [SerializeField] Transform towerParent;

    Queue<Tower> towerQueue = new Queue<Tower>();

    int currentTowerCount = 0;

    // Start is called before the first frame update
    public void AddTower(Waypoint baseWaypoint)
    {
        Debug.Log(towerQueue.Count);
        if (towerQueue.Count >= towerLimit)
        {
            MoveExistingTower(baseWaypoint);
        }
        else
        {
            InstantiateNewTower(baseWaypoint);
        }
    }

    private void MoveExistingTower(Waypoint baseWaypoint)
    {
        var oldTower = towerQueue.Dequeue();
        oldTower.baseWaypoint.isPlacable = true;
        baseWaypoint.isPlacable = false;

        oldTower.baseWaypoint = baseWaypoint;

        oldTower.transform.position = baseWaypoint.transform.position;

        towerQueue.Enqueue(oldTower);
    }

    private void InstantiateNewTower(Waypoint baseWaypoint)
    {
        var newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        newTower.transform.SetParent(towerParent);

        newTower.baseWaypoint = baseWaypoint;
        baseWaypoint.isPlacable = false;

        towerQueue.Enqueue(newTower);
    }

}
