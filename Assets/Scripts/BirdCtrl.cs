using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdCtrl : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _flyUpForce = 4.0f;
    [SerializeField] private GameObject _restartBtn;
    [SerializeField] private GameObject _failText;
    [SerializeField] private int _cameraViewLimit = 6;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _failText.SetActive(false);
        _restartBtn.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (transform.position.y >= _cameraViewLimit ||
            transform.position.y <= -_cameraViewLimit) // При вылете за рамки камеры.
        {
            EndGame();
        }
    }

    /// <summary>
    /// Взлёт вверх.
    /// </summary>
    public void FlyUp()
    {
        _rb.velocity = Vector2.up * _flyUpForce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameObject.GetComponent<Collider2D>().tag == "Player" &&
            collision.collider.tag == "Pipe" ||
            collision.collider.tag == "Ground")
        {
            EndGame();
        }
    }

    /// <summary>
    /// Заканчивание игры.
    /// </summary>
    internal void EndGame()
    {
        Destroy(gameObject);
        Time.timeScale = 0;
        _restartBtn.SetActive(true);
        _failText.SetActive(true);
    }
}
