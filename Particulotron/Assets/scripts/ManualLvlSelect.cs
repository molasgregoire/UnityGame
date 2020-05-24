using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualLvlSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            levelCreator.level = 0;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            levelCreator.level = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            levelCreator.level = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            levelCreator.level = 1;
        }
    }
    


}
