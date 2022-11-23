using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Clip {
    [SerializeField] private int id;
    [SerializeField] private int anime;
    [SerializeField] private int character;

    public int GetId() { return id; }
    public int GetAnimeId() { return anime; }
    public int GetCharacterId() { return character; }
}