using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweeper : MonoBehaviour
{
    public Transform[] objectsToSweep;
    public Transform[] objectsStartingPos;
    [SerializeField] GameObject sortedCards;
    Vector3[] path;
    [SerializeField] Transform targetPosition;
    Quaternion targetRotation;
    [SerializeField] ParticleSystem cardEffect;
    private void Start()
    {
                path = new Vector3[] {
                targetPosition.position,
                targetPosition.position,
                targetPosition.position,
                };
    }
    public void Sweep()
    {
        sortedCards.SetActive(false);

        Sequence sequence = DOTween.Sequence();

        // Move each object to the sweeper location
        foreach (Transform objectToSweep in objectsToSweep)
        {
            objectToSweep.gameObject.SetActive(true);

            targetRotation = Quaternion.Euler(0f, 0f, 0f); // Set the sweeper rotation


            sequence.Join(objectToSweep.DOPath(path, 1.5f, PathType.CubicBezier,PathMode.Full3D ,10,null).SetEase(Ease.InOutSine));
            sequence.Join(objectToSweep.DORotate(targetRotation.eulerAngles, 1.5f).SetEase(Ease.InOutSine));
        }

        // deActive the objects after the sweep animation is complete
        sequence.OnComplete(() =>
        {
            sortedCards.SetActive(true);
            cardEffect.gameObject.SetActive(true);
            cardEffect.Play();
            foreach (Transform objectToSweep in objectsToSweep)
            {
                objectToSweep.gameObject.SetActive(false);

            }
        });
    }

    public void ResetingCardsPoisiton()
    {
        for (int i = 0; i < objectsToSweep.Length; i++)
        {
            objectsToSweep[i].gameObject.SetActive(true);

            objectsToSweep[i].DOMove(objectsStartingPos[i].position,1f);
        }


    }
}
