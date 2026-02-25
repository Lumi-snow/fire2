using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTalk : IPlayerState
{
    Player _player;
    public PlayerState State => PlayerState.Talk;
    public PlayerTalk(Player plaeyr) => _player = plaeyr;
    
    public void Entry() 
    {
        _player.DialogueManager.StartDialogue();
    }
    public void Update() 
    {
        if (_player.DialogueManager.CurrentNode == null)
            _player.ChangeState(PlayerState.Walk);
    }
    public void Exit() 
    {

    }
}
