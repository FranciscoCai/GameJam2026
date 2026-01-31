using UnityEngine;
using System.Collections;

public class DestroyVfx : MonoBehaviour
{
    public float lifespam;
    void Start()
    {
        StartCoroutine(autodestroy());
    }

    private IEnumerator autodestroy()
    {
        yield return new WaitForSeconds(lifespam);
        Destroy(gameObject);
    }
}
