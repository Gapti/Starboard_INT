using UnityEngine;
using System.Collections;

public class RightClickManager : MonoBehaviour {

	public static RightClickManager instance;

	public GameObject RightClickPrefab;

	public GameObject TempRightClick;
	public ItemSlot _itemSlot;

	private Camera _camera;

	void Awake () { instance = this; }
	void OnDestroy () { instance = null; }

	void Start()
	{
		_camera = NGUITools.FindCameraForLayer(gameObject.layer);
	}

	public void Show(ItemSlot itemSlot)
	{
		Vector3 pos = Input.mousePosition;

		pos.x = Mathf.Clamp01(pos.x / Screen.width);
		pos.y = Mathf.Clamp01(pos.y / Screen.height);

		if(TempRightClick == null)
		{
			_itemSlot = itemSlot;
			TempRightClick = NGUITools.AddChild(this.gameObject.transform.parent.gameObject, RightClickPrefab);
			TempRightClick.transform.position = _camera.ViewportToWorldPoint(pos);
			RightClickMaker m = TempRightClick.GetComponent<RightClickMaker>();
			m.item = _itemSlot.item;
		}
	}
}
