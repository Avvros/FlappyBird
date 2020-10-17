using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCtrl : MonoBehaviour
{
    [SerializeField] private float _speed = 2.0f;
    void Update()
    {
        Move();
    }

    /// <summary>
    /// Двигает трубы.
    /// </summary>
    private void Move()
    {
        transform.Translate(-1.0f * _speed * Time.deltaTime, 0, 0);
    }
}
