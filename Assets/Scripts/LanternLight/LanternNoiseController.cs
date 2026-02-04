using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternNoiseController : MonoBehaviour
{
    [Header("Intensityの設定")]
    [SerializeField] private float _intensityRange;
    [SerializeField] private float _intensitySpeed;

    [Header("Temperatureの設定")]
    [SerializeField] private float _temperatureMin;
    [SerializeField] private float _temperatureMax;
    [SerializeField] private float _temperatureSpeed;

    [Header("灯りの強さ変化速度")]
    [SerializeField] private float _changeSpeed;

    private Light _light;

    private float _baseIntensity;
    private float _targetBaseIntensity;

    private float _intensitySeed;
    private float _temperatureSeed;

    void Start()
    {
        _light = GetComponent<Light>();
        _light.useColorTemperature = true;

        //現在のIntensityを取得
        _baseIntensity = _light.intensity;

        //各ライトごとに揺らぎを変えるためのシード
        _intensitySeed = Random.value * 100f;
        _temperatureSeed = Random.value * 100f;
    }

    void Update()
    {
        _baseIntensity = Mathf.MoveTowards(
        _baseIntensity,
        _targetBaseIntensity,
        _changeSpeed * Time.deltaTime
        );

        float intensityNoise = Mathf.PerlinNoise(Time.time * _intensitySpeed, _intensitySeed);

        float centeredNoise = (intensityNoise - 0.5f) * 2f;

        _light.intensity = _baseIntensity + centeredNoise * _intensityRange;

        float temperatureNoise = Mathf.PerlinNoise(Time.time * _temperatureSpeed, _temperatureSeed);
        _light.colorTemperature = Mathf.Lerp(_temperatureMin, _temperatureMax, temperatureNoise);
    }

    //LanternStrengthControllerから_baseIntendityを変更させる。
    public void SetBaseIntensity(float newBaseIntensity)
    {
        _targetBaseIntensity = newBaseIntensity;
    }
}