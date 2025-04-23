using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization.Components;
public class StartLoaderSceneDialogue : MonoBehaviour
{
    [SerializeField] float dotInTxtCd;
    [SerializeField] List<string> dialogues;
    [SerializeField] List<string> tipKeyList;
    [SerializeField] TextMeshProUGUI dialogueTxt;
    [SerializeField] LocalizeStringEvent localizeStringEvent;

    public void CallDialogue() => StartCoroutine(StartDialogue());

    IEnumerator StartDialogue()
    {
        int dialogueIndex = Random.Range(0, dialogues.Count);
        string tipKey;

        while (true)
        {
            tipKey = tipKeyList[dialogueIndex];

            SetDialogueTxt(tipKey, "");

            yield return new WaitForSeconds(dotInTxtCd);
            SetDialogueTxt(tipKey, ".");

            yield return new WaitForSeconds(dotInTxtCd);
            SetDialogueTxt(tipKey, "..");

            yield return new WaitForSeconds(dotInTxtCd);
            SetDialogueTxt(tipKey, "...");

            yield return new WaitForSeconds(dotInTxtCd);

            dialogueIndex++;
            if (dialogueIndex >= dialogues.Count) dialogueIndex = 0;
        }

    }

    void SetDialogueTxt(string tip_key, string dot)
    {
        localizeStringEvent.StringReference.TableEntryReference = tip_key;
        dialogueTxt.text = string.Concat(localizeStringEvent.StringReference.GetLocalizedString(), dot);
    }
    

}
