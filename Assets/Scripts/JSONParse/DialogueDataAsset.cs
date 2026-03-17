using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ DialogueDateAsset")]
public class DialogueDataAsset : ScriptableObject
{
    [Header("会話JSON")]
    public TextAsset jsonFile;

    [Header("会話ID（管理用）")]
    public string dialogueID;
}