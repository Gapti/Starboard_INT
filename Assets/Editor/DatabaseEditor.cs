using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

[CustomEditor(typeof(ItemDataBase))]
public class DatabaseEditor : Editor {

	public ItemType CurrentVisableCatagoery;

	public int index = 0;
	public int ItemIndex = 0;
	public bool IsFoldOutOpen;

	public override void OnInspectorGUI()
	{

		var path = Application.dataPath + "/StreamingAssets";
		var control = (ItemDataBase)target;

		CurrentVisableCatagoery =(ItemType) EditorGUILayout.EnumPopup("Item Catagoery", CurrentVisableCatagoery);

		NGUIEditorTools.DrawSeparator();

		switch (CurrentVisableCatagoery) {
		case ItemType.Armor:
			DrawArmorList(control);
			break;
		case ItemType.Consumable:
			DrawConsumableList(control);
			break;
		case ItemType.Enhancer:
			DrawEnhancerList(control);
			break;
		case ItemType.Generator:
			DrawGeneratorList(control);
			break;
		case ItemType.Misc:
			DrawMiscList(control);
			break;
		case ItemType.Quest:
			DrawQuestList(control);
			break;
		case ItemType.LaserWeapon:
			DrawLaserWeaponList(control);
			break;
		case ItemType.Melee:
			DrawMeleeWeaponList(control);
			break;
		case ItemType.LegacyWeapon:
			DrawLegacyWeaponList(control);
			break;
		case ItemType.MagneticWeapon:
			DrawMagneticWeaponList(control);
			break;

		default:
						break;
		}

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Save to xml");

		if(GUILayout.Button("Save"))
		{
			control.Save(Path.Combine(path, "magneticweapons.xml"), ItemType.MagneticWeapon);
			control.Save(Path.Combine(path, "legacyweapons.xml"), ItemType.LegacyWeapon);
			control.Save(Path.Combine(path, "laserweapons.xml"), ItemType.LaserWeapon);
			control.Save(Path.Combine(path, "meleeweapons.xml"), ItemType.Melee);
			control.Save(Path.Combine(path, "armor.xml"), ItemType.Armor);
			control.Save(Path.Combine(path, "misc.xml"), ItemType.Misc);
			control.Save (Path.Combine(path, "consumables.xml"), ItemType.Consumable);
			control.Save(Path.Combine(path, "Quests.xml"), ItemType.Quest);
			control.Save(Path.Combine(path, "Generators.xml"), ItemType.Generator);
			control.Save(Path.Combine(path, "enhancers.xml"), ItemType.Enhancer);
		}

		if(GUILayout.Button("Load"))
		{
			control.LoadXML(Path.Combine(path, "magneticweapons.xml"), ItemType.MagneticWeapon);
			control.LoadXML(Path.Combine(path, "legacyweapons.xml"), ItemType.LegacyWeapon);
			control.LoadXML(Path.Combine(path, "laserweapons.xml"), ItemType.LaserWeapon);
			control.LoadXML(Path.Combine(path, "meleeweapons.xml"), ItemType.Melee);
			control.LoadXML(Path.Combine(path, "armor.xml"), ItemType.Armor);
			control.LoadXML(Path.Combine(path, "misc.xml"), ItemType.Misc);
			control.LoadXML(Path.Combine(path, "consumables.xml"), ItemType.Consumable);
			control.LoadXML(Path.Combine(path, "Quests.xml"), ItemType.Quest);
			control.LoadXML(Path.Combine(path, "Generators.xml"), ItemType.Generator);
			control.LoadXML(Path.Combine(path, "enhancers.xml"), ItemType.Enhancer);
		}
		
	}

	public Rect DrawSprite (Texture2D tex, Rect sprite, Material mat, bool addPadding, int maxSize)
	{
		float paddingX = addPadding ? 4f / tex.width : 0f;
		float paddingY = addPadding ? 4f / tex.height : 0f;
		float ratio = (sprite.height + paddingY) / (sprite.width + paddingX);
		
		ratio *= (float)tex.height / tex.width;
		
		// Draw the checkered background
		Color c = GUI.color;
		Rect rect = GUILayoutUtility.GetRect(0f, 0f);
		rect.width = Screen.width - rect.xMin;
		rect.height = rect.width * ratio;
		GUILayout.Space(Mathf.Min(rect.height, maxSize));
		GUI.color = c;
		
		if (maxSize > 0)
		{
			float dim = maxSize / Mathf.Max(rect.width, rect.height);
			rect.width *= dim;
			rect.height *= dim;
		}
		
		// We only want to draw into this rectangle
		if (Event.current.type == EventType.Repaint)
		{
			if (mat == null)
			{
				GUI.DrawTextureWithTexCoords(rect, tex, sprite);
			}
			else
			{
				// NOTE: DrawPreviewTexture doesn't seem to support BeginGroup-based clipping
				// when a custom material is specified. It seems to be a bug in Unity.
				// Passing 'null' for the material or omitting the parameter clips as expected.
				UnityEditor.EditorGUI.DrawPreviewTexture(sprite, tex, mat);
				//UnityEditor.EditorGUI.DrawPreviewTexture(drawRect, tex);
				//GUI.DrawTexture(drawRect, tex);
			}
			rect = new Rect(sprite.x + rect.x, sprite.y + rect.y, sprite.width, sprite.height);
		}
		return rect;
	}


	void CommonItemProps(Item item)
	{
		EditorGUILayout.LabelField("ItemID", item.id.ToString());
		item.ItemName = EditorGUILayout.TextField("Item Name", item.ItemName);
		item.Description = EditorGUILayout.TextField("Item Description", item.Description);
		item.Slot = (SlotType)EditorGUILayout.EnumPopup("What equipment slot? ", item.Slot);
		item.Stackable = EditorGUILayout.Toggle("Stackable ?", item.Stackable);
		item.MaxStack = EditorGUILayout.IntField("Max Stack", item.MaxStack);
		item.ItemGameObject = EditorGUILayout.TextField("GameObject for Item", item.ItemGameObject);
		item.Atlas = EditorGUILayout.TextField("Sprite Atlas", item.Atlas);

		UIAtlas atlasObject = Resources.Load <UIAtlas>(item.Atlas);

		if(atlasObject != null)
		{
			string[] sprites = atlasObject.GetListOfSprites().ToArray();

			index = System.Array.IndexOf<string>(sprites, item.ItemSprite);
			index = EditorGUILayout.Popup(index, sprites);


			UISpriteData sprite = (index > 0) ? atlasObject.GetSprite(sprites[index]) : null;


			if(sprite != null)
			{
				item.ItemSprite = sprite.name;

				Rect spriteRect = new Rect(sprite.x, sprite.y,sprite.width, sprite.height);

				spriteRect = NGUIMath.ConvertToTexCoords(spriteRect, atlasObject.spriteMaterial.mainTexture.width, atlasObject.spriteMaterial.mainTexture.height);
				DrawSprite((Texture2D)atlasObject.spriteMaterial.mainTexture, spriteRect, null,false,100);
			}
		}

		NGUIEditorTools.DrawSeparator();
	}

	void DrawLaserWeaponList(ItemDataBase control)
	{
		if(control.LaserWeaponList.Count > 0)
		{
			ItemIndex = EditorGUILayout.IntSlider("Item Index", ItemIndex, 0, control.LaserWeaponList.Count - 1);

			NGUIEditorTools.DrawSeparator();

			CommonItemProps(control.LaserWeaponList[ItemIndex]);
			
			control.LaserWeaponList[ItemIndex].MinDamage = EditorGUILayout.FloatField("Min Damage", control.LaserWeaponList[ItemIndex].MinDamage);
			control.LaserWeaponList[ItemIndex].MaxDamage = EditorGUILayout.FloatField("Max Damage", control.LaserWeaponList[ItemIndex].MaxDamage);
			control.LaserWeaponList[ItemIndex].MinRange = EditorGUILayout.FloatField("MinRange", control.LaserWeaponList[ItemIndex].MinRange);
			control.LaserWeaponList[ItemIndex].MaxRange = EditorGUILayout.FloatField("MaxRange", control.LaserWeaponList[ItemIndex].MaxRange);
			control.LaserWeaponList[ItemIndex].CriticalStrike = EditorGUILayout.IntField("CriticalStrike", control.LaserWeaponList[ItemIndex].CriticalStrike);		
			control.LaserWeaponList[ItemIndex].CoolDownTime = EditorGUILayout.FloatField("Cool Down Time", control.LaserWeaponList[ItemIndex].CoolDownTime);
		}

		EditorGUILayout.BeginHorizontal();

		if(GUILayout.Button("Add Item"))
		{
			var newWeapon = new LaserWeapon();
			newWeapon.id = MakeNewID(control.LaserWeaponList);
			newWeapon.Type = ItemType.LaserWeapon;
			control.LaserWeaponList.Add(newWeapon);
			ItemIndex = control.LaserWeaponList.Count - 1;
		}

		if(GUILayout.Button("Remove Item"))
		{
			control.LaserWeaponList.RemoveAt(ItemIndex);
			ItemIndex = control.LaserWeaponList.Count - 1;
		}

		EditorGUILayout.EndHorizontal();
	}

	void DrawMagneticWeaponList(ItemDataBase control)
	{
		if(control.MagneticWeaponList.Count > 0)
		{
			ItemIndex = EditorGUILayout.IntSlider("Item Index", ItemIndex, 0, control.MagneticWeaponList.Count - 1);
			NGUIEditorTools.DrawSeparator();

			CommonItemProps(control.MagneticWeaponList[ItemIndex]);
			
			control.MagneticWeaponList[ItemIndex].MinDamage = EditorGUILayout.FloatField("Min Damage", control.MagneticWeaponList[ItemIndex].MinDamage);
			control.MagneticWeaponList[ItemIndex].MaxDamage = EditorGUILayout.FloatField("Max Damage", control.MagneticWeaponList[ItemIndex].MaxDamage);
			control.MagneticWeaponList[ItemIndex].MinRange = EditorGUILayout.FloatField("MinRange", control.MagneticWeaponList[ItemIndex].MinRange);
			control.MagneticWeaponList[ItemIndex].MaxRange = EditorGUILayout.FloatField("MaxRange", control.MagneticWeaponList[ItemIndex].MaxRange);
			control.MagneticWeaponList[ItemIndex].CriticalStrike = EditorGUILayout.IntField("CriticalStrike", control.MagneticWeaponList[ItemIndex].CriticalStrike);
			control.MagneticWeaponList[ItemIndex].BatteryRecharge = EditorGUILayout.FloatField("BatteryRecharge", control.MagneticWeaponList[ItemIndex].BatteryRecharge);
		}
		
		EditorGUILayout.BeginHorizontal();
		
		if(GUILayout.Button("Add Item"))
		{
			var newWeapon = new MagneticWeapon();
			newWeapon.id = MakeNewID(control.MagneticWeaponList);
			newWeapon.Type = ItemType.MagneticWeapon;
			control.MagneticWeaponList.Add(newWeapon);
			ItemIndex = control.MagneticWeaponList.Count - 1;
		}
		
		if(GUILayout.Button("Remove Item"))
		{
			control.MagneticWeaponList.RemoveAt(ItemIndex);
			ItemIndex = control.MagneticWeaponList.Count - 1;
		}
		
		EditorGUILayout.EndHorizontal();
	}

	void DrawLegacyWeaponList(ItemDataBase control)
	{

		if(control.LegacyWeaponList.Count > 0 )
		{
			ItemIndex = EditorGUILayout.IntSlider("Item Index", ItemIndex, 0, control.LegacyWeaponList.Count - 1);
			NGUIEditorTools.DrawSeparator();

			CommonItemProps(control.LegacyWeaponList[ItemIndex]);
			control.LegacyWeaponList[ItemIndex].MinDamage = EditorGUILayout.FloatField("Min Damage", control.LegacyWeaponList[ItemIndex].MinDamage);
			control.LegacyWeaponList[ItemIndex].MaxDamage = EditorGUILayout.FloatField("Max Damage", control.LegacyWeaponList[ItemIndex].MaxDamage);
			control.LegacyWeaponList[ItemIndex].MaxRange = EditorGUILayout.FloatField("MaxRange", control.LegacyWeaponList[ItemIndex].MaxRange);
			control.LegacyWeaponList[ItemIndex].CriticalStrike = EditorGUILayout.IntField("CriticalStrike", control.LegacyWeaponList[ItemIndex].CriticalStrike);
			control.LegacyWeaponList[ItemIndex].ReloadTime = EditorGUILayout.FloatField("Reload Time", control.LegacyWeaponList[ItemIndex].ReloadTime);
		}
		
		EditorGUILayout.BeginHorizontal();
		
		if(GUILayout.Button("Add Item"))
		{
			var newWeapon = new LegacyWeapon();
			newWeapon.id = MakeNewID(control.LegacyWeaponList);
			newWeapon.Type = ItemType.LegacyWeapon;
			control.LegacyWeaponList.Add(newWeapon);
			ItemIndex = control.LegacyWeaponList.Count - 1;
		}
		
		if(GUILayout.Button("Remove Item"))
		{
			control.LegacyWeaponList.RemoveAt(ItemIndex);
			ItemIndex = control.LegacyWeaponList.Count - 1;
		}
		
		EditorGUILayout.EndHorizontal();
	}

	void DrawMeleeWeaponList(ItemDataBase control)
	{
		if(control.MeleeList.Count > 0)
		{
			ItemIndex = EditorGUILayout.IntSlider("Item Index", ItemIndex, 0, control.MeleeList.Count - 1);
			NGUIEditorTools.DrawSeparator();

			CommonItemProps(control.MeleeList[ItemIndex]);
			
			control.MeleeList[ItemIndex].MinDamage = EditorGUILayout.FloatField("Min Damage", control.MeleeList[ItemIndex].MinDamage);
			control.MeleeList[ItemIndex].MaxDamage = EditorGUILayout.FloatField("Max Damage", control.MeleeList[ItemIndex].MaxDamage);
			control.MeleeList[ItemIndex].MinRange = EditorGUILayout.FloatField("MinRange", control.MeleeList[ItemIndex].MinRange);
			control.MeleeList[ItemIndex].MaxRange = EditorGUILayout.FloatField("MaxRange", control.MeleeList[ItemIndex].MaxRange);
			control.MeleeList[ItemIndex].CriticalStrike = EditorGUILayout.IntField("CriticalStrike", control.MeleeList[ItemIndex].CriticalStrike);
			control.MeleeList[ItemIndex].Exhaustion = EditorGUILayout.FloatField("Exaustion", control.MeleeList[ItemIndex].Exhaustion);
		}
		
		EditorGUILayout.BeginHorizontal();
		
		if(GUILayout.Button("Add Item"))
		{
			var newWeapon = new Melee();
			newWeapon.id = MakeNewID(control.MeleeList);
			newWeapon.Type = ItemType.Melee;
			control.MeleeList.Add(newWeapon);
			ItemIndex = control.MeleeList.Count - 1;
		}
		
		if(GUILayout.Button("Remove Item"))
		{
			control.MeleeList.RemoveAt(ItemIndex);
			ItemIndex = control.MeleeList.Count - 1;
		}
		
		EditorGUILayout.EndHorizontal();
		

		
	}

	void DrawArmorList(ItemDataBase control)
	{
		if(control.ArmorList.Count > 0)
		{
			ItemIndex = EditorGUILayout.IntSlider("Item Index", ItemIndex, 0, control.ArmorList.Count - 1);
			NGUIEditorTools.DrawSeparator();

			CommonItemProps(control.ArmorList[ItemIndex]);

			control.ArmorList[ItemIndex].Category = (ArmorQuality)EditorGUILayout.EnumPopup("Category", control.ArmorList[ItemIndex].Category);
			control.ArmorList[ItemIndex].ArmorValue = EditorGUILayout.FloatField("Armor Value", control.ArmorList[ItemIndex].ArmorValue);
			control.ArmorList[ItemIndex].DefenseValue = EditorGUILayout.FloatField("Defense Value", control.ArmorList[ItemIndex].DefenseValue);
		}
		
		EditorGUILayout.BeginHorizontal();
		
		if(GUILayout.Button("Add Item"))
		{
			var newArmor = new Armor();
			newArmor.id = MakeNewID(control.ArmorList);
			newArmor.Type = ItemType.Armor;
			control.ArmorList.Add(newArmor);
			ItemIndex = control.ArmorList.Count - 1;
		}
		
		if(GUILayout.Button("Remove Item"))
		{
			control.ArmorList.RemoveAt(ItemIndex);
			ItemIndex = control.ArmorList.Count - 1;
		}
		
		EditorGUILayout.EndHorizontal();

	}

	void DrawMiscList(ItemDataBase control)
	{
		if(control.MiscList.Count > 0)
		{
			ItemIndex = EditorGUILayout.IntSlider("Item Index", ItemIndex, 0, control.MiscList.Count - 1);
			NGUIEditorTools.DrawSeparator();

			CommonItemProps(control.MiscList[ItemIndex]);
		}
				
		EditorGUILayout.BeginHorizontal();
		
		if(GUILayout.Button("Add Item"))
		{
			var newMisc = new Misc();
			newMisc.id = MakeNewID(control.MiscList);
			newMisc.Type = ItemType.Misc;
			control.MiscList.Add(newMisc);
			ItemIndex = control.MiscList.Count - 1;
		}
		
		if(GUILayout.Button("Remove Item"))
		{
			control.MiscList.RemoveAt(ItemIndex);
			ItemIndex = control.MiscList.Count - 1;
		}
		
		EditorGUILayout.EndHorizontal();
	}

	void DrawConsumableList(ItemDataBase control)
	{
		if(control.ConsumableList.Count > 0)
		{
			ItemIndex = EditorGUILayout.IntSlider("Item Index", ItemIndex, 0, control.ConsumableList.Count - 1);
			NGUIEditorTools.DrawSeparator();

			CommonItemProps(control.ConsumableList[ItemIndex]);
		}

		
		EditorGUILayout.BeginHorizontal();
		
		if(GUILayout.Button("Add Item"))
		{
			var newConsume = new Consumable();
			newConsume.id = MakeNewID(control.ConsumableList);
			newConsume.Type = ItemType.Consumable;
			control.ConsumableList.Add(newConsume);
			ItemIndex = control.ConsumableList.Count - 1;
		}
		
		if(GUILayout.Button("Remove Item"))
		{
			control.ConsumableList.RemoveAt(ItemIndex);
			ItemIndex = control.ConsumableList.Count - 1;
		}
		
		EditorGUILayout.EndHorizontal();
	
	}

	void DrawQuestList(ItemDataBase control)
	{

		if(control.QuestList.Count > 0)
		{
			ItemIndex = EditorGUILayout.IntSlider("Item Index", ItemIndex, 0, control.QuestList.Count - 1);
			NGUIEditorTools.DrawSeparator();

			CommonItemProps(control.QuestList[ItemIndex]);
		}
			
		EditorGUILayout.BeginHorizontal();
		
		if(GUILayout.Button("Add Item"))
		{
			var newQuest = new QuestItem();
			newQuest.id = MakeNewID(control.QuestList);
			newQuest.Type = ItemType.Quest;
			control.QuestList.Add(newQuest);
			ItemIndex = control.QuestList.Count - 1;
		}
		
		if(GUILayout.Button("Remove Item"))
		{
			control.QuestList.RemoveAt(ItemIndex);
			ItemIndex = control.QuestList.Count - 1;
		}
		
		EditorGUILayout.EndHorizontal();
	}

	void DrawGeneratorList(ItemDataBase control)
	{
		if(control.GeneratorList.Count > 0)
		{
			ItemIndex = EditorGUILayout.IntSlider("Item Index", ItemIndex, 0, control.GeneratorList.Count - 1);
			NGUIEditorTools.DrawSeparator();

			CommonItemProps(control.GeneratorList[ItemIndex]);
		}
		
		EditorGUILayout.BeginHorizontal();
		
		if(GUILayout.Button("Add Item"))
		{
			var newGen = new Generator();
			newGen.id = MakeNewID(control.GeneratorList);
			newGen.Type = ItemType.Generator;
			control.GeneratorList.Add(newGen);
			ItemIndex = control.GeneratorList.Count - 1;
		}
		
		if(GUILayout.Button("Remove Item"))
		{
			control.GeneratorList.RemoveAt(ItemIndex);
			ItemIndex = control.GeneratorList.Count - 1;
		}
		
		EditorGUILayout.EndHorizontal();
	}

	void DrawEnhancerList(ItemDataBase control)
	{

		if(control.EnhancerList.Count > 0)
		{
			ItemIndex = EditorGUILayout.IntSlider("Item Index", ItemIndex, 0, control.EnhancerList.Count - 1);
			NGUIEditorTools.DrawSeparator();

			CommonItemProps(control.EnhancerList[ItemIndex]);
		}
		
		EditorGUILayout.BeginHorizontal();
		
		if(GUILayout.Button("Add Item"))
		{
			var newEnhancer = new Enhancer();
			newEnhancer.id = MakeNewID(control.EnhancerList);
			newEnhancer.Type = ItemType.Enhancer;
			control.EnhancerList.Add(newEnhancer);
			ItemIndex = control.EnhancerList.Count - 1;
		}
		
		if(GUILayout.Button("Remove Item"))
		{
			control.EnhancerList.RemoveAt(ItemIndex);
			ItemIndex = control.EnhancerList.Count - 1;
		}
		
		EditorGUILayout.EndHorizontal();
	}

	int MakeNewID <T>(List<T> itemList) where T : Item
	{
		if(itemList.Count < 1){ return 0;}

		int lastid = itemList[itemList.Count - 1].id;
		int newid = lastid + 1;
		return newid;
	}

}
