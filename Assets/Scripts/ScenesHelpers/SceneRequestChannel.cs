using ScenesHelpers.Editor;
using UnityEngine;
using UnityEngine.Events;
namespace ScenesHelpers
{
    [CreateAssetMenu(fileName = "SceneRequestChannel", menuName = "Utilities/Scenes/SceneRequestChannel")]
    public class SceneRequestChannel : ScriptableObject
    {
        public event UnityAction<SceneField> OnSceneRequested;
        
        public void RequestSceneChange(SceneField sceneField) => OnSceneRequested?.Invoke(sceneField);
    }
}