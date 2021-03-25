using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkController : NetworkBehaviour
{
    #region MovePlayer
    public void MovePlayer(Vector3 position, int playerID)
    {
        if (!isLocalPlayer)
            return;

        if (isServer)
            RpcMovePlayer(position, playerID);
        else
            CmdMovePlayer(position, playerID);
    }

    [Command]
    private void CmdMovePlayer(Vector3 position, int playerID)
    {
        RpcMovePlayer(position, playerID);
    }

    [ClientRpc]
    private void RpcMovePlayer(Vector3 position, int playerID)
    {
        if (playerID == 1)
            Variables.Instance.player1.GetComponent<PlayerController>().MovePlayer(position, playerID);
        if (playerID == 2)
            Variables.Instance.player2.GetComponent<PlayerController>().MovePlayer(position, playerID);
    }

    #endregion

    #region Start
    public void StartPlayer()
    {
        if (!isLocalPlayer)
            return;

        if (isServer)
            RpcStartPlayer();
        else
            CmdStartPlayer();
    }

    [Command]
    private void CmdStartPlayer()
    {
        RpcStartPlayer();
    }

    [ClientRpc]
    private void RpcStartPlayer()
    {
        if (Variables.Instance.player1 != null)
            Variables.Instance.player1.GetComponent<PlayerController>().StartGame();

        if (Variables.Instance.player2 != null)
            Variables.Instance.player2.GetComponent<PlayerController>().StartGame();
    }

    #endregion

    #region Bomb
    public void Bomb(int playerID)
    {
        if (!isLocalPlayer)
            return;

        
        if (isServer)
            RpcBomb(playerID);
        else
            CmdBomb(playerID);
    }

    [Command]
    private void CmdBomb(int playerID)
    {
        RpcBomb(playerID);
    }

    [ClientRpc]
    private void RpcBomb(int playerID)
    {
        if (playerID == 1)
            Variables.Instance.player1.GetComponent<PlayerController>().Bomb();
        if (playerID == 2)
            Variables.Instance.player2.GetComponent<PlayerController>().Bomb();
    }

    #endregion

    #region Missile
    public void Missile(int playerID)
    {
        if (!isLocalPlayer)
            return;


        if (isServer)
            RpcMissile(playerID);
        else
            CmdMissile(playerID);
    }

    [Command]
    private void CmdMissile(int playerID)
    {
        RpcMissile(playerID);
    }

    [ClientRpc]
    private void RpcMissile(int playerID)
    {
        if (playerID == 1)
            Variables.Instance.player1.GetComponent<PlayerController>().Missile();
        if (playerID == 2)
            Variables.Instance.player2.GetComponent<PlayerController>().Missile();
    }

    #endregion
}
