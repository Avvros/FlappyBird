using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdCtrl : MonoBehaviour
{
    public UnityEvent OnScoreZoneEnter;

    private Rigidbody2D _rigidbody;
    [SerializeField] private float _flyUpForce = 4.0f;
    [SerializeField] private GameObject _restartBtn;
    [SerializeField] private GameObject _failText;
    [SerializeField] private GameObject _flyManager;
    [SerializeField] private int _cameraViewLimit = 6;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _failText.SetActive(false);
        _restartBtn.SetActive(false);
        _flyManager.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            FlyUp();
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.y >= _cameraViewLimit ||
            transform.position.y <= -_cameraViewLimit) // При вылете за рамки камеры.
        {
            EndGame();
        }

        Debug.Log(Time.timeScale);
    }

    /// <summary>
    /// Взлёт вверх.
    /// </summary>
    public void FlyUp()
    {
        _rigidbody.velocity = Vector2.up * _flyUpForce;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ScoreZone")
        {
            OnScoreZoneEnter?.Invoke();
        }
        Destroy(collision.gameObject);
    }

    /// <summary>
    /// Заканчивание игру.
    /// </summary>
    internal void EndGame()
    {
        Destroy(gameObject);
        Time.timeScale = 0;
        _restartBtn.SetActive(true);
        _failText.SetActive(true);
        _flyManager.SetActive(false);
    }
}
