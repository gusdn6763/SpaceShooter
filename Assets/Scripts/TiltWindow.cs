using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class TiltWindow : MonoBehaviour
{
    public Image image;
    private Color color;


    public void StartMenu()
    {
        StartCoroutine(FadeOut());
        image.gameObject.SetActive(true);
    }

    IEnumerator FadeOut()
    {
        color = image.color;
        color.a = 0;
        image.color = color;

        while (color.a < 1f)
        {
            color.a += 0.01f;
            image.color = color;
            yield return new WaitForSeconds(0.01f);
        }

        SceneManager.LoadScene("Game");

    }
}
