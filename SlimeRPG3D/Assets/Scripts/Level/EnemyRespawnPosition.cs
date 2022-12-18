using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnPosition : MonoBehaviour
{
    public static EnemyRespawnPosition instance;
    private void Awake()
    {
        instance = this;
    }
    public Vector3 GetPosition() { return transform.position; }
}
