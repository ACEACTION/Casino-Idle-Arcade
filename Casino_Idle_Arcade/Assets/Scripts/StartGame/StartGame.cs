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
    [SerializeField] GameObject chipDesk;
    [SerializeField] GameObject ambienceAudioScr;

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
        startGameCanvas.SetActive(false);

        PlayerMovements.Instance.canMove = false;

        if (!GameManager.isCompleteTutorial)
        { 
            chipDesk.SetActive(false); 
            ambienceAudioScr.SetActive(false);
        }

        StartCoroutine(ActiveStartGameCanvas());

    }

    IEnumerator ActiveStartGameCanvas() 
    {
        yield return new WaitForSeconds(1f);
        startGameCanvas.SetActive(true);
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
