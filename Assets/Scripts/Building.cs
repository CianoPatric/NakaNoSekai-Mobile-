using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Vector2Int buildSize = Vector2Int.one;
    private GameObject CurrentBuilding;
    public GameObject PrefabOnCard;
    private void OnDrawGizmosSelected()
    {
        for(int x = 0; x < buildSize.x; x++)
        {
            for(int y = 0; y < buildSize.y; y++)
            {
                Gizmos.color = new Color(0.88f, 0f, 1f, 0.30f);
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, 0.1f, 1));
            }
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        CurrentBuilding = Instantiate(PrefabOnCard, Input.touches[0].position, Quaternion.identity);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(CurrentBuilding != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            if(groundPlane.Raycast(ray, out float pos))
            {
                Vector3 worldPosition = ray.GetPoint(pos);
                float x = worldPosition.x;
                float z = worldPosition.z;
                CurrentBuilding.transform.position = new Vector3(x, 0, z);
            }
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
