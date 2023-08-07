using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class WorkerManager
{
    public static List<RouletteCleaner> rouletteCleaners = new List<RouletteCleaner>();
    public static List<CasinoGame_ChipGame> casinoGamesForCleaners = new List<CasinoGame_ChipGame>();
    public static List<CasinoGame_ChipGame> casinoGamesForDeliverer = new List<CasinoGame_ChipGame>();

    public static List<ChipDeliverer> chipDeliverers = new List<ChipDeliverer>();

    public static void AddGamesToDeliverer(CasinoGame_ChipGame casinoGame)
    {        

        if (chipDeliverers.Count != 0)
        {
            if (chipDeliverers[0].capacity > 0)
            {
                chipDeliverers[0].capacity--;
                chipDeliverers[0].casinoGamesPoses.Add(casinoGame);
                casinoGame.chipDeliverer = chipDeliverers[0];
                casinoGamesForDeliverer.Remove(casinoGame);
            }
            else chipDeliverers.Remove(chipDeliverers[0]);

        }
    }

    public static void AddAvailableGamesToDeliverer()
    {
        if (chipDeliverers.Count > 0)
        {
            for (int i = 0; i < chipDeliverers[0].capacityAmount; i++)
            {
                if (casinoGamesForDeliverer.Count == 0) return;

                chipDeliverers[0].casinoGames.Add(casinoGamesForDeliverer[0]);
                casinoGamesForDeliverer[0].chipDeliverer = chipDeliverers[0];
                casinoGamesForDeliverer.Remove(casinoGamesForDeliverer[0]);
                chipDeliverers[0].capacity--;
            }
        }
        if (chipDeliverers[0].capacity == 0) chipDeliverers.Remove(chipDeliverers[0]);
    }

    public static void AddNewCasinoGamesToAvailabeCleaners(CasinoGame_ChipGame casinoGame)
    {
        if (rouletteCleaners.Count != 0)
        {
            if (rouletteCleaners[0].capacity > 0)
            {
                rouletteCleaners[0].capacity--;
                rouletteCleaners[0].casinoGames.Add(casinoGame);
                casinoGame.cleaner = rouletteCleaners[0];
                casinoGamesForCleaners.Remove(casinoGame);
            }
            else rouletteCleaners.Remove(rouletteCleaners[0]);

        }
    }

    public static void AddAvaiableCasinoGamesToCleaner()
    {
        if (rouletteCleaners.Count > 0)
        {
            for (int i = 0; i < rouletteCleaners[0].capacityAmount; i++)
            {
                if (casinoGamesForCleaners.Count == 0) return;

                rouletteCleaners[0].casinoGames.Add(casinoGamesForCleaners[0]);
                casinoGamesForCleaners[0].cleaner = rouletteCleaners[0];
                casinoGamesForCleaners.Remove(casinoGamesForCleaners[0]);
                rouletteCleaners[0].capacity--;

            }
        }
        if(rouletteCleaners[0].capacity == 0) rouletteCleaners.Remove(rouletteCleaners[0]);
    }

    public static void SetAgentMoveSpeed()
    {
        foreach (Worker worker in rouletteCleaners)
        {
            worker.SetMoveSpeed();
        }

        foreach (Worker worker in chipDeliverers)
        {
            worker.SetMoveSpeed(); 
        }
    }

    public static bool BuyedWorker() => rouletteCleaners.Count > 0 || chipDeliverers.Count > 0;

    
    public static void ResetData()
    {
        rouletteCleaners.Clear();
        casinoGamesForCleaners.Clear();
        casinoGamesForDeliverer.Clear();
        chipDeliverers.Clear();
    }
}
