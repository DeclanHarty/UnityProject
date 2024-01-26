using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class DrawableBoxCast : MonoBehaviour
{
    [SerializeField] protected Vector3 boxSize;
    [SerializeField] protected float castDistance;
    [SerializeField] protected Vector3 castDirection;
    [SerializeField] protected LayerMask castLayer;
    [SerializeField] protected Vector3 posOffset;
    // Start is called before the first frame update
    private void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position + posOffset + castDistance * castDirection, boxSize);
    }

    public abstract bool CastCheck();
}
