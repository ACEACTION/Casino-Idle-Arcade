using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCanvas : MonoBehaviour
{

    public static GameplayCanvas instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }


}
