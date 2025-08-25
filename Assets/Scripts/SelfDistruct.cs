using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDistruct : MonoBehaviour
{
    [SerializeField] float time;
    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    void Start()
    {
        StartCoroutine(Waiter());
    }

}
