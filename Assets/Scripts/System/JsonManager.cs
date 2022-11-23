using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class JsonManager : MonoBehaviour {
    public string dataJson;

    private List<Clip> clips;
    private List<Anime> animes;
    private List<CV> cvs;
    private Data data;

    private RandomGenerator clipRndGen;

    void Awake() {
        TextAsset rawData;
        rawData = Resources.Load<TextAsset>(dataJson);
        data = JsonUtility.FromJson<Data>(rawData.ToString());
        clips = data.GetClips();
        animes = data.GetAnimes();
        cvs = data.GetCVs();
        clipRndGen = new RandomGenerator(0, clips.Count);
    }

    public ClipAttribute GetClipData(Clip clip) {
        return GetClipData(clips.Find(element => element.GetId().Equals(clip.GetId())));
    }

    public ClipAttribute GetClipData(int index) {
        Clip clip = clips[index];
        Anime anime = animes.Find(element => element.GetId() == clip.GetAnimeId());
        Character character = anime.GetCharacters().Find(
            element => element.GetID() == clip.GetCharacterId());
        CV cv = cvs.Find(element => element.GetId() == character.GetCvId());
        return new ClipAttribute(clip, anime, character, cv);
    }

    public ClipAttribute GetRandomClipData() {
        return GetClipData(clipRndGen.GetRandom());
    }

    public CV GetRandomCV() {
        return cvs[Random.Range(0, cvs.Count)];
    }

    public CV GetRandomCV(CV except) {
        CV cv;
        do {
            cv = cvs[Random.Range(0, cvs.Count)];
        } while (cv.GetId() == except.GetId());
        return cv;
    }

    public List<CV> GetRandomCVList(CV except, int count) {
        HashSet<CV> cvSet = new HashSet<CV>();
        while (cvSet.Count < count)
            cvSet.Add(GetRandomCV(except));
        return new List<CV>(cvSet);
    }
}
