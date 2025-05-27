using System;
using R3;

namespace Game.UI.Root
{
    public interface IGridSizeProvider
    {
        Observable<GameUIEnterParams> GetEnterParams();
    }
}