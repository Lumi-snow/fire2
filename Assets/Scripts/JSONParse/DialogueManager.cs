using JetBrains.Annotations;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField, Tooltip("読み込むJSONファイルをここにアタッチ")]
    private TextAsset json;
    [Header("テキストウィンドウUI")]
    [SerializeField] private GameObject _textWindow;
    [Header("テキストウィンドウ用テキスト")]
    [SerializeField] private TextMeshProUGUI _windowText;

    private Dialogue dialogue;
    private Dictionary<string, DialogueNode> nodeDictionary;

    private DialogueNode currentNode;
    private bool isTalking = false;

    private void Start()
    {
        _textWindow.SetActive(false);

        dialogue = JsonUtility.FromJson<Dialogue>(json.text);

        //ノードを辞書に変換
        nodeDictionary = new Dictionary<string, DialogueNode>();
        foreach (DialogueNode node in dialogue.nodes)
        {
            nodeDictionary.Add(node.id, node);
        }
    }
    private void Update()
    {
        if (isTalking && Input.GetKeyDown(KeyCode.E))
        {
            GoNext();
        }
    }
    // 会話開始
    public void StartDialogue()
    {
        _textWindow.SetActive(true);
        isTalking = true;

        currentNode = nodeDictionary[dialogue.startID];
        ShowNode();
    }

    void ShowNode()
    {
        _windowText.text =  currentNode.text;
    }

    void GoNext()
    {
        if (string.IsNullOrEmpty(currentNode.next))
        {
            EndDialogue();
            return;
        }

        currentNode = nodeDictionary[currentNode.next];
        ShowNode();
    }

    void EndDialogue()
    {
        isTalking = false;
        _textWindow.SetActive(false);
    }
}