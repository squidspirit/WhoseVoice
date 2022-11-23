using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CV {
    [SerializeField] private int id;
    [SerializeField] private string name;

    public int GetId() { return id; }
    public string GetName() { return name; }
}