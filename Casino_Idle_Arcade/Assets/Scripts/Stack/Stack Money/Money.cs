using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : CasinoResource
{
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

    public override void ReleasResource()
    {
        base.ReleasResource();
        effect.gameObject.SetActive(false);
        effect.Stop();
        StackMoneyPool.Instance.OnReleaseMoney(this);
    }

    public void ActiveEffect()
    {
        effect.gameObject.SetActive(true);
        effect.Play();
    }


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
                GameManager.AddMoney(moneyData.moneyPrice);
                ReleasResource();
            }
        }        
    }

    public void SetGoToPlayer()
    {
        goToPlayer = true;
    }

}
