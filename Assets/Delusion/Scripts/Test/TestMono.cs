using System.Collections.Generic;
using Dajunctic;
using UnityEngine;

public class TestMono: MonoBehaviour
{
    [SerializeField, GuidReference(typeof(ComputerConfig))] 
    private string configId;   

    [GuidReference(typeof(ComputerConfig))] 
    private int notConfigId; 


    // [GuidReference(typeof(ComputerConfig))] 
    // // [SerializeField]
    // private List<string> configIds;   
}