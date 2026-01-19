using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    [Header("ˆÚ“®‘¬“x")]
    [SerializeField]
    private float _speed;

    void Update()
    {
        // WASD“ü—ÍŽæ“¾
        float x = Input.GetAxis("Horizontal"); // A,D
        float z = Input.GetAxis("Vertical");   // W,S

        // ˆÚ“®•ûŒü
        Vector3 move = new Vector3(x, 0, z);

        // ˆÚ“®
        transform.Translate(move * _speed * Time.deltaTime);
    }
}
