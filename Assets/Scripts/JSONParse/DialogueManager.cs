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
    private List<GameObject> _choiceObjects = new List<GameObject>();
    private int _currentChoiceIndex = 0;

    private void Start()
    {
        _textWindow.SetActive(false);
    }
    private void Update()
    {
        if (!_isTalking) return;

        // 選択肢がある場合
        if (_currentNode.choices != null && _currentNode.choices.Length > 0)
        {
            HandleChoiceInput();
        }
        else
        {
            if (_textWindow.activeSelf && Input.GetKeyDown(KeyCode.Return))
            {
                GoNext();
            }
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

    //ノードの表示
    void ShowNode()
    {
        _windowText.text =  _currentNode.text;

        //選択肢のあるノードの場合は選択肢を表示する
        if (_currentNode.choices != null && _currentNode.choices.Length > 0)
        {
            ShowChoices();
        }
    }
    //選択肢を表示する
    void ShowChoices()
    {
        ClearChoices();
        _choiceObjects.Clear();
        _currentChoiceIndex = 0;

        foreach (var choice in _currentNode.choices)
        {
            var buttonObj = Instantiate(_choiceButtonPrefab, _choiceParent);
            var text = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            text.text = choice.text;

            int index = _choiceObjects.Count;

            buttonObj.GetComponent<UnityEngine.UI.Button>()
                    .onClick.AddListener(() => OnChoiceSelected(choice.next));

            _choiceObjects.Add(buttonObj);
        }
        UpdateChoiceVisual();
    }

    //入力処理
    void HandleChoiceInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _currentChoiceIndex--;
            if (_currentChoiceIndex < 0)
                _currentChoiceIndex = _choiceObjects.Count - 1;

            UpdateChoiceVisual();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _currentChoiceIndex++;
            if (_currentChoiceIndex >= _choiceObjects.Count)
                _currentChoiceIndex = 0;

            UpdateChoiceVisual();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            var choice = _currentNode.choices[_currentChoiceIndex];
            OnChoiceSelected(choice.next);
        }
    }

    //選択肢の見た目の処理
    void UpdateChoiceVisual()
    {
        for (int i = 0; i < _choiceObjects.Count; i++)
        {
            var text = _choiceObjects[i].GetComponentInChildren<TextMeshProUGUI>();
            var image = _choiceObjects[i].GetComponentInChildren<UnityEngine.UI.Image>();
            if (i == _currentChoiceIndex)
            {
                text.color = Color.yellow; // 選択中
                image.color = new Color32(50, 50, 50, 255);
            }
            else
            {
                text.color = Color.white;
                image.color = new Color32(50, 50, 50, 120);
            }
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
        _choiceObjects.Clear();
    }
    //次のノードに移動する処理
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

        var npcObj = FindObjectOfType<Player>().GetTargetNpc();
        if (npcObj != null)
        {
            var npc = npcObj.GetComponent<NPCTalkManager>();
            if (npc != null)
                npc.OnDialogueFinished();
        }
    }

    public DialogueNode CurrentNode
    {
        get { return _currentNode; }
    }
}