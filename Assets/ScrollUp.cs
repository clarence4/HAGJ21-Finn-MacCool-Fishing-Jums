using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUp : MonoBehaviour
{
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //private void OnEnable()
    //{
        
    //}

    //private IEnumerator Scroll()
    //{
    //    transform.position += Vector3.up * speed * Time.deltaTime;
    //    yield
    //}
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
}
