using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ChipPool : MonoBehaviour
{
    [SerializeField] Chip redChipPrefab;
    [SerializeField] Chip greenChipPrefab;
    [SerializeField] Chip blueChipPrefab;

    public ObjectPool<Chip> redChipPool;
    public ObjectPool<Chip> greenChipPool;
    public ObjectPool<Chip> blueChipPool;

    public static ChipPool Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    void Start()
    {
        redChipPool = new ObjectPool<Chip>(
            CreateRedChip, OnGet, OnRelease, OnDestoryChip, false, 100, 100000);
        greenChipPool = new ObjectPool<Chip>(
            CreateGreenChip, OnGet, OnRelease, OnDestoryChip, false, 100, 100000);
        blueChipPool = new ObjectPool<Chip>(
            CreateBlueChip, OnGet, OnRelease, OnDestoryChip, false, 100, 100000);
    }

    private void OnDestoryChip(Chip obj)
    {
        Destroy(obj);
    }

    private void OnRelease(Chip obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnGet(Chip obj)
    {
        obj.gameObject.SetActive(true);
    }

    private Chip CreateRedChip()
    {
        Chip chip =
            Instantiate(redChipPrefab, transform.position, Quaternion.identity);
        return chip;
    }

    private Chip CreateGreenChip()
    {
        Chip chip =
            Instantiate(greenChipPrefab, transform.position, Quaternion.identity);
        return chip;
    }

    private Chip CreateBlueChip()
    {
        Chip chip =
            Instantiate(blueChipPrefab, transform.position, Quaternion.identity);
        return chip;
    }

    public void OnReleaseChip(Chip chip, ChipType chipType)
    {
        switch (chipType)
        {
            case ChipType.red:
                redChipPool.Release(chip);
                break;
            case ChipType.green:
                greenChipPool.Release(chip);
                break;
            case ChipType.blue:
                blueChipPool.Release(chip);
                break;
        }
    }
}
