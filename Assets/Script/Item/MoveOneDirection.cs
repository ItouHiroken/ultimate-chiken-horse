using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveOneDirection : MonoBehaviour
{
    [SerializeField] float _moveSeconds = 1f;
    [SerializeField] Vector3 _moveDirection=Vector3.up +Vector3.right;
    void Start()
    {
        this.transform.DOMove(_moveDirection, _moveSeconds)
            .SetRelative(true)
            .SetEase(Ease.InBounce)
            .SetLoops(-1, LoopType.Incremental);
    }
}
