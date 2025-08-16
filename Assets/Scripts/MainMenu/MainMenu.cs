using ScenesHelpers;
using ScenesHelpers.Editor;
using UnityEngine;

namespace MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private SceneRequestChannel sceneRequestChannel;

        [SerializeField] private SceneField gameScene;
        public void SwapToMainGame()
        {
            sceneRequestChannel.RequestSceneChange(gameScene);    
        }
        
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
