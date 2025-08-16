using ScenesHelpers;
using ScenesHelpers.Editor;
using UnityEngine;

namespace UI
{
    public class TYforplaying : MonoBehaviour
    {
        [SerializeField] private SceneRequestChannel sceneRequestChannel;

        [SerializeField] private SceneField menuScene;
        [SerializeField] private SceneField gameScene;
        
        public void GoToMenu()
        {
            sceneRequestChannel.RequestSceneChange(menuScene);
        }
        public void GoToGame()
        {
            sceneRequestChannel.RequestSceneChange(gameScene);
        }
    }
}