using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float localScaleMultiplier;

    private void Start()
    {
        Destroy(this.transform.parent.gameObject, 3.5f);
    }

    void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime * -transform.up);
        transform.localScale *=  localScaleMultiplier;
    }
}
