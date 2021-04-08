using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class dmg : MonoBehaviour
{
    public playerVariables playervar;
    private void OnParticleCollision(GameObject other)
    {
        playervar.TakeDamage(1);
    }
}
