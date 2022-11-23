using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class Data {
    [SerializeField] private List<CV> cvs;
    [SerializeField] private List<Clip> clips;
    [SerializeField] private List<Anime> animes;

    public List<CV> GetCVs() { return cvs; }
    public List<Clip> GetClips() { return clips; }
    public List<Anime> GetAnimes() { return animes; }
}
