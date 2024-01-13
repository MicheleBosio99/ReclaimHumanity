using System;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsManager : MonoBehaviour {
    
    [SerializeField] private GameObject partyMemberStatistics;
    
    private bool firstTime = false;

    private void OnEnable() {
        // if (!firstTime) { firstTime = true; return; }
        
        var partyLength = GameManager.party.Count;
        var positions = GetPositions(partyLength);
        var numOfChildren = gameObject.transform.childCount;

        for (var i = 0; i < partyLength; i++) {
            var partyMemberStats = Instantiate(partyMemberStatistics, gameObject.transform, false);
            partyMemberStats.transform.localPosition = positions[i];
            partyMemberStats.GetComponent<PartyMemberStatsManager>().SetStatsParameters(i);
        }
    }

    private void OnDisable() { foreach (Transform child in transform) { Destroy(child.gameObject); } }

    private List<Vector2> GetPositions(int numOfMembers) {
        return numOfMembers switch {
            1 => new List<Vector2> { new Vector2(0.0f, 0.0f) },
            2 => new List<Vector2> { new Vector2(-187.0f, 0.0f), new Vector2(187.0f, 0.0f) },
            3 => new List<Vector2> { new Vector2(0.0f, 0.0f), new Vector2(-280.5f, 0.0f), new Vector2(280.5f, 0.0f) },
            _ => new List<Vector2>()
        };
    }
}
