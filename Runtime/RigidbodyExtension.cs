using UnityEngine;

public static class RigidbodyExtension
{
    /// <summary>
    /// Freezes the rigidbody movement.
    /// </summary>
    public static void Freeze( this Rigidbody rigidbody )
    {
        if( rigidbody == null )
        {
            return;
        }

        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.isKinematic = true;
    }
}