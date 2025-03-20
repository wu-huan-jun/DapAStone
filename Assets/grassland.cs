using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grassland : MonoBehaviour
{
    public float existingTime=2;//存在时间
    [SerializeField] private float passedTime = 0;

    // Update is called once per frame
    void Update()
    {
        passedTime += Time.deltaTime;
        if (passedTime >= existingTime)
        {
            //Destroy(this.gameObject);
        }
    }
}
