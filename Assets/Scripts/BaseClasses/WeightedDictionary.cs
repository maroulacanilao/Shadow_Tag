using System.Collections.Generic;
using AYellowpaper.SerializedCollections;

[System.Serializable]
public class WeightedDictionary<T>
{
    [UnityEngine.SerializeField] private SerializedDictionary<T, float> itemDictionary;
    private Dictionary<T, float> fixedChances;

    private bool hasInitialized = false;
    private float totalWeight;

    private T itemWithLargestWeight;

    public void Initialize()
    {
        if (hasInitialized) return;

        ForceInitialize();
    }

    public void ForceInitialize()
    {
        fixedChances = new Dictionary<T, float>();
        if (itemDictionary.Count == 0) return;
        RecalculateChances();

        hasInitialized = true;
    }

    public void RecalculateChances()
    {
        totalWeight = 0;
        fixedChances = new Dictionary<T, float>();

        float _largestWeight = 0;
        foreach (var w in itemDictionary)
        {
            if (w.Value > _largestWeight)
            {
                _largestWeight = w.Value;
                itemWithLargestWeight = w.Key;
            }

            totalWeight += w.Value;
            fixedChances.Add(w.Key, totalWeight);
        }
    }

    public T GetWeightedRandom()
    {
        // init class if it hasn't already
        Initialize();

        // check if the dictionary size the item list count is the same. recalculate chances if it is not 
        if (fixedChances == null || fixedChances.Count != itemDictionary.Count) RecalculateChances();

        // early returns
        if (fixedChances.Count == 0) return default;

        // random number
        var rngVal = UnityEngine.Random.value * totalWeight;

        foreach (var w in fixedChances)
        {
            if (w.Value > rngVal)
            {
                return w.Key;
            }
        }

        // fallback option, returns item with largest weight. this should not happen tho
        return itemWithLargestWeight;
    }

    public void AddItem(T itemToAdd, float weight)
    {
        itemDictionary.Add(itemToAdd, weight);

        totalWeight += weight;

        fixedChances.Add(itemToAdd, totalWeight);
    }

    public void RemoveItem(T itemToRemove)
    {
        if (!itemDictionary.Remove(itemToRemove)) return;
        if (!fixedChances.Remove(itemToRemove)) return;

        RecalculateChances();
    }

    public float GetChanceOfItem(T itemToEvaluate)
    {
        return itemDictionary[itemToEvaluate];
    }

    public void ChangeWeightOfItem(T itemToChange, float newWeight)
    {
        itemDictionary[itemToChange] = newWeight;
        RecalculateChances();
    }
}