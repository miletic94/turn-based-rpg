using System;
using System.Collections.Generic;
using Presentation.Stat;

namespace Presentation.StatsManagement.StatsPanel
{
    public class StatPanelController
    {
        private readonly StatsPanelView _view;
        private readonly StatsManagementService _service;
        private StatManagementData _data;

        public StatPanelController(
            StatsPanelView view,
            StatsManagementService service)
        {
            _view = view;
            _service = service;
        }

        public void CreateStatPanel(Hero hero, Action<StatSaveData> onSaveButtonClicked)
        {
            _data = CreateStatManagementData(hero);
            RefreshAll();

            _view.SetSaveButtonClickedCallback(
                () => onSaveButtonClicked(CreateStatSaveData(_data)));
        }

        // ----------------------------
        // Refresh
        // ----------------------------

        private void RefreshAll()
        {
            RefreshAvailablePoints();
            RefreshStatRows();
        }

        private void RefreshAvailablePoints()
        {
            _view.ShowAvailablePoints(
                _data.AvailableStatPoints.ToString());
        }

        private void RefreshStatRows()
        {
            List<StatRowViewData> statsData = new();
            int idx = 0;
            foreach (var stat in _data.Stats.GetStats())
            {
                var statData = new StatRowViewData(
                idx,
                stat.Type,
                ConvertToViewValue(stat.BaseValue),
                ConvertToViewValue(stat.CurrentValue),
                () => ModifyStat(stat.Type, -1),
                () => ModifyStat(stat.Type, 1),
                stat.CurrentGTBase,
                _data.AvailableStatPoints > 0
                );

                statsData.Add(statData);
                idx++;
            }
            _view.Render(statsData);
        }

        // ----------------------------
        // Stat Modification
        // ----------------------------

        private void ModifyStat(StatType type, int delta)
        {
            if (delta > 0)
            {
                _service.IncreaseStat(_data, type);
            }
            else
            {
                _service.DecreaseStat(_data, type);
            }

            RefreshAll();
        }
        // ----------------------------
        //  Data
        // ----------------------------

        public StatSaveData CreateStatSaveData(StatManagementData data)
        {
            return new StatSaveData(
                data.Stats.GetStat(StatType.Attack).CurrentValue,
                data.Stats.GetStat(StatType.Defense).CurrentValue,
                data.Stats.GetStat(StatType.Magic).CurrentValue
                );
        }
        public StatManagementData CreateStatManagementData(Hero hero)
        {
            return new StatManagementData(
                hero.AvailableStatPoints,
                new Stats(hero.Attack, hero.Defense, hero.Magic));
        }

        // ----------------------------
        // View Data
        // ----------------------------

        private int ConvertToViewValue(float value)
        {
            return (int)(value * 10);
        }
    }
}