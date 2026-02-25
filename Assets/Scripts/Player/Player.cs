using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//あらかじめ Playerが操作するプレイアブルなスクリプト
public class Player : MonoBehaviour
{
    // StateパターンのContext
    PlayerStateContext _context;
    public PlayerState CurrentState => _context.CurrentState;

    [Header("移動速度")]
    [SerializeField] float _speed = 5f;

    [Header("DialogueManager")]
    [SerializeField] DialogueManager _dialogueManager;

    GameObject _targetNpc;

    public float Speed => _speed;
    public DialogueManager DialogueManager => _dialogueManager;

    private void Awake()
    {
        // Stateを初期化
        _context = new PlayerStateContext();
        _context.Init(this, PlayerState.Walk);
    }

    private void Update() => _context.Update();

    public void ChangeState(PlayerState state)
    {
        _context.ChangeState(state);
    }
  
    //接触判定
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            _targetNpc = other.gameObject;
            Debug.Log("接触しています");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            _targetNpc = null;
        }
    }

    public bool HasNpcTarget()
    {
        return _targetNpc != null;
    }

    void OnDialogueEnd()
    {
        ChangeState(PlayerState.Walk);
    }
}
