using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PathGenerator : MonoBehaviour
{
    public GameObject[] Roads;
    private Transform NextNode;
    private List<GameObject> _activeRoads = new();

    private const int MaxRoads = 50;
    private Transform RoadParent;
    private bool _spawnRoad = true;
    
    public Path pathController;

    void Start()
    {
        pathController = FindObjectOfType<Path>();
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
        pathController.AddNode(NextNode);
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