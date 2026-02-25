using System;
using UnityEngine;

/// <summary>
/// JSONファイルから読み取るデータ構造を定義するクラス
/// </summary>
[Serializable]
public class Dialogue
{
    public string name;
    public string startID;
    public DialogueNode[] nodes;
}

[Serializable]
public class DialogueNode
{
    public string id;
    public string speaker;
    public string text;
    public string next;
}