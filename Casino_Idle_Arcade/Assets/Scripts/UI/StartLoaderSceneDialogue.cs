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
        string dialogue = string.Concat(dialogues[Random.Range(0, dialogues.Count)]);

        while (true)
        {
            SetDialogueTxt(string.Concat(dialogue));

            yield return new WaitForSeconds(dotInTxtCd);
            SetDialogueTxt(string.Concat(dialogue, "."));

            yield return new WaitForSeconds(dotInTxtCd);
            SetDialogueTxt(string.Concat(dialogue, ".."));

            yield return new WaitForSeconds(dotInTxtCd);
            SetDialogueTxt(string.Concat(dialogue, "..."));

            yield return new WaitForSeconds(dotInTxtCd);
        }

    }

    void SetDialogueTxt(string txt)
    {
        dialogueTxt.text = txt;
    }
    

}
