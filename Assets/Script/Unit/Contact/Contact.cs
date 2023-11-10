using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class Contact : MonoBehaviour
{
    public event Action<float> OnGetDamage;
    public void GetDamage(float Damage)
    {
        OnGetDamage?.Invoke(Damage);
    }

    public void BeginContact()
    {

    }
    public void StayContact()
    {

    }
    public void EndContact()
    {

    }
}
