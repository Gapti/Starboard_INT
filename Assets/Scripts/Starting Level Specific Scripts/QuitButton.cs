using UnityEngine;

public class QuitButton : MonoBehaviour
{

    public float Width = 120f;
    public float Height = 40f;
    private bool _MenuActive;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.Escape))
	        _MenuActive = !_MenuActive;
	}

    void OnGUI()
    {
        if (_MenuActive && GUI.Button(new Rect(Screen.width/2f - Width /2f, 140f, Width , Height), "Quit"))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}
