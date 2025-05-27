using System;
using Game.UI.Root.View;
using UnityEngine;

namespace Game.UI.Root
{
    public class StrategySelector:MonoBehaviour
    {

        private Action<IGridSizeProvider> _onStrategySelected;

        public void Init(Action<IGridSizeProvider> onSelected)
        {
            _onStrategySelected = onSelected;
        }

        public void OnManualSelected()
        {
            var picker = FindFirstObjectByType<GridSizePicker>();
            if (picker == null)
            {
                Debug.LogError("GridSizePicker не найден");
                return;
            }

            var provider = new ManualGridSizeProvider(picker);
            _onStrategySelected?.Invoke(provider);
            picker.ApplySelection();
        }

        public void GuestDataSelected()
        {
            var provider = new DatabaseGridSizeProvider();
            _onStrategySelected?.Invoke(provider);
        }
    }
}