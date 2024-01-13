using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "ScriptableObjects/PowerUp")]
public class PowerUp : ScriptableObject {
    
    public float energyThreshold;
    public FunctionsCallable selectedFunction;
    public int powerUpLevel;
    public string powerUpText;
    public bool enabled;

    public enum FunctionsCallable {
        IncreasePartyLevel,
        IncreaseItemsDropped,
        EnlargeMapShowed,
        UnlockMove,
        UnlockFinalRecipe,
        UnlockNewBiome,
        IncreaseHealingFromMushrooms,
        IncreaseHealingFromElectronicScraps,
        IncreaseStatsBonus,
    }

    public void CallSelectedFunction() {
        enabled = true;
        switch (selectedFunction) {
            case FunctionsCallable.IncreasePartyLevel: IncreasePartyLevel(powerUpLevel); break;
            case FunctionsCallable.IncreaseItemsDropped: IncreaseItemsDropped(powerUpLevel); break;
            case FunctionsCallable.EnlargeMapShowed: EnlargeMapShowed(powerUpLevel); break;
            case FunctionsCallable.UnlockMove: UnlockMove(powerUpLevel); break;
            case FunctionsCallable.UnlockFinalRecipe: UnlockFinalRecipe(powerUpLevel); break;
            case FunctionsCallable.UnlockNewBiome: UnlockNewBiome(powerUpLevel); break;
            case FunctionsCallable.IncreaseHealingFromMushrooms: IncreaseHealingFromMushrooms(powerUpLevel); break;
            case FunctionsCallable.IncreaseHealingFromElectronicScraps: IncreaseHealingFromElectronicScraps(powerUpLevel); break;
            case FunctionsCallable.IncreaseStatsBonus: IncreaseStatsBonus(powerUpLevel); break;
            default: throw new ArgumentOutOfRangeException();
        }
    }

    private void IncreasePartyLevel(int level) { Debug.Log($"Increase Party Level by: {level};"); GameManager.IncreasePartyLevel(); }
    
    private void IncreaseItemsDropped(int level) { Debug.Log($"Increase Items Dropped by: {level};"); }

    private void IncreaseHealingFromMushrooms(int level) { GameManager.IncreaseMinorHealingBonusMultiplier(level); }
    
    private void IncreaseHealingFromElectronicScraps(int level) { GameManager.IncreaseMajorHealingBonusMultiplier(level); }
    
    private void IncreaseStatsBonus(int level) { GameManager.IncreaseStatsBonusMultiplier(0.2f * level); }

    private void EnlargeMapShowed(int level) { Debug.Log($"Increase Map Showed by: {level};"); }

    private void UnlockMove(int level) { Debug.Log($"Unlocked {level} Move;"); }
    
    private void UnlockFinalRecipe(int level) { Debug.Log($"Unlocked Final Recipe"); }
    
    private void UnlockNewBiome(int biome) { Debug.Log($"Unlocked Biome"); }
}
