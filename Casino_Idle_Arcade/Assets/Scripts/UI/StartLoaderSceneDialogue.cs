using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StartLoaderSceneDialogue : MonoBehaviour
{
    [SerializeField] float dotInTxtCd;
    [SerializeField] List<string> dialogues;
    [SerializeField] TextMeshProUGUI dialogueTxt;
    
    IEnumerator Start()
    {
        int dialogueIndex = Random.Range(0, dialogues.Count);
        string dialogue;

        while (true)
        {
            dialogue = dialogues[dialogueIndex];

            SetDialogueTxt(dialogue, "");

            yield return new WaitForSeconds(dotInTxtCd);
            SetDialogueTxt(dialogue, ".");

            yield return new WaitForSeconds(dotInTxtCd);
            SetDialogueTxt(dialogue, "..");

            yield return new WaitForSeconds(dotInTxtCd);
            SetDialogueTxt(dialogue, "...");

            yield return new WaitForSeconds(dotInTxtCd);

            dialogueIndex++;
            if (dialogueIndex >= dialogues.Count) dialogueIndex = 0;
        }

    }

    void SetDialogueTxt(string dialogue, string dot)
    {
        dialogueTxt.text = string.Concat(dialogue, dot);
        
    }
    

}
