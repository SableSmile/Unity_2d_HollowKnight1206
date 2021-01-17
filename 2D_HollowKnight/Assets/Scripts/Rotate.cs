using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    public int x_speed; 
    public int y_speed;
    public int z_speed;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(x_speed*Time.deltaTime,y_speed*Time.deltaTime,z_speed*Time.deltaTime);
    }
}
