using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chef : MonoBehaviour
{
    //chef's name
    string name;

    //Define all skills (Gene)
    double stove;
    double oven;
    double cutting;
    double stirring;
    double plating;
    double confidence;


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
