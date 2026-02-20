using System;
using UnityEngine;

/// <summary>
/// JSONファイルから読み取るデータ構造を定義するクラス
/// </summary>
[Serializable]
public class Dialogue
{
    public string title;
    public string[] lines;
}