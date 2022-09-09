using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LetfRight : MonoBehaviour
{
    [SerializeField] int MoveDistance;
    void Start()
    {
        DOTween.Sequence()
           .Append(this.transform.DOLocalMoveX(MoveDistance, 1f).SetRelative())
           .Append(this.transform.DOLocalMoveX(-MoveDistance, 1f).SetRelative())
           .Play()
           .SetLoops(-1);
    }
}
