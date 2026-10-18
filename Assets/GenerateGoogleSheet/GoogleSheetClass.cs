using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>You must approach through `GoogleSheetManager.SO<GoogleSheetSO>()`</summary>
public class GoogleSheetSO : ScriptableObject
{
	public List<EnemyData> EnemyDataList;
}

[Serializable]
public class EnemyData
{
	public int Id;
	public string Name;
	public int Health;
}

