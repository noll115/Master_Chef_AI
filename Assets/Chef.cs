using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : MonoBehaviour
{

    //Define all skills (Gene)
    public double stove;
    public double oven;
    public double cutting;
    public double stirring;
    public double plating;
    public double confidence;


    void Awake()
    {
        name = "Box Man";
        stove = Random.value;
        oven = Random.value;
        cutting = Random.value;
        stirring = Random.value;
        plating = Random.value;
        confidence = Random.value;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
