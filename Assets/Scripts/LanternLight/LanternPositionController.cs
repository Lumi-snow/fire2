using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternPositionController : MonoBehaviour
{
    private Animator _animator;
    private int _direction = 0;

    [Header("各方向でのランタンのローカル位置")]
    [SerializeField] private Vector3 _back;
    [SerializeField] private Vector3 _left;
    [SerializeField] private Vector3 _front;
    [SerializeField] private Vector3 _right;
    
    [Header("プレイヤーオブジェクト")]
    [SerializeField] private Transform _player;
    [SerializeField] private Animator _targetAnimator;

    void Start()
    {
        if (_player == null)
        {
            Debug.LogError("Playerが設定されていません");
            return;
        }

        if (_targetAnimator == null)
        {
            Debug.LogError("Animator が Inspector で設定されていません");
            return;
        }

        _animator = _targetAnimator;
    }

    void Update()
    {
        _direction = _animator.GetInteger("Direction");

        switch (_direction)
        {
            case 1:
                transform.localPosition = _back;
                break;
            case 2:
                transform.localPosition = _left;
                break;
            case 3:
                transform.localPosition = _front;
                break;
            case 4:
                transform.localPosition = _right;
                break;
        }
    }
}

