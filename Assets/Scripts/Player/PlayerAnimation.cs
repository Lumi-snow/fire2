using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Player _player;
    private Animator _animator;
    private int _direction = 0;
    private bool _isMoving = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null)
        Debug.LogError("Animator‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ");
    }

    void Update()
    {
        if (_player.CurrentState != PlayerState.Walk)
        {
            _animator.SetBool("IsMoving", false);
            return;
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                _direction = 1;
                _isMoving = true;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                _direction = 3;
                _isMoving = true;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                _direction = 2;
                _isMoving = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _direction = 4;
                _isMoving = true;
            }
            else
                _isMoving = false;

            _animator.SetBool("IsMoving", _isMoving);
            _animator.SetInteger("Direction", _direction);
        }
    }
    public int GetDirection()
    {
        return _direction;
    }
}
