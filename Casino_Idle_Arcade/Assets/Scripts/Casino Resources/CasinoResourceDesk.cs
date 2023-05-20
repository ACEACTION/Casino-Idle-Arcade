using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum ResourceDeskType
{
    chip, food
}

public class CasinoResourceDesk : MonoBehaviour
{
    [SerializeField] ChipResourceDeskData data;
    [SerializeField] Transform resourceSpawnPoint;
    public ResourceDeskType deskType;

    public virtual void AddResourceToStack(HandStack stack)
    {

    }

    public void SetResourcePos(Transform resource) => resource.position = resourceSpawnPoint.position;
    
    public void SetResourceParent(Transform resource, Transform parent) => resource.parent = parent;
    public void SetResourceLocalMove(Transform resource, Transform targetPos) => resource.DOLocalJump(targetPos.localPosition, 3, 1,
            data.addResourceToDeskTime)
            .OnComplete(() => {
                resource.DOShakeScale(0.1f, 0.3f).
                OnComplete(() => { });
            });

    public virtual void AddToStackList(List<CasinoResource> stackList, CasinoResource resource) => stackList.Add(resource);

}
