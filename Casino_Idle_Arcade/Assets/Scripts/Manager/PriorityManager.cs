using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityManager : MonoBehaviour
{
    public List<GameObject> elements;
    public List<int> priorityIndex;

    public static PriorityManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }


    public void OpenNextPriority()
    {
        if (!GameManager.isCompleteTutorial) return;

        elements.RemoveAt(0);
        if (elements.Count != 0)
            elements[0].gameObject.SetActive(true); 
    }



}
