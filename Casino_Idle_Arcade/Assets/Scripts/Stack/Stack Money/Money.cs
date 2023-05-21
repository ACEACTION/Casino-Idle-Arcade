using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Money : CasinoResource
{
    public int moneyAmount;
    public MoneyData moneyData;
    bool goToPlayer;
    Vector3 defaultScale;

    [SerializeField] ParticleSystem effect;

    private void Awake()
    {
       defaultScale = transform.localScale;               
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
        transform.localScale = defaultScale;        
    }

    public override void ReleaseResource()
    {
        base.ReleaseResource();
        effect.gameObject.SetActive(false);
        effect.Stop();
        transform.DOKill();
        StackMoneyPool.Instance.OnReleaseMoney(this);
    }

    public void ActiveEffect()
    {
        effect.gameObject.SetActive(true);
        effect.Play();
    }

    public void SetMoneyAmount(MoneyType type) 
        => moneyAmount = moneyData.GetMoneyAmount(type);

    private void Update()
    {
        if (goToPlayer)
        {
            transform.position =
                Vector3.MoveTowards(transform.position,
                PlayerMovements.Instance.transform.position + new Vector3(0, 1, 0),
                moneyData.goToPlayerTime * Time.deltaTime);

            if (Vector3.Distance(transform.position,
                PlayerMovements.Instance.transform.position + new Vector3(0, 1, 0)) < .1f)
            {
                goToPlayer = false;
                GameManager.AddMoney(moneyAmount);
                Money_UI.Instance.SetTotalMoneyTxt();
                ReleaseResource();
            }
        }        
    }

    public void SetGoToPlayer()
    {
        goToPlayer = true;
    }

}
