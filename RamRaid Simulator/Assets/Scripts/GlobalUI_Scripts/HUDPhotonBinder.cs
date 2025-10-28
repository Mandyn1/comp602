using System.Collections;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class HUDPhotonBinder : MonoBehaviour
{
    GameState gs;
    int localActor;
    int opponentActor;

    // cache to avoid spam updates
    string lastLocalName, lastOppName, lastLocalRole, lastOppRole;
    int lastLocalScore = int.MinValue, lastOppScore = int.MinValue, lastWallet = int.MinValue;

    void Start()
    {
        StartCoroutine(BindWhenReady());
        
        //Tester
        //GameHUD.Instance.Initialize("You", PlayerRole.Raider, "Opponent", PlayerRole.Police);
        //GameHUD.Instance.UpdateLocalScore(1200);
        //GameHUD.Instance.UpdateOpponentScore(950);
        //GameHUD.Instance.UpdateLocalBalance(2540);
    }

    IEnumerator BindWhenReady()
    {
        // wait for Photon room
        while (PhotonNetwork.CurrentRoom == null) yield return null;

        // find GameState and wait until it has two players in playerData
        while ((gs = FindObjectOfType<GameState>()) == null) yield return null;
        while (gs.playerData == null || gs.playerData.Count < 2) yield return null;

        localActor = gs.localPlayerNumber;
        opponentActor = gs.playerData.Keys.First(k => k != localActor);

        // first paint
        PushIdentity();
        PushNumbers();
    }

    void Update()
    {
        if (gs == null || gs.playerData == null || gs.playerData.Count < 2) return;
        PushIdentity();
        PushNumbers();
    }

    void PushIdentity()
    {
        // names from Photon via ActorNumber
        string localName = PhotonNetwork.CurrentRoom.Players.TryGetValue(localActor, out Player lp) ? lp.NickName : $"P{localActor}";
        string opponentName = PhotonNetwork.CurrentRoom.Players.TryGetValue(opponentActor, out Player op) ? op.NickName : $"P{opponentActor}";

        // roles from GameState.playerData[*].position  ("Police" / "Raider")
        string localRole = gs.playerData[localActor].position;
        string opponentRole = gs.playerData[opponentActor].position;

        if (localName != lastLocalName || localRole != lastLocalRole)
        {
            GameHUD.Instance?.SetLocalIdentity(localName, ToRole(localRole));
            lastLocalName = localName; lastLocalRole = localRole;
        }
        if (opponentName != lastOppName || opponentRole != lastOppRole)
        {
            GameHUD.Instance?.SetOpponentIdentity(opponentName, ToRole(opponentRole));
            lastOppName = opponentName; lastOppRole = opponentRole;
        }
    }

    void PushNumbers()
    {
        var local = gs.playerData[localActor];
        var opp = gs.playerData[opponentActor];

        if (local.score != lastLocalScore)
        {
            GameHUD.Instance?.UpdateLocalScore(local.score);
            lastLocalScore = local.score;
        }

        if (opp.score != lastOppScore)
        {
            GameHUD.Instance?.UpdateOpponentScore(opp.score);
            lastOppScore = opp.score;
        }

        int wallet = (local.position == "Raider") ? local.raiderBank : local.policeBank;
        if (wallet != lastWallet)
        {
            GameHUD.Instance?.UpdateLocalBalance(wallet);
            lastWallet = wallet;
        }
    }

    PlayerRole ToRole(string s) => (s == "Police") ? PlayerRole.Police : PlayerRole.Raider;
}
