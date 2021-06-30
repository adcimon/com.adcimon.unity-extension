using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public float speed = 1;
    public float offset = 1;

    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if( !target )
        {
            return;
        }

        if( rigidBody && !rigidBody.isKinematic )
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, target.position);
        if( distance <= offset )
        {
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        if( !target )
        {
            return;
        }

        if( !rigidBody || rigidBody.isKinematic )
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, target.position);
        if( distance <= offset )
        {
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        rigidBody.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
    }
}