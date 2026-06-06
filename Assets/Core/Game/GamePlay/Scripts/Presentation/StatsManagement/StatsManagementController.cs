using System;
using System.Collections.Generic;

namespace Presentation.StatsManagement
{
    public class StatsManagementController
    {
        private readonly StatManagementView _view;
        private readonly StatsManagementService _service;
        private StatManagementData _data;

        public StatsManagementController(
            StatManagementView view,
            StatsManagementService service)
        {
            _view = view;
            _service = service;
        }

        public void CreateStatPanel(Hero hero, Action<StatSaveData> onSaveButtonClicked)
        {
            _data = CreateStatManagementData(hero);

            RefreshAvailablePoints();
            CreateStatRows();

            _view.BindSaveClicked(
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
            _view.Panel.ShowAvailablePoints(
                _data.AvailableStatPoints.ToString());
        }

        private void CreateStatRows()
        {
            List<StatsPanel.StatRowViewData> statsData = new();
            foreach (var stat in _data.Stats.GetStats())
            {
                var statData = new StatsPanel.StatRowViewData(
                (int)stat.Type,
                stat.Type,
                ViewUtils.ConvertToViewValue(stat.BaseValue),
                ViewUtils.ConvertToViewValue(stat.CurrentValue),
                stat.CurrentGTBase,
                _data.AvailableStatPoints > 0
                );

                statsData.Add(statData);
            }
            var rows = _view.Panel.Render(statsData);
            foreach (var row in rows)
            {
                row.SetControlCallbacks(ModifyStat, ModifyStat);
            }
        }

        private void RefreshStatRows()
        {
            List<StatsPanel.StatRowViewData> statsData = new();
            foreach (var stat in _data.Stats.GetStats())
            {
                var statData = new StatsPanel.StatRowViewData(
                (int)stat.Type,
                stat.Type,
                ViewUtils.ConvertToViewValue(stat.BaseValue),
                ViewUtils.ConvertToViewValue(stat.CurrentValue),
                stat.CurrentGTBase,
                _data.AvailableStatPoints > 0
                );

                statsData.Add(statData);
            }
            _view.Panel.Refresh(statsData);
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
                data.Stats.GetStat(StatType.Magic).CurrentValue,
                data.AvailableStatPoints
                );
        }
        public StatManagementData CreateStatManagementData(Hero hero)
        {
            return new StatManagementData(
                hero.AvailableStatPoints,
                new Stats(hero.Attack, hero.Defense, hero.Magic));
        }
    }
}