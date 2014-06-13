using UnityEngine;
using System.Collections;

public class CharacterHealthMonitor : MonoBehaviour
{

    public GameObject ToMonitor;
    private INTCharacter _monitor;
    private UISlider _update;
    public GameObject ToUpdate;

	// Use this for initialization
	void Start ()
	{
	    _monitor = ToMonitor.GetComponent<INTCharacter>();
	    _update = ToUpdate.GetComponent<UISlider>();
	}

    public void ApplyDamage()
    {
        _monitor[INTAttributeTypes.Health].Damage += 1;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (_monitor != null && _update != null)
            _update.value = (_monitor.GetCurrentPercentageOf(INTAttributeTypes.Health) + 0.00001f) / 100f;
	}
}
