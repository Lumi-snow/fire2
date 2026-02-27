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
    public DialogueChoice[] choices;
}

[Serializable]
public class DialogueChoice
{
    public string text;   // 選択肢に表示する文章
    public string next;   // 遷移先ノードID
}