using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StartLoaderSceneDialogue : MonoBehaviour
{
    [SerializeField] List<string> dialogues;
    [SerializeField] TextMeshProUGUI dialogueTxt;
    private void Start()
    {        
        dialogueTxt.text = dialogues[Random.Range(0, dialogues.Count)];
    }
}
