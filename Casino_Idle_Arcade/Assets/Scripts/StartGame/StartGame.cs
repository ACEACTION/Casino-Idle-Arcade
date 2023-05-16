using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public bool startGame;

    [SerializeField] GameObject defaultCam;
    [SerializeField] GameObject startGameCam;
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject startGameCanvas;
    [SerializeField] GameObject tutorial;

    public static StartGame Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        defaultCam.SetActive(false);
        mainCanvas.SetActive(false);
        startGame = false;
    }

    public void TapToStart()
    {
        startGameCam.SetActive(false);
        defaultCam.SetActive(true);
        startGameCanvas.SetActive(false);
        mainCanvas.SetActive(true);

        startGame = true;

        if (!CasinoManager.instance.isCompleteTutorial)
            tutorial.SetActive(true);
    }




}
