using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;



public class ItemDataBase : MonoBehaviour {

	public List<LaserWeapon> LaserWeaponList;
	public List<LegacyWeapon> LegacyWeaponList;
	public List<MagneticWeapon> MagneticWeaponList;
	public List<Melee> MeleeList;
	public List<Armor> ArmorList;
	public List<Misc> MiscList;
	public List<Consumable> ConsumableList;
	public List<QuestItem> QuestList;
	public List<Generator> GeneratorList;
	public List<Enhancer> EnhancerList;


	public Item Get(ItemType itemType, int index)
	{
		int n;

		switch(itemType)
		{
		case ItemType.LaserWeapon:
				n = GetIndexFromItemID(LaserWeaponList,index);
			return LaserWeaponList[n].Clone();
		case ItemType.LegacyWeapon:
				n = GetIndexFromItemID(LaserWeaponList,index);
			return LegacyWeaponList[n].Clone();
		case ItemType.MagneticWeapon:
				n = GetIndexFromItemID(LaserWeaponList,index);
			return MagneticWeaponList[n].Clone();
		case ItemType.Melee:
				n = GetIndexFromItemID(LaserWeaponList,index);
			return MeleeList[n].Clone();
		case ItemType.Armor:
				n = GetIndexFromItemID(LaserWeaponList,index);
			return ArmorList[n].Clone();
		case ItemType.Misc:
				n = GetIndexFromItemID(LaserWeaponList,index);
			return MiscList[n].Clone();
		case ItemType.Consumable:
				n = GetIndexFromItemID(LaserWeaponList,index);
			return ConsumableList[n].Clone();
		case ItemType.Quest:
				n = GetIndexFromItemID(LaserWeaponList,index);
			return QuestList[n].Clone();
		case ItemType.Enhancer:
				n = GetIndexFromItemID(LaserWeaponList,index);
			return EnhancerList[n].Clone();
		case ItemType.Generator:
				n = GetIndexFromItemID(LaserWeaponList,index);
			return GeneratorList[n].Clone ();
		default:
			return null;
		}

	}

	int GetIndexFromItemID <T>(List<T> list, int index) where T : Item
	{
		for(int a = 0; a < list.Count; a++)
		{
			if(list[a].id == index)
			{
				return a;
			}
		}

		Debug.LogError("This item has been removed please update your database for item id :" + index + " on list type " + list);

		return 0;
	}
	
	public void Save(string path, ItemType itemType)
	{

		TextWriter WriteFileStream = new StreamWriter(path);
		XmlSerializer serializer;

		switch(itemType)
		{
		case ItemType.Armor:

			serializer = new XmlSerializer(typeof(List<Armor>));
			serializer.Serialize(WriteFileStream,ArmorList);
			WriteFileStream.Close ();

			break;
		case ItemType.LaserWeapon:

			serializer = new XmlSerializer(typeof(List<LaserWeapon>));
			serializer.Serialize(WriteFileStream,LaserWeaponList);
			WriteFileStream.Close ();

			break;
		case ItemType.MagneticWeapon:
			
			serializer = new XmlSerializer(typeof(List<MagneticWeapon>));
			serializer.Serialize(WriteFileStream,MagneticWeaponList);
			WriteFileStream.Close ();
			
			break;
		case ItemType.Melee:
				
			serializer = new XmlSerializer(typeof(List<Melee>));
			serializer.Serialize(WriteFileStream,MeleeList);
			WriteFileStream.Close ();
			
			break;
		case ItemType.LegacyWeapon:
				
			serializer = new XmlSerializer(typeof(List<LegacyWeapon>));
			serializer.Serialize(WriteFileStream,LegacyWeaponList);
			WriteFileStream.Close ();
			
			break;

		case ItemType.Misc:

			serializer = new XmlSerializer(typeof(List<Misc>));
			serializer.Serialize(WriteFileStream, MiscList);
			WriteFileStream.Close();

			break;

		case ItemType.Consumable:
			serializer = new XmlSerializer(typeof(List<Consumable>));
			serializer.Serialize(WriteFileStream, ConsumableList);
			WriteFileStream.Close();
			
			break;
		case ItemType.Quest:
			serializer = new XmlSerializer(typeof(List<QuestItem>));
			serializer.Serialize(WriteFileStream, QuestList);
			WriteFileStream.Close();
			
			break;
		case ItemType.Enhancer:
			serializer = new XmlSerializer(typeof(List<Enhancer>));
			serializer.Serialize(WriteFileStream, EnhancerList);
			WriteFileStream.Close();
			
			break;

		case ItemType.Generator:
			serializer = new XmlSerializer(typeof(List<Generator>));
			serializer.Serialize(WriteFileStream, GeneratorList);
			WriteFileStream.Close();
		
		break;
		}
	}

	public void LoadXML(string path, ItemType itemType)
	{
		XmlSerializer serializer;

		switch(itemType)
		{
		case ItemType.LaserWeapon:
			
			serializer = new XmlSerializer(typeof(List<LaserWeapon>));
			using(var stream = new FileStream(path, FileMode.Open))
			{
				LaserWeaponList = serializer.Deserialize(stream) as List<LaserWeapon>;
			}
			break;
		case ItemType.LegacyWeapon:
			
			serializer = new XmlSerializer(typeof(List<LegacyWeapon>));
			using(var stream = new FileStream(path, FileMode.Open))
			{
				LegacyWeaponList = serializer.Deserialize(stream) as List<LegacyWeapon>;
			}
			break;
		case ItemType.Melee:
			
			serializer = new XmlSerializer(typeof(List<Melee>));
			using(var stream = new FileStream(path, FileMode.Open))
			{
				MeleeList = serializer.Deserialize(stream) as List<Melee>;
			}
			break;
		case ItemType.MagneticWeapon:
			
			serializer = new XmlSerializer(typeof(List<MagneticWeapon>));
			using(var stream = new FileStream(path, FileMode.Open))
			{
				MagneticWeaponList = serializer.Deserialize(stream) as List<MagneticWeapon>;
			}
			break;
		case ItemType.Armor:
			
			serializer = new XmlSerializer(typeof(List<Armor>));
			using(var stream = new FileStream(path, FileMode.Open))
			{
				ArmorList = serializer.Deserialize(stream) as List<Armor>;
			}
			break;
		case ItemType.Misc:
			
			serializer = new XmlSerializer(typeof(List<Misc>));
			using(var stream = new FileStream(path, FileMode.Open))
			{
				MiscList = serializer.Deserialize(stream) as List<Misc>;
			}
			break;
		case ItemType.Consumable:
			
			serializer = new XmlSerializer(typeof(List<Consumable>));
			using(var stream = new FileStream(path, FileMode.Open))
			{
				ConsumableList = serializer.Deserialize(stream) as List<Consumable>;
			}
			break;
		case ItemType.Quest:

			serializer = new XmlSerializer(typeof(List<QuestItem>));
			using(var stream = new FileStream(path, FileMode.Open))
			{
				QuestList = serializer.Deserialize(stream) as List<QuestItem>;
			}
			break;
		case ItemType.Enhancer:
				
			serializer = new XmlSerializer(typeof(List<Enhancer>));
			using(var stream = new FileStream(path, FileMode.Open))
			{
				EnhancerList = serializer.Deserialize(stream) as List<Enhancer>;
			}
			break;
			case ItemType.Generator:
				
				serializer = new XmlSerializer(typeof(List<Generator>));
				using(var stream = new FileStream(path, FileMode.Open))
				{
					GeneratorList = serializer.Deserialize(stream) as List<Generator>;
				}
				break;
		}
	}
	
}
