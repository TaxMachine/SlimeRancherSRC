// Decompiled with JetBrains decompiler
// Type: SECTRDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class SECTRDirector : MonoBehaviour
{
  private const int MIN_LOCAL_ARRAY_RESIZE_AMOUNT = 50;
  private List<SECTR_Member> _allMembers = new List<SECTR_Member>();
  private List<SECTR_Member> _offsetUpdates = new List<SECTR_Member>();
  private List<SECTR_Member> _nonOffsetUpdates = new List<SECTR_Member>();
  private ExposedArrayList<SECTR_Hibernator> _hibernatorsToUpdate = new ExposedArrayList<SECTR_Hibernator>(1000);
  private List<SECTR_Member> _toRegister = new List<SECTR_Member>();
  private SECTR_Hibernator[] Update_localHibernators = new SECTR_Hibernator[50];

  public void RegisterMember(SECTR_Member member)
  {
    RemoveMember(member);
    _allMembers.Add(member);
    if (member.BoundsUpdateMode == SECTR_Member.BoundsUpdateModes.Static)
      return;
    if (member.BoundsUpdateMode == SECTR_Member.BoundsUpdateModes.Offset)
      _offsetUpdates.Add(member);
    else
      _nonOffsetUpdates.Add(member);
  }

  public void DeregisterMember(SECTR_Member member)
  {
    _allMembers.Remove(member);
    if (member.BoundsUpdateMode == SECTR_Member.BoundsUpdateModes.Static)
      return;
    if (member.BoundsUpdateMode == SECTR_Member.BoundsUpdateModes.Offset)
      _offsetUpdates.Remove(member);
    else
      _nonOffsetUpdates.Remove(member);
  }

  private void RemoveMember(SECTR_Member member)
  {
    _allMembers.Remove(member);
    _nonOffsetUpdates.Remove(member);
    _offsetUpdates.Remove(member);
  }

  public void RegisterHibernator(SECTR_Hibernator hibernator) => _hibernatorsToUpdate.Add(hibernator);

  public void DeregisterHibernator(SECTR_Hibernator hibernator) => _hibernatorsToUpdate.Remove(hibernator);

  public void ClearRegistrations()
  {
    _allMembers.Clear();
    _nonOffsetUpdates.Clear();
    _offsetUpdates.Clear();
    _hibernatorsToUpdate.Clear();
  }

  private void Start() => Log.Debug("Starting SECTRDirector");

  private void Update()
  {
    if (_hibernatorsToUpdate.Data.Length > Update_localHibernators.Length)
      Array.Resize(ref Update_localHibernators, Math.Max(_hibernatorsToUpdate.Data.Length, 50));
    int count = _hibernatorsToUpdate.GetCount();
    _hibernatorsToUpdate.Data.CopyTo(Update_localHibernators, 0);
    for (int index = 0; index < count; ++index)
    {
      try
      {
        Update_localHibernators[index].OnUpdate();
      }
      catch (NullReferenceException ex)
      {
        Log.Debug("Null reference caught in SECTRDirector update.", "position", index);
      }
    }
  }

  private void LateUpdate()
  {
    for (int index = 0; index < _offsetUpdates.Count; ++index)
    {
      SECTR_Member offsetUpdate = _offsetUpdates[index];
      if (offsetUpdate.enabled && !offsetUpdate.IsHibernating && (offsetUpdate.BoundsUpdateMode == SECTR_Member.BoundsUpdateModes.Always || offsetUpdate.memberTransform.hasChanged))
      {
        if (offsetUpdate.BoundsUpdateMode != SECTR_Member.BoundsUpdateModes.Static)
          offsetUpdate.OffsetLateUpdate();
        else
          _toRegister.Add(offsetUpdate);
      }
    }
    for (int index = 0; index < _nonOffsetUpdates.Count; ++index)
    {
      SECTR_Member nonOffsetUpdate = _nonOffsetUpdates[index];
      if (nonOffsetUpdate.enabled && !nonOffsetUpdate.IsHibernating && (nonOffsetUpdate.BoundsUpdateMode == SECTR_Member.BoundsUpdateModes.Always || nonOffsetUpdate.memberTransform.hasChanged))
      {
        if (nonOffsetUpdate.BoundsUpdateMode != SECTR_Member.BoundsUpdateModes.Static)
          nonOffsetUpdate.NonOffsetLateUpdate();
        else
          _toRegister.Add(nonOffsetUpdate);
      }
    }
    for (int index = 0; index < _toRegister.Count; ++index)
    {
      if (_toRegister[index] != null)
        RegisterMember(_toRegister[index]);
    }
    _toRegister.Clear();
  }
}
