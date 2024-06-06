using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartBehavior : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 3.5f;

    public bool shouldMove = false;
    public Transform target;

    private void Update()
    {
        if (shouldMove)
            MoveToTarget();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Balloon"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    public void MoveToTarget()
    {
        if (target == null)
            Destroy(gameObject);
        else
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }
}
