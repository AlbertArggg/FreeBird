using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    private List<Transform> nodesList = new();
    private float t;

    void Update()
    {
        if (nodesList.Count >= 4)
        {
            Vector3 newPosition = CatmullRomSpline.GetPosition(
                nodesList[0].position,
                nodesList[1].position,
                nodesList[2].position,
                nodesList[3].position,
                t
            );

            Vector3 tangent = CatmullRomSpline.GetTangent(
                nodesList[0].position,
                nodesList[1].position,
                nodesList[2].position,
                nodesList[3].position,
                t
            ).normalized;
            
            transform.position = Vector3.Lerp(transform.position, newPosition, moveSpeed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(tangent);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            
            t += Time.deltaTime * moveSpeed;

            if (t >= 1f)
            {
                nodesList.RemoveAt(0);
                t = 0f;
            }
        }
    }

    public void AddNode(Transform node)
    {
        nodesList.Add(node);
    }
}
