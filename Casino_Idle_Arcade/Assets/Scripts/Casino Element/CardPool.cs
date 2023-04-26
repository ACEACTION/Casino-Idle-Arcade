using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using DG.Tweening;

public class CardPool : MonoBehaviour
{
    [SerializeField] Card cardPrefab;
    public ObjectPool<Card> pool;


    public static CardPool Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    void Start()
    {
        pool = new ObjectPool<Card>(
            CreateCard, OnGet, OnRelease, OnDestoryCard, false, 100, 100000);
    }

    private void OnDestoryCard(Card obj)
    {
        Destroy(obj);
    }

    private void OnRelease(Card obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnGet(Card obj)
    {
        obj.gameObject.SetActive(true);
    }

    private Card CreateCard()
    {
        Card card =
            Instantiate(cardPrefab, transform.position, Quaternion.identity);
        return card;
    }

    public void OnReleaseCard(Card card)
    {
        card.transform.DOKill();
        pool.Release(card);
    }
}
