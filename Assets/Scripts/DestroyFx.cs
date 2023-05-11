using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFx : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyFxObject());
    }

    IEnumerator DestroyFxObject()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
