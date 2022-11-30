using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character {
    [SerializeField] private int cv;
    [SerializeField] private int id;
    [SerializeField] private string name;

    public int GetId() { return id; }
    public string GetName() { return name; }
    public int GetCvId() { return cv; }
}