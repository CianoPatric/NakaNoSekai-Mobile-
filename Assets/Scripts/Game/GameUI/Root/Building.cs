using System;
using Game.GameUI.Root;
using UnityEngine;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Build CurrentBuilding;
    public Build PrefabOnCard;
    private Plane groundPlane;
    private Camera mainCamera;
    private void Start()
    {
        groundPlane = new Plane(Vector3.up, Vector3.zero);
        mainCamera = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControllCamera.Instance.canControl = false;
        CurrentBuilding = Instantiate(PrefabOnCard, eventData.position, Quaternion.identity);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(CurrentBuilding != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(eventData.position);
            if(groundPlane.Raycast(ray, out float pos))
            {
                Vector3 worldPosition = ray.GetPoint(pos);
                int x = Mathf.RoundToInt(worldPosition.x);
                int z = Mathf.RoundToInt(worldPosition.z);
                CurrentBuilding.transform.position = new Vector3(x, 0, z);
            }
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        bool available = true;
        if(CurrentBuilding.transform.position.x < 0 || CurrentBuilding.transform.position.x > BuildingsGrid.Instance.GridSize.x - CurrentBuilding.buildSize.x) available = false;
        if(CurrentBuilding.transform.position.z < 0 || CurrentBuilding.transform.position.z > BuildingsGrid.Instance.GridSize.y - CurrentBuilding.buildSize.y) available = false;
        if(available && CanPlacing(Mathf.RoundToInt(CurrentBuilding.transform.position.x), Mathf.RoundToInt(CurrentBuilding.transform.position.z))) available = false;
        
        if (available == false)
        {
            Destroy(CurrentBuilding.gameObject);
        }
        else
        {
            PlacingBuilding(Mathf.RoundToInt(CurrentBuilding.transform.position.x), Mathf.RoundToInt(CurrentBuilding.transform.position.z));   
        }
        ControllCamera.Instance.canControl = true;
    }

    private bool CanPlacing(int placeX, int placeY)
    {
        for(int x = 0; x < CurrentBuilding.buildSize.x; x++)
        {
            for(int y = 0; y < CurrentBuilding.buildSize.y; y++)
            {
                if(BuildingsGrid.Instance.grid[placeX + x, placeY +y] != null) return true;
            }
        }
        return false;
    }
    private void PlacingBuilding(int placeX, int placeY)
    {
        for(int x = 0; x < CurrentBuilding.buildSize.x; x++)
        {
            for(int y = 0; y < CurrentBuilding.buildSize.y; y++)
            {
                BuildingsGrid.Instance.grid[placeX + x, placeY + y] = CurrentBuilding;
            }
        }
    }
}
