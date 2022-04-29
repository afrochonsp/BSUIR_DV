using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PingPong
{
    public class GameManager : MonoBehaviour
    {
        public void Goal(Collider2D col, PlayersEnum player)
        {
            if (!col.TryGetComponent(out Ball ball))
            {
                return;
            }
            Debug.Log("Гол " + (player == PlayersEnum.LeftPlayer ? "левому" : "правому") + " игроку!");
        }
    }
}