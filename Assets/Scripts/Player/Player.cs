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

    //NPCと接触しているかどうかの判別に使う
    //private GameObject _targetNpc;
    NPCTalkManager _targetNpc;

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
        if (other.TryGetComponent<NPCTalkManager>(out var npc))
        {
            _targetNpc = npc;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<NPCTalkManager>(out var npc))
        {
            if (_targetNpc == npc)
                _targetNpc = null;
        }
    }

    public bool HasNpcTarget()
    {
        return _targetNpc != null;
    }
    public NPCTalkManager GetTargetNpc()
    {
        return _targetNpc;
    }
}
