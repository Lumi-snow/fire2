using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalkManager : MonoBehaviour
{
    [SerializeField] private DialogueDataAsset[] _dialogues;

    private int _dialogueCount = 0;

    public DialogueDataAsset GetDialogue()
    {
        if (_dialogues.Length == 0) return null;

        int index = Mathf.Clamp(_dialogueCount, 0, _dialogues.Length - 1);

        return _dialogues[index];
    }

    public void OnDialogueFinished()
    {
        _dialogueCount++;
    }
}