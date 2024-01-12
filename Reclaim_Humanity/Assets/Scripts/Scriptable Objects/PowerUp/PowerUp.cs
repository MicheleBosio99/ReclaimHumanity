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
        IncreaseEnemyLevel,
        IncreaseItemsDropped,
        EnlargeMapShowed,
        UnlockMove,
        UnlockFinalRecipe,
    }

    public void CallSelectedFunction() {
        enabled = true;
        switch (selectedFunction) {
            case FunctionsCallable.IncreasePartyLevel: IncreasePartyLevel(powerUpLevel); break;
            case FunctionsCallable.IncreaseEnemyLevel: IncreaseEnemyLevel(powerUpLevel); break;
            case FunctionsCallable.IncreaseItemsDropped: IncreaseItemsDropped(powerUpLevel); break;
            case FunctionsCallable.EnlargeMapShowed: EnlargeMapShowed(powerUpLevel); break;
            case FunctionsCallable.UnlockMove: UnlockMove(powerUpLevel); break;
            case FunctionsCallable.UnlockFinalRecipe: UnlockFinalRecipe(powerUpLevel); break;
            default: throw new ArgumentOutOfRangeException();
        }
    }

    private void IncreasePartyLevel(int level) { Debug.Log($"Increase Party Level by: {level};"); GameManager.IncreasePartyLevel(); }
    
    private void IncreaseEnemyLevel(int level) { Debug.Log($"Increase Enemy Level by: {level};"); }
    
    private void IncreaseItemsDropped(int level) { Debug.Log($"Increase Items Dropped by: {level};"); }

    private void EnlargeMapShowed(int level) { Debug.Log($"Increase Map Showed by: {level};"); }

    private void UnlockMove(int level) { Debug.Log($"Unlock {level} Move;"); }
    
    private void UnlockFinalRecipe(int level) { Debug.Log($"Unlock Final Recipe"); }
}
