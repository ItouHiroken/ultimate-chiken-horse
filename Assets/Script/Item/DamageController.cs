using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [Tooltip("ダメージを付与！")] private int _damage=1;
    public int Damage { get { return _damage; } }

    void DamageOther(Collision2D other)
    { 
        
    }
}
