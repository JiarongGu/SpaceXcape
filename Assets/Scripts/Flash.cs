using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    private new Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Animation());
    }

    IEnumerator Animation() {
        renderer.enabled = !renderer.enabled;
        yield return new WaitForSeconds(0.5f);
    }
}
