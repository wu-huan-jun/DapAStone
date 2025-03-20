using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("1");
            animator.Play("Death");
            StartCoroutine(yieldDestroy());
        }
    }
    IEnumerator yieldDestroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
    void OnCollisionEnter(Collision collision)
    {
    }
}
