using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    [Header("移動速度")]
    [SerializeField]
    private float _speed;

    void Update()
    {
        // WASD入力取得
        float x = Input.GetAxis("Horizontal"); // A,D
        float z = Input.GetAxis("Vertical");   // W,S

        //入力そのままの「移動ベクトル」((1,1)なら長さはroot2)
        Vector3 velocity = new Vector3(x, 0, z);
        //長さを1に揃えた「方向ベクトル」(normalizedで長さを1にする)
        Vector3 direction = velocity.normalized;

        //移動距離
        float distance = _speed * Time.deltaTime;
        //移動先を計算(direction * 移動速度 * Time.deltaTime)
        Vector3 destination = transform.position + direction * distance;

        //移動先の座標を設定
        transform.position = destination;
    }
}
