using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 Offset;

    private GameObject _player;

    private void Start()
    {
        _player = GameManager.I.PlayerManager.Player;
    }

    void LateUpdate()
    {
        transform.position = _player.transform.position + Offset;
    }
}
