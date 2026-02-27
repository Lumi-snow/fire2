using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DialogueManager : MonoBehaviour
{
    [Header("テキストウィンドウUI")]
    [SerializeField] private GameObject _textWindow;
    [Header("テキストウィンドウ用テキスト")]
    [SerializeField] private TextMeshProUGUI _windowText;
    [Header("選択肢用")]
    [SerializeField] private Transform _choiceParent;
    [SerializeField] private GameObject _choiceButtonPrefab;
    /*
    [Header("選択肢1用のボタン")]
    [SerializeField] private GameObject _choice1Button;
    [Header("選択肢1用テキスト")]
    [SerializeField] private TextMeshProUGUI _choice1Text;
    [Header("選択肢2用のボタン")]
    [SerializeField] private GameObject _choice2Button;
    [Header("選択肢2用テキスト")]
    [SerializeField] private TextMeshProUGUI _choice2Text;
    */

    private Dialogue _dialogue;
    private Dictionary<string, DialogueNode> _nodeDictionary;
    private DialogueNode _currentNode;
    private bool _isTalking;//会話開始時の入力が重複しないようにするため

    private void Start()
    {
        _textWindow.SetActive(false);
    }
    private void Update()
    {
        if (!_isTalking) return;

            if (_textWindow.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            GoNext();
        }
    }

    //接触NPCからjsonを呼び出す
    public void LoadDialogue(DialogueDataAsset data)
    {
        if (data == null || data.jsonFile == null)
        {
            Debug.LogError("DialogueData is null!");
            return;
        }

        _dialogue = JsonUtility.FromJson<Dialogue>(data.jsonFile.text);

        _nodeDictionary = new Dictionary<string, DialogueNode>();
        //ノードを辞書に変換
        foreach (DialogueNode node in _dialogue.nodes)
        {
            _nodeDictionary.Add(node.id, node);
        }
    }

    // 会話開始
    public void StartDialogue()
    {
        if (_dialogue == null)
        {
            Debug.LogError("Dialogue not loaded!");
            return;
        }
        _isTalking = true;

        _textWindow.SetActive(true);
        _currentNode = _nodeDictionary[_dialogue.startID];
        ShowNode();
    }

    void ShowNode()
    {
        _windowText.text =  _currentNode.text;

        if (_currentNode.choices != null && _currentNode.choices.Length > 0)
        {
            ShowChoices();
        }
    }
    void ShowChoices()
    {
        ClearChoices();

        foreach (var choice in _currentNode.choices)
        {
            var buttonObj = Instantiate(_choiceButtonPrefab, _choiceParent);
            var text = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            text.text = choice.text;

            buttonObj.GetComponent<UnityEngine.UI.Button>()
                .onClick.AddListener(() => OnChoiceSelected(choice.next));
        }
    }
    void OnChoiceSelected(string nextID)
    {
        ClearChoices();

        _currentNode = _nodeDictionary[nextID];
        ShowNode();
    }
    void ClearChoices()
    {
        foreach (Transform child in _choiceParent)
        {
            Destroy(child.gameObject);
        }
    }
    void GoNext()
    {
        // 選択肢があるなら無視
        if (_currentNode.choices != null && _currentNode.choices.Length > 0)
            return;

        if (string.IsNullOrEmpty(_currentNode.next))
        {
            EndDialogue();
            return;
        }

        _currentNode = _nodeDictionary[_currentNode.next];
        ShowNode();
    }

    void EndDialogue()
    {
        _isTalking = false;

        ClearChoices();

        _textWindow.SetActive(false);
        _currentNode = null;
    }

    public DialogueNode CurrentNode
    {
        get { return _currentNode; }
    }
}