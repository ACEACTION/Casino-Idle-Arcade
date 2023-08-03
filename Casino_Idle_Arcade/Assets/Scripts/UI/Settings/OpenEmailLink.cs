using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenEmailLink : MonoBehaviour
{
    [SerializeField] string email;
    [SerializeField] Button btn;

    void Start()
    {
        btn.onClick.AddListener(OpenEmail);
    }

    void OpenEmail()
    {
        Application.OpenURL("mailto:" + email);
    }
    
}
