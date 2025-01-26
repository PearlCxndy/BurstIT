using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject animal;

    public void Trigger()
    {
        if (animal.activeInHierarchy == false)
        {
            animal.SetActive(true);
        }
        else
        {
            animal.SetActive(false);
        }
    }
}
