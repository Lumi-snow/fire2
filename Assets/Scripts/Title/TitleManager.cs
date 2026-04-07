using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    Coroutine _blinkCoroutine;

    [Header("スタートテキスト")]
    [SerializeField]private TextMeshProUGUI _StartText;
    [Header("暗転用Image")]
    [SerializeField] private GameObject _bloackImage;

    void Start()
    {
        AudioManager.Instance.PlayLoop("FireNoise");
        _bloackImage.SetActive(false);
        _StartText.gameObject.SetActive(false);

        /*
        //文字の点滅
        _StartText.gameObject.SetActive(true);
        _blinkCoroutine = StartCoroutine(BlinkIcon());
        StartBlink();
    */
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            AudioManager.Instance.StopLoop();
            AudioManager.Instance.Play("FireGoesOut");

            _bloackImage.SetActive(true);
            //StopBlink();
            Invoke("LoadMainScene", 3.0f);
        }
    }

    void LoadMainScene()
    {
        SceneManager.LoadScene("PlayerTest");
    }

    /*
    //文字の点滅
    IEnumerator BlinkIcon()
    {
        float t = 0f;

        while (true)
        {
            t += Time.deltaTime;

            float alpha = (Mathf.Sin(t * 2f) + 1f) * 0.35f;

            var color = _StartText.color;
            color.a = alpha;
            _StartText.color = color;

            yield return null;
        }
    }
    //点滅開始処理
    void StartBlink()
    {
        if (_blinkCoroutine != null)
            StopCoroutine(_blinkCoroutine);

        _StartText.gameObject.SetActive(true);
        _blinkCoroutine = StartCoroutine(BlinkIcon());
    }

    //点滅停止処理
    void StopBlink()
    {
        if (_blinkCoroutine != null)
            StopCoroutine(_blinkCoroutine);

        _StartText.gameObject.SetActive(false);
    }
    */
}
