using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DeadZoneCtrl : MonoBehaviour
{
    [SerializeField] private GameObject _bird;
    BirdCtrl _birdCtrl;

    private void Start()
    {
        _birdCtrl = _bird.GetComponent<BirdCtrl>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" || collision.collider.tag == "SheldedPlayer")
        {
            _birdCtrl.EndGame();
        }
    }
}
