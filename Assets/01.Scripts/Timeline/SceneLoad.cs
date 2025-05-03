using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JMT
{
    public class SceneLoad : MonoBehaviour
    {
        // 타임라인 종료를 알리는 bool 변수
        private bool endProcessing = false;

        void Update()
        {
            // endProcessing이 true로 설정되면 씬을 로드
            if (endProcessing)
            {
                StartCoroutine(DelayedLoad());
                endProcessing = false;  // 로드 후 flag 초기화
            }
        }

        public void EndProcessing()
        {
            endProcessing = true;
        }

        // 타임라인 끝나면 호출되는 메서드
        public void OnTimelineEnd()
        {
            endProcessing = true;
            Debug.Log("타임라인 종료, 씬 로딩 대기 중...");
        }

        private IEnumerator DelayedLoad()
        {
            Debug.Log("씬 비동기 로드 시작");

            // 비동기 씬 로드
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("KMH_Level_Update");
            asyncLoad.allowSceneActivation = false;

            // 씬 로딩이 90% 완료될 때까지 대기
            while (asyncLoad.progress < 0.9f)
            {
                Debug.Log($"로딩 중... {asyncLoad.progress * 100:0.0}%");
                yield return null;
            }

            // 씬 준비 완료, 잠시 대기
            Debug.Log("씬 준비 완료, 전환 시작");
            yield return new WaitForSeconds(2f);  // 이 부분은 조정 가능

            // 씬 활성화
            asyncLoad.allowSceneActivation = true;

            // 씬이 로드될 때까지 기다림
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            Debug.Log("씬 전환 완료!");
        }
    }
}
