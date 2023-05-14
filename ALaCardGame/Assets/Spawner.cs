using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Vector3[] spawnPositions;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        foreach (var pos in spawnPositions)
        {
            Gizmos.DrawSphere(pos,0.15f);
        }
    }
}
