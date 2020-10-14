using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
public class BirdCtrl : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _flyUpForce = 4.0f;
    [SerializeField] private GameObject _restartBtn;
    [SerializeField] private GameObject _failText;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _failText.SetActive(false);
        _restartBtn.SetActive(false);
    }
    void Update()
    {
    }

    public void FlyUp()
    {
        _rb.velocity = Vector2.up * _flyUpForce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Pipe" || collision.collider.tag == "Ground")
        {
            Destroy(gameObject);
            Time.timeScale = 0;
            _restartBtn.SetActive(true);
            _failText.SetActive(true);
        }
    }
}
