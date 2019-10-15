using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour
{
    public GameObject settingsPanel;
    AudioSource audioSource;
    Coroutine settingsCoroutine;


    private void Start()
    {
        settingsPanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Play");
    }

    public void OpenSettings()
    {
        // 설정창 열기
        settingsPanel.SetActive(true);
        if (settingsCoroutine != null) StopCoroutine(settingsCoroutine);
        settingsCoroutine = StartCoroutine(openAndCloseSettings(true, 1));
    }
    IEnumerator openAndCloseSettings(bool isOpen, float duration, Action callback = null)
    {
        CanvasGroup canvas = settingsPanel.GetComponent<CanvasGroup>();
        float currentTime = 0;
        while (currentTime < duration)
        {
            if (isOpen)
            {
                canvas.alpha = Mathf.Lerp(0, 1, currentTime / duration);
            }
            else
            {
                canvas.alpha = Mathf.Lerp(1, 0, currentTime / duration);
            }
            currentTime += Time.deltaTime;
            yield return null;
        }
        if (callback != null)
            callback.Invoke();
    }

    public void CloseSettings()
    {
        // 설정창 닫기

        if (settingsCoroutine != null) StopCoroutine(settingsCoroutine);
        settingsCoroutine = StartCoroutine(openAndCloseSettings(false, 1, () => { settingsPanel.SetActive(false); }));

    }
}
