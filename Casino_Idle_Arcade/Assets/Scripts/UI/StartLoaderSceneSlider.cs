using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartLoaderSceneSlider : MonoBehaviour
{
    [SerializeField] GameObject startLoaderCanvas;
    [SerializeField] Slider progressSlider;
    [SerializeField] float waitToLoadScene;
    void Start()
    {
        progressSlider.value = 0;
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously()
    {

        //yield return new WaitForSeconds(1f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            progressSlider.value = progress;            
            yield return null;
        }

        yield return new WaitForSeconds(waitToLoadScene);
        startLoaderCanvas.SetActive(false);
    }

}
