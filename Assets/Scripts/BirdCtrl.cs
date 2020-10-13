using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
public class BirdCtrl : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _flyUpForce = 4.0f;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        FlyUp();
    }

    private void FlyUp()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            _rb.velocity = Vector2.up * _flyUpForce;
        }
    }
}
