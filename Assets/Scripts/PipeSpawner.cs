using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Pipe;
    void Start()
    {
        SpawnPipe(5, 5);
        StartCoroutine(Spawner());
    }
    /// <summary>
    /// Безконечное создание труб.
    /// </summary>
    IEnumerator Spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            SpawnPipe(3.0f, 7.0f);
        }
    }
    /// <summary>
    /// Создаёт трубы.
    /// </summary>
    /// <param name="minHeight">Минимальная высота трубы.</param>
    /// <param name="maxHeight">Максимальная высота трубы.</param>
    private void SpawnPipe(float minHeight, float maxHeight)
    {
        float rand = Random.Range(minHeight, maxHeight);
        GameObject newPipe = Instantiate(Pipe, new Vector3(4, rand, 0), Quaternion.identity);
        Destroy(newPipe, 8);
    }
}
