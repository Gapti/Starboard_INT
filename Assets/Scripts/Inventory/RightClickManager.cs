using UnityEngine;
using System.Collections;

public class RightClickManager : MonoBehaviour {

	public static RightClickManager instance;

	public GameObject RightClickPrefab;

	private GameObject _tempRightClick;
	private ItemSlot _itemSlot;

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

		if(_tempRightClick == null)
		{
			_itemSlot = itemSlot;
			_tempRightClick = NGUITools.AddChild(this.gameObject.transform.parent.gameObject, RightClickPrefab);
			_tempRightClick.transform.position = _camera.ViewportToWorldPoint(pos);
			RightClickMaker m = _tempRightClick.GetComponent<RightClickMaker>();
			m.Make(_itemSlot);
		}
	}

	public void Clear()
	{
		if(_tempRightClick != null)
		{
			NGUITools.Destroy(_tempRightClick);
			_tempRightClick = null;
		}

		_itemSlot = null;
	}
}
