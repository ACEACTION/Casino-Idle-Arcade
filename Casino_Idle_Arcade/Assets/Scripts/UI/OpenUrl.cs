using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUrl : MonoBehaviour
{
    [SerializeField] string url;

    public void OpenUrlAction() => Application.OpenURL(url);

}
