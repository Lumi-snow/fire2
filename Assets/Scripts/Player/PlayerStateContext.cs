using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateContext
{
    IPlayerState _currentState;    // 現在の状態
    IPlayerState _previousState;   // 直前の状態
    public PlayerState CurrentState => _currentState.State;
    // 状態のテーブル
    Dictionary<PlayerState, IPlayerState> _stateTable;

    public void Init(Player player, PlayerState initState)
    {
        if (_stateTable != null) return; // 何度も初期化しない

        // 各状態クラスの初期化
        Dictionary<PlayerState, IPlayerState> table = new()
        {
            { PlayerState.Idle, new PlayerIdle(player) },
            { PlayerState.Walk, new PlayerWalk(player) },
            { PlayerState.Talk, new PlayerTalk(player) },
        };
        _stateTable = table;
        ChangeState(initState);
    }

    // 別の状態に変更する
    public void ChangeState(PlayerState next)
    {
        if (_stateTable == null) return; // 未初期化の時は無視
        if (_currentState != null && _currentState.State == next)
        {
            return; // 同じ状態には遷移しない
        }
        // 退場 → 現在状態変更 → 入場
        var nextState = _stateTable[next];
        _previousState = _currentState;
        _previousState?.Exit();
        _currentState = nextState;
        _currentState.Entry();
    }

    // 現在の状態をUpdateする
    public void Update() => _currentState?.Update();
}
