using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipAttribute {
    public Clip clip { get; }
    public Anime anime { get; }
    public Character character { get; }
    public CV cv { get; }

    public ClipAttribute(Clip clip, Anime anime, Character character, CV cv) {
        this.clip = clip;
        this.anime = anime;
        this.character = character;
        this.cv = cv;
    }
}
