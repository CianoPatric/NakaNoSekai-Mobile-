using R3;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI.Root.View
{
    public class GridSizePicker: MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform gridRoot;
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private int maxWidth = 10;
        [SerializeField] private int maxHeight = 10;

        private GridCell[,] _cells;
        private bool _isSelecting;
        private Vector2Int _currentSelection = Vector2Int.one;

        private Subject<GameUIEnterParams> _selectionSubject = new();
        public Subject<GameUIEnterParams> OnSizeSelected => _selectionSubject;

        private void Start()
        {
            GenerateGrid();
        }

        private void GenerateGrid()
        {
            _cells = new GridCell[maxWidth, maxHeight];

            for (int y = 0; y < maxHeight; y++)
            {
                for (int x = 0; x < maxWidth; x++)
                {
                    var go = Instantiate(cellPrefab, gridRoot);
                    var cell = go.GetComponent<GridCell>();
                    cell.SetCoordinates(x, y);
                    _cells[x, y] = cell;
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isSelecting = true;
            UpdateSelection(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isSelecting)
                UpdateSelection(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isSelecting = false;
            UpdateSelection(eventData); // Обновим выбор даже при отпускании
        }

        private void UpdateSelection(PointerEventData eventData)
        {
            var localPoint = ScreenToLocal(eventData.position);
            var (x, y) = GetCellIndex(localPoint);
            _currentSelection = new Vector2Int(x + 1, y + 1);

            for (int i = 0; i < maxWidth; i++)
            {
                for (int j = 0; j < maxHeight; j++)
                {
                    _cells[i, j].SetHighlighted(i < _currentSelection.x && j < _currentSelection.y);
                }
            }
        }

        private Vector2 ScreenToLocal(Vector2 screen)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(gridRoot, screen, null, out var local);
            return local;
        }

        private (int x, int y) GetCellIndex(Vector2 localPoint)
        {
            var size = gridRoot.rect.size / new Vector2(maxWidth, maxHeight);
            int x = Mathf.Clamp((int)((localPoint.x + gridRoot.rect.width / 2) / size.x), 0, maxWidth - 1);
            int y = Mathf.Clamp((int)((gridRoot.rect.height / 2 - localPoint.y) / size.y), 0, maxHeight - 1);
            return (x, y);
        }

        public void ApplySelection()
        {
            _selectionSubject.OnNext(new GameUIEnterParams(_currentSelection.x, _currentSelection.y));
        }
    }
}