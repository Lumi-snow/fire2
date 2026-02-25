using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DialogueManager : MonoBehaviour
{
    [SerializeField, Tooltip("読み込むJSONファイルをここにアタッチ")]
    private TextAsset json;
    [Header("テキストウィンドウUI")]
    [SerializeField] private GameObject _textWindow;
    [Header("テキストウィンドウ用テキスト")]
    [SerializeField] private TextMeshProUGUI _windowText;

    private Dialogue _dialogue;
    private Dictionary<string, DialogueNode> _nodeDictionary;
    private DialogueNode _currentNode;
    private bool _isTalking;

    private void Start()
    {
        _textWindow.SetActive(false);

        _dialogue = JsonUtility.FromJson<Dialogue>(json.text);

        //ノードを辞書に変換
        _nodeDictionary = new Dictionary<string, DialogueNode>();
        foreach (DialogueNode node in _dialogue.nodes)
        {
            _nodeDictionary.Add(node.id, node);
        }
    }
    private void Update()
    {
        if (!_isTalking) return;

            if (_textWindow.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            GoNext();
        }
    }
    // 会話開始
    public void StartDialogue()
    {
        _isTalking = true;

        _textWindow.SetActive(true);
        _currentNode = _nodeDictionary[_dialogue.startID];
        ShowNode();
    }

    void ShowNode()
    {
        _windowText.text =  _currentNode.text;
    }

    void GoNext()
    {
        if (string.IsNullOrEmpty(_currentNode.next))
        {
            EndDialogue();
            return;
        }
        else
        {
            _currentNode = _nodeDictionary[_currentNode.next];
            ShowNode();
        }
    }

    void EndDialogue()
    {
        _isTalking = false;

        _textWindow.SetActive(false);
        _currentNode = null;
    }

    public DialogueNode CurrentNode
    {
        get { return _currentNode; }
    }
}