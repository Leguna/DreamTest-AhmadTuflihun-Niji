using System;
using EventStruct;
using UnityEngine;
using Utilities;

namespace PauseSystem
{
    public class PauseManager : SingletonMonoBehaviour<PauseManager>
    {
        [SerializeField] private bool showUiDebug;
            
        private void OnGUI()
        {
            if (!showUiDebug) return;
            if (GUI.Button(new Rect(10, 10, 150, 100), "Pause"))
            {
                Pause();
            }

            if (GUI.Button(new Rect(10, 120, 150, 100), "Resume"))
            {
                Resume();
            }
        }

        public void Pause()
        {
            Time.timeScale = 0;
            EventManager.TriggerEvent(new PauseResumeEventData(isPause: true));
        }

        public void Resume()
        {
            Time.timeScale = 1;
            EventManager.TriggerEvent(new PauseResumeEventData(isPause: false));
        }
    }
}