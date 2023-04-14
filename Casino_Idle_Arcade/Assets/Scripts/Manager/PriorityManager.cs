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

        if (elements.Count != 0)
        {
            elements[0].gameObject.SetActive(true);
            elements.RemoveAt(0);
        }
    }



}
