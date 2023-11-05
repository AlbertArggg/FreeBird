using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public GameObject[] Roads;
    private Transform NextNode;
    private List<GameObject> _activeRoads = new List<GameObject>();

    private const int MaxRoads = 50;
    private Transform RoadParent;
    private bool _spawnRoad = true;

    void Start()
    {
        for (int i = 0; i < MaxRoads; i++)
        {
            GenerateRoad();
        }
        StartCoroutine(ContinuousRoadSpawn());
    }

    void GenerateRoad()
    {
        GameObject randomRoad = Roads[Random.Range(0, Roads.Length)];

        GameObject instantiatedRoad;
        if (NextNode == null)
        {
            instantiatedRoad = Instantiate(randomRoad);
        }
        else
        {
            instantiatedRoad = Instantiate(randomRoad, NextNode.position, NextNode.rotation);
        }

        _activeRoads.Add(instantiatedRoad);
        NextNode = instantiatedRoad.GetComponent<Road>().endNode;
    }

    IEnumerator ContinuousRoadSpawn()
    {
        while (_spawnRoad)
        {
            yield return new WaitForSeconds(0.5f);
            GenerateRoad();
            
            if (_activeRoads.Count > MaxRoads)
            {
                Destroy(_activeRoads[0]);
                _activeRoads.RemoveAt(0);
            }
        }
    }
}