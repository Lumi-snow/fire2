using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWalk : IPlayerState
{
    Player _player;
    public PlayerState State => PlayerState.Walk;
    public PlayerWalk(Player plaeyr) => _player = plaeyr;
    private Vector3 inputAxis;
    public void Entry() { /*...*/ }

    public void Update()
    {
        // NPCに触れていてEnter押したらTalkへ
        if (_player.CanTalk() && Input.GetKeyDown(KeyCode.Return))
        {
            _player.ChangeState(PlayerState.Talk);
            return;
        }

        // WASD入力取得
        inputAxis.x = Input.GetAxis("Horizontal"); // A,D
        inputAxis.z = Input.GetAxis("Vertical");   // W,S
        inputAxis.y = 0;

        /*
        //入力そのままの「移動ベクトル」((1,1)なら長さはroot2)
        //Vector3 velocity = new Vector3(x, 0, z).normalized;
        //長さを1に揃えた「方向ベクトル」(normalizedで長さを1にする)
        Vector3 direction = inputAxis.normalized;

        //移動距離
        float distance = _player.Speed * Time.deltaTime;

        //移動先を計算(direction * 移動速度 * Time.deltaTime)
        Vector3 destination = _player.transform.position + direction * distance;

        //移動先の座標を設定
        _player.transform.position = destination;
    */
    }
    public void FixedUpdate()
    {
        Vector3 dir = inputAxis.normalized;

        if (dir.magnitude > 0)
        {
            _player.rb.velocity = dir * _player.Speed;
        }
        else
        {
            _player.rb.velocity = Vector3.zero;
        }
    }
    public void Exit() { /*...*/ }
}
