using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Spine.AnimationState;
using System;

//帮助Unit处理spine动画播放的回调，帧事件的处理
public class UnitSpineAnimationHelper
{
    Unit unit;
    private SkeletonAnimation skeletonAnimation;
    private Dictionary<string, UnityEvent> keyFrameEventDict = new Dictionary<string, UnityEvent>();
    public UnitSpineAnimationHelper(GameObject obj)
    {
        skeletonAnimation = obj.GetComponent<SkeletonAnimation>();
        skeletonAnimation.AnimationState.Event += HandleAnimationStateEvent;
    }

    public void PlayAnimation(string animName, bool loop = false, Action callBack = null)
    {
        if (skeletonAnimation != null)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, animName, loop);
            if (callBack != null)
            {
                TrackEntryDelegate ac = null;
                ac = delegate
                {
                    callBack();
                    //删除回调，否则会堆积
                    skeletonAnimation.AnimationState.Complete -= ac;
                };
                skeletonAnimation.AnimationState.Complete += ac;
            }
        }
    }
    public Spine.Animation GetAnimation(string animName)
    {
        if (skeletonAnimation != null)
        {
            return skeletonAnimation.skeletonDataAsset.GetAnimationStateData().skeletonData.FindAnimation(animName);
        }
        return null;
    }

    public void HandleAnimationStateEvent(TrackEntry trackEntry, Spine.Event e)
    {
        if (keyFrameEventDict.TryGetValue(e.data.name, out UnityEvent ev))
        {
            ev?.Invoke();
        }
    }

    public void AddCustomEventHandler(string key, UnityAction handler)
    {
        if (keyFrameEventDict.TryGetValue(key, out UnityEvent ev))
        {
            ev.AddListener(handler);
        }
        else
        {
            var ue = new UnityEvent();
            ue.AddListener(handler);
            keyFrameEventDict.Add(key, ue);
        }
    }

    public void RemoveCustomEventHandler(string key, UnityAction handler = null)
    {
        if (keyFrameEventDict.TryGetValue(key, out UnityEvent ev))
        {
            if (handler == null)
            {
                ev.RemoveAllListeners();
            }
            else
            {
                ev.RemoveListener(handler);
            }
        }
    }
}
