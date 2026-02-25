using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : IPlayerState
{
    Player _player;
    public PlayerState State => PlayerState.Idle;
    public PlayerIdle(Player plaeyr) => _player = plaeyr;
    public void Entry() { /*...*/ }
    public void Update()
    {    }
    public void Exit() { /*...*/ }
}