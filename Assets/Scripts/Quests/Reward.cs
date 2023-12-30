using System;
using UnityEngine;

namespace Portfolio.Rewards {
    [Serializable]
    public class Reward {
        [SerializeField] private int _moneyReward;
        [SerializeField] private int _xpReward;

        public override string ToString()
            => $"MoneyReward: {_moneyReward}, XP Reward: {_xpReward}";
    }
}