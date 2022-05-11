using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitFPS : MonoBehaviour
{
    [Range(0, 1000)]
    [SerializeField] private int _FPS;

    void Start()
    {
        Application.targetFrameRate = _FPS;
    }
}
