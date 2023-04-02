using FlowerDeer.Utility;
using System;
using System.Collections;
using UnityEngine;

namespace FlowerDeer.Manager
{
    public class TimerManager : Singleton<TimerManager>
    {
        public Coroutine StartTimer(float time, Action onCompleteEvent)
        {
            return StartCoroutine(Timer(time, onCompleteEvent));
        }

        public void CancleTimer(Coroutine timer)
        {
            StopCoroutine(timer);
        }

        private IEnumerator Timer(float time, Action onCompleteEvent)
        {
            while (time > 0)
            {
                time -= Time.deltaTime;
                yield return null;
            }
            onCompleteEvent?.Invoke();
            yield return null;
        }
    }
}
