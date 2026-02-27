using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalkManager : MonoBehaviour
{
    [SerializeField] private DialogueDataAsset _dialogueDataAsset;
    public DialogueDataAsset DialogueDataAsset => _dialogueDataAsset;
}
