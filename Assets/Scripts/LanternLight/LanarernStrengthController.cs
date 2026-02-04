using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanarernStrengthController : MonoBehaviour
{
    [SerializeField] private LanternNoiseController _LanternNoiseController;

    private int _strengthLevel;

    [Header("ランタンの Light")]
    [SerializeField] private Light _lanternLight;
    [Header("ランタンの強さ上限")]
    [SerializeField] private int _strengthNumMax;
    [Header("ランタンの各段階の設定")]
    [SerializeField] private LanternStrengthData[] _strengthLevels;

    [System.Serializable]
    public class LanternStrengthData
    {
        public float intensity;
        public float indirectMultiplier;
        public float range;
    }

    void Start()
    {
        if (_lanternLight == null)
        {
            Debug.LogError("Light が設定されていません");
            return;
        }
        _strengthLevel = _strengthNumMax;
        ApplyStrength();
    }

    void Update()
    {
        //ランタンの灯りの強さ変更(一旦OLキーで変更できるようにしている)
        if (Input.GetKeyDown(KeyCode.O) && _strengthLevel < _strengthNumMax)
        {
            _strengthLevel++;
            ApplyStrength();
        }
        if (Input.GetKeyDown(KeyCode.L) && _strengthLevel > 0)
        {
            _strengthLevel--;
            ApplyStrength();
        }
    }

    //各段階のlight設定を反映させる
    void ApplyStrength()
    {
        LanternStrengthData data = _strengthLevels[_strengthLevel];

        _lanternLight.intensity = data.intensity;
        _lanternLight.range = data.range;
        _lanternLight.bounceIntensity = data.indirectMultiplier;

        //LanternNoiseControllerのIntensityの基準値を変更させる
        if (_LanternNoiseController != null)
        {
            _LanternNoiseController.SetBaseIntensity(data.intensity);
        }
    }
}
