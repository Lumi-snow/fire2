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

    [Header("PlayerAnimation")]
    [SerializeField] PlayerAnimation _animation;

    //NPCと接触しているかどうかの判別に使う
    NPCTalkManager _targetNpc;

    public float Speed => _speed;
    public DialogueManager DialogueManager => _dialogueManager;

    public Rigidbody rb { get; private set; }

    private void Awake()
    {
        // Stateを初期化
        _context = new PlayerStateContext();
        _context.Init(this, PlayerState.Walk);
        //rigitBodyの取得
        rb = GetComponent<Rigidbody>();
    }

    private void Update() => _context.Update();

    public void ChangeState(PlayerState state)
    {
        _context.ChangeState(state);
    }
  
    //接触判定
    private void OnTriggerEnter(Collider other)
    {
        //NPCと接触している場合
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

    //NPCに対する方向判定(NPCの方向を向いているかどうか)
    public bool IsFacingNPC(NPCTalkManager npc)
    {
        if (npc == null) return false;

        Vector3 toNpc = npc.TalkPoint.position - transform.position;
        // Y無視（水平だけ見る）
        toNpc.y = 0;

        // 4方向なので正規化
        toNpc.Normalize();

        int dir = _animation.GetDirection();

        switch (dir)
        {
            case 1: // 上（W）
                return toNpc.z > 0.7f;

            case 2: // 左（A）
                return toNpc.x < -0.7f;

            case 3: // 下（S）
                return toNpc.z < -0.7f;

            case 4: // 右（D）
                return toNpc.x > 0.7f;
        }
        return false;
    }

    public bool CanTalk()
    {
        return _targetNpc != null && IsFacingNPC(_targetNpc);
    }
    public NPCTalkManager GetTargetNpc()
    {
        return _targetNpc;
    }
    void FixedUpdate()
    {
        _context.FixedUpdate();
    }
}
