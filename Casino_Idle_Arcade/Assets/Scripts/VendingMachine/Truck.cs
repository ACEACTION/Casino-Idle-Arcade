using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    public List<CasinoFood> snacks = new List<CasinoFood>();
    [SerializeField] List<Transform> snackPositions = new List<Transform>();

    [SerializeField] Transform loadingSpot;
    [SerializeField] Transform unloadSpot;
    [SerializeField] GameObject foodMaker;
    [SerializeField] CasinoFoodResourceDesk casinoResource;

    [SerializeField] float loadCd;
    [SerializeField] float loadCdAmount;
    [SerializeField] float unloadCd;
    [SerializeField] float unloadCdAmount;

    [SerializeField] int maxCapacitiy;

    public bool HasCapacity { get { return snacks.Count < maxCapacitiy; } }


    private void Update()
    {
            if (snacks.Count == 0) transform.DOMove(loadingSpot.position, 2f);
            if (!HasCapacity) transform.DOMove(unloadSpot.position, 2f);

            //we unload here
            if (casinoResource != null)
            {
                if (snacks.Count != 0 && casinoResource.HasCapacity)
                {
                    //unload process
                    unloadCd -= Time.deltaTime;
                    if (unloadCd <= 0)
                    {
                        unloadCd = unloadCdAmount;
                        UnLoadBar();
                    }
                }
            }

            //we load here
            if (foodMaker != null)
            {
                if (HasCapacity)
                {
                    loadCd -= Time.deltaTime;
                    if (loadCd <= 0)
                    {
                        LoadBar();
                    }
                }
            }
        }


        public void LoadBar()
        {

            loadCd = loadCdAmount;
            //time to load objects
            var snack = VendingMachinePool.Instance.casinoFoodPool.Get();
            snacks.Add(snack);
            snack.transform.SetParent(this.transform);

            snack.transform.position = snackPositions[snacks.Count - 1].position;


        }
        public void UnLoadBar()
        {
            var snack = snacks[snacks.Count - 1];
            snacks.Remove(snack);
            snack.transform.SetParent(casinoResource.transform);
            casinoResource.snacks.Add(snack);

            snack.transform.DOJump(casinoResource.snackPositions[casinoResource.snacks.Count - 1].transform.position, 1f, 1, 0.1f).OnComplete(() =>
            {
                snack.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f);

            });
           // casinoResource.listIndex++;


        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Casino Resource"))
            {
                casinoResource = other.gameObject.GetComponent<CasinoFoodResourceDesk>();
            }
            if (other.gameObject.CompareTag("FoodMaker"))
            {
                foodMaker = other.gameObject;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (casinoResource != null && other.gameObject.Equals(casinoResource.gameObject))
            {
                casinoResource = null;
            }

            if (foodMaker != null && other.gameObject.Equals(foodMaker.gameObject))
            {
                foodMaker = null;
            }
        }
    
}

