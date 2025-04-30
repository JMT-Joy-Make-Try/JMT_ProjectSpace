using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace JMT
{
    public class SceneLoad : MonoBehaviour
    {
        public PlayableDirector director;

        void Start()
        {
            director = GetComponent<PlayableDirector>();
            director.stopped += OnTimelineStopped;
        }

        void OnTimelineStopped(PlayableDirector obj)
        {
            Debug.Log("Timeline 끝났음!");
            SceneManager.LoadScene("SampleScene");
        }
    }
}
