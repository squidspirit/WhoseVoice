using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 此亂數產生器保證，範圍長度的一半次數內，不會重複。
class RandomGenerator {

    private int count;
    private int noRepeat;
    private List<int> nums;

    public RandomGenerator(int min, int max) {
        count = max - min;
        noRepeat = count / 2;
        nums = new List<int>(count);
        List<int> rnds = new List<int>();
        for (int i = 0; i < count; i ++)
            nums.Add(min + i);
        for (int i = 0; i < noRepeat; i ++) {
            int rndIndex = Random.Range(0, count - i - 1);
            rnds.Add(nums[rndIndex]);
            nums.RemoveAt(rndIndex);
        }
        for (int i = 0; i < noRepeat; i++)
            nums.Add(rnds[i]);
    }

    public int GetRandom() {
        int rndIndex = Random.Range(0, count - noRepeat - 1);
        int rnd = nums[rndIndex];
        nums.RemoveAt(rndIndex);
        nums.Add(rnd);
        return rnd;
    }
}
