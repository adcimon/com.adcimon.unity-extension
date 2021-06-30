using System;
using UnityEngine;
using UnityEngine.Events;

public class CollisionArea : MonoBehaviour
{
    [Serializable]
    public class OnCollisionEvent : UnityEvent<GameObject, Collider> { }

    public LayerMask layerMask;
    public OnCollisionEvent onCollisionEnter;
    public OnCollisionEvent onCollisionExit;

    private new Collider collider;

    private void Awake()
    {
        collider = this.GetComponent<Collider>();
    }

    private void OnCollisionEnter( Collision collision )
    {
        if( !((layerMask.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer) )
        {
            return;
        }

        onCollisionEnter.Invoke(collision.gameObject, collider);
    }

    private void OnCollisionExit( Collision collision )
    {
        if( !((layerMask.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer) )
        {
            return;
        }

        onCollisionExit.Invoke(collision.gameObject, collider);
    }
}