using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class WorkerManager
{
    public static List<RouletteCleaner> rouletteCleaners = new List<RouletteCleaner>();
    public static List<Roulette> roulettes = new List<Roulette>();


    public static void AddNewRoulettesToAvailableWorker(Roulette roulette)
    {
        if (rouletteCleaners.Count != 0)
        {
            if (rouletteCleaners[0].capacity > 0)
            {
                rouletteCleaners[0].capacity--;
                rouletteCleaners[0].roulettes.Add(roulette);
                roulettes.Remove(roulette);
            }
            else rouletteCleaners.Remove(rouletteCleaners[0]);

        }
    }

    public static void AddAvaiableRouletteToCleaner()
    {
        if (rouletteCleaners.Count > 0)
        {
            for (int i = 0; i < rouletteCleaners[0].capacityAmount; i++)
            {
                if (roulettes.Count == 0) return;

                rouletteCleaners[0].roulettes.Add(roulettes[0]);
                roulettes[0].cleaner = rouletteCleaners[0];
                roulettes.Remove(roulettes[0]);
                rouletteCleaners[0].capacity--;

            }
        }
        if(rouletteCleaners[0].capacity == 0) rouletteCleaners.Remove(rouletteCleaners[0]);
    }
}
