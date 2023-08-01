using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    //public bool startGame;

    [SerializeField] GameObject defaultCam;
    [SerializeField] GameObject startGameCam;
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject startGameCanvas;
    [SerializeField] GameObject tutorial;
    [SerializeField] GameObject settingPanel;
    public static StartGame Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        //defaultCam.SetActive(false);
        mainCanvas.SetActive(false);
        tutorial.SetActive(false);
        PlayerMovements.Instance.canMove = false;
    }

    public void TapToStart()
    {
        //startGameCam.SetActive(false);
        //defaultCam.SetActive(true);
        startGameCanvas.SetActive(false);
        mainCanvas.SetActive(true);

        PlayerMovements.Instance.canMove = true;

        //if (!CasinoManager.instance.isCompleteTutorial)
        if (!GameManager.isCompleteTutorial)
            tutorial.SetActive(true);

        settingPanel.SetActive(true);

    }


    

}
