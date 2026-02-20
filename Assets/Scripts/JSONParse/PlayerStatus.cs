using System;
using UnityEngine;

[Serializable]
public class PlayerStatus : MonoBehaviour
{
    public int hp;
    public int mp;
    public Vector3 position;

    public PlayerStatus(int hp, int mp, Vector3 position)
    {
        this.hp = hp;
        this.mp = mp;
        this.position = position;
    }
}