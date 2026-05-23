using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Final : MonoBehaviour
{
    [SerializeField] private GameObject finalPanel;
    [SerializeField] private Image blackBackground;
    [SerializeField] private float fadeDuration = 5f;
    private void Start()
    {
        finalPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(FadeToBlack());
        }
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu");
    }
    private IEnumerator FadeToBlack()
    {
        float timer = 0f;
        Color color = blackBackground.color;

        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            color.a = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            blackBackground.color = color;
            yield return null;
        }
        color.a = 1f;
        blackBackground.color = color;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(1.0f);

        finalPanel.SetActive(true);
    }
}
