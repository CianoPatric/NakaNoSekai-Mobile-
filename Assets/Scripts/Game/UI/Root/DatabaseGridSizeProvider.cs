using System;
using R3;
using UnityEngine;

namespace Game.UI.Root
{
    public class DatabaseGridSizeProvider : IGridSizeProvider
    {
        public Observable<GameUIEnterParams> GetEnterParams()
        {
            return Observable.Timer(TimeSpan.FromSeconds(0.5f))
                .Select(_ =>
                {
                    var dataFromDb = new Vector2Int(10, 15); // Здесь можно заменить на реальные данные
                    return new GameUIEnterParams(dataFromDb.x, dataFromDb.y, false);
                });
        }
    }
}