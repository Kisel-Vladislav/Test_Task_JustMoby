using _Project.Game.Logic.GameRule;
using _Project.Game.Logic.GameRule.Placement;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Infrastructure.Services.StaticData
{
    [Serializable]
    public class CubeGameStaticData
    {
        [SerializeField] private List<GameRuleType> GameRuleTypes;

        public GameObject Prefab;
        public float CubeSize;

        public List<CubeStaticData> CubeStaticDates;

        private List<IPlacementRule> gamePlacementRules;

        public List<IPlacementRule> GamePlacementRules
        {
            get => gamePlacementRules ?? GetGamePlacementRules();
            private set => gamePlacementRules = value;
        }

        private List<IPlacementRule> GetGamePlacementRules()
        {
            var rules = new List<IPlacementRule>();

            foreach (var ruleType in GameRuleTypes)
            {
                var rule = CreatePlacementRuleInstance(ruleType);
                if (rule != null)
                    rules.Add(rule);
            }

            GamePlacementRules = rules;
            return rules;
        }

        private IPlacementRule CreatePlacementRuleInstance(GameRuleType ruleType)
        {
            switch (ruleType)
            {
                case GameRuleType.SameColorPlacementRule:
                    return new SameColorPlacementRule();
                default:
                    return null;
            }
        }
    }
}