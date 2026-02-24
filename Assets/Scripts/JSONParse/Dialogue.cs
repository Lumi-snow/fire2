using System;
using UnityEngine;

/// <summary>
/// JSONファイルから読み取るデータ構造を定義するクラス
/// </summary>
[Serializable]
public class Dialogue
{
    public string name;
    public string startNode;
    public string nodes;
    public string id;
    public string speaker;
    public string text;
    public string next;
}