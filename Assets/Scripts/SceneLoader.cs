using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject _darkMask;

    public void LoadGameScene()
    {
        StartCoroutine(ScreenDarker());
    }

    IEnumerator ScreenDarker()
    {
        float alpha = 0f;

        while(_darkMask.GetComponent<SpriteRenderer>().color.a < 0.9f)
        {
            alpha += 0.02f;
            _darkMask.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, alpha);
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(1);
        StopCoroutine(ScreenDarker());
    }
}
