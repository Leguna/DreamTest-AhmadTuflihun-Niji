using System.Threading.Tasks;
using EventStruct;
using UnityEngine;
using Utilities;

namespace LoadingModule
{
#if UNITY_EDITOR
    public class AsyncTester : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                Load(1);
            else if (Input.GetKeyDown(KeyCode.Alpha2)) Load(5);
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 100, 50), "Load 1"))
                Load(1);
            else if (GUI.Button(new Rect(10, 70, 100, 50), "Load 5")) Load(5);
        }

        private void Load(int n)
        {
            for (var i = 0; i < n; i++)
            {
                var loadingMethod = LoadingMethod();
                var index = i;
                EventManager.TriggerEvent(new LoadingEventData(loadingMethod,
                    onComplete: () => Debug.Log($"Loaded {index}"),
                    message: $"Loading {index}"));
            }
        }

        private Task LoadingMethod()
        {
            return Task.Delay(Random.Range(1000, 5000));
        }
    }
#endif
}