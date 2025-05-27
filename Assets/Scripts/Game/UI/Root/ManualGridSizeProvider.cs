using Game.UI.Root.View;
using R3;

namespace Game.UI.Root
{
    public class ManualGridSizeProvider : IGridSizeProvider
    {
        private readonly GridSizePicker _picker;

        public ManualGridSizeProvider(GridSizePicker picker)
        {
            _picker = picker;
        }

        public Observable<GameUIEnterParams> GetEnterParams()
        {
            return _picker.OnSizeSelected
                .Select(size => new GameUIEnterParams(size.Width, size.Height, size.GameMode));
        }
    }
}