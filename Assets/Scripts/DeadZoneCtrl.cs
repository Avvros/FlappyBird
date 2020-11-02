using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DeadZoneCtrl : MonoBehaviour
{
    [SerializeField] private GameObject _bird;
    private BirdCtrl _birdHandler;

    private void Start()
    {
        _birdHandler = _bird.GetComponent<BirdCtrl>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" || 
            collision.collider.tag == "SheldedPlayer")
        {
            _birdHandler.EndGame();
        }
    }
}
