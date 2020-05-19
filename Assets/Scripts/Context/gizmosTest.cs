using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gizmosTest : MonoBehaviour
{
    // Start is called before the first frame update
    public float explosionRadius = 5.0f;
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
}
