using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMoveCtrl : MonoBehaviour
{
    [SerializeField] private float _speed = 2.0f;
    void Update()
    {
        transform.Translate(-1.0f * _speed * Time.deltaTime, 0, 0);
    }
}
