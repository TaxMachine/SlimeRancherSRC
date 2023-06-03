// Decompiled with JetBrains decompiler
// Type: SkyTopLimit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

public class SkyTopLimit : SRSingleton<SkyTopLimit>
{
  private const float GRAV_PER_Y = 0.04f;
  private float bottomY;

  public override void Awake()
  {
    base.Awake();
    bottomY = transform.position.y - 0.5f * transform.localScale.y;
  }

  public float DownwardExtraGravity(float y)
  {
    float num = y - bottomY;
    return num <= 0.0 ? 0.0f : num * 0.04f;
  }
}
