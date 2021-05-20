using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    void OnAnimationEnded()
    {
        Destroy(gameObject);
    }
}
