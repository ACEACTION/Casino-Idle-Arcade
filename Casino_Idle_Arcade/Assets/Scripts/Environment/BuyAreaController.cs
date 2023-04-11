using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BuyAreaController : MonoBehaviour
{
    public float price;
    bool isPlayerAvailabe;
    [SerializeField] float cooldown;
    [SerializeField] float cooldownAmount;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] ParticleSystem buildEffect;

    private void Start()
    {
        priceText.text = price.ToString();
        GameManager.totalMoney += 2000;
    }
    private void Update()
    {
        if (price > 0 && isPlayerAvailabe && GameManager.totalMoney >0 ) 
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {

                Money money = StackMoneyPool.Instance.pool.Get();
                money.transform.position = PlayerMovements.Instance.transform.position;
                money.transform.eulerAngles = new Vector3(Random.Range(0,359), 0, Random.Range(0, 359));
                money.transform.DORotate(Vector3.zero,0.5f);
                money.transform.DOJump(transform.position + new Vector3(Random.Range(-0.2f ,0.2f), 0,Random.Range(-0.2f ,0.2f)), 2f, 1, .5f).OnComplete(() =>
                {
                   StackMoneyPool.Instance.OnReleaseMoney(money);
                });
                price -= (GameManager.totalMoney * 2) / 100;
                priceText.text = price.ToString();
                priceText.transform.DOShakeScale(0.5f, 0.1f).OnComplete(() =>
                { priceText.transform.DOScale(1f, 0f);  });
                GameManager.totalMoney -= (GameManager.totalMoney * 2) / 100;
                cooldown = cooldownAmount;

            }


        }
        if(price <= 0)
        {
            transform.parent.transform.GetChild(0).gameObject.SetActive(true);
            buildEffect.gameObject.SetActive(true);
            buildEffect.Play();
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.DOScale(1.2f, 0.5f);
            isPlayerAvailabe = true;

        }
    }




    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.DOScale(1f, 0.5f);

            isPlayerAvailabe = false;
        }
    }
}
