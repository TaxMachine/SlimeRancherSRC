// Decompiled with JetBrains decompiler
// Type: FirestormController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

public class FirestormController : SRBehaviour
{
  private FireColumn[] columns;

  public void Awake() => columns = GetComponentsInChildren<FireColumn>(true);

  public void AddColumnsToList(List<FireColumn> nearbyColumns) => nearbyColumns.AddRange(columns);
}
