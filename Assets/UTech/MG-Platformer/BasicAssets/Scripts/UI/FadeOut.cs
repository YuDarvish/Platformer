﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Time.time > 8 && gameObject.active)
        {
            gameObject.SetActive(false);
        }    
    }
}
