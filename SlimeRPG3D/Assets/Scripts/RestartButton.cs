using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public static RestartButton instance;
    private void Awake()
    {
        instance = this;
    }
   
}
