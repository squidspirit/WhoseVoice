using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Anime {
    [SerializeField] private int id;
    [SerializeField] private string title;
    [SerializeField] private List<Character> characters;

    public int GetId() { return id; }
    public string GetTitle() { return title; }
    public List<Character> GetCharacters() { return characters; }
}