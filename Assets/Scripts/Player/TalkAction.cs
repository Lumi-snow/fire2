using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TalkAction : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;

    private GameObject _targetNpc;
    private GameObject _targetItem;

    [Header("選択肢UI")]
    [SerializeField] private GameObject _option1;
    [SerializeField] private GameObject _option2;

    private void Start()
    {
        _option1.SetActive(false);
        _option2.SetActive(false);
    }

    void Update()
    {
        // 接触中にキー入力したら会話を試みる
        if (_targetNpc != null && Input.GetKeyDown(KeyCode.Return))
        {
            if (_targetNpc.CompareTag("NPC"))
            {
                Debug.Log("NPCに接触して対話を試みた");
                dialogueManager.StartDialogue();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter: " + other.name);

        if (other.CompareTag("NPC"))
        {
            _targetNpc = other.gameObject;
            Debug.Log(other.gameObject + "と接触中");
        }
    }
}