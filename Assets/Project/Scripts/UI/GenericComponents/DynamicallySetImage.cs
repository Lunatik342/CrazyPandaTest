using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace RedPanda.Project.UI.GenericComponents
{
    //Тут конечно видно как изображение подгружается, подождать бы подгрузки перед открытием
    [RequireComponent(typeof(Image))]
    public class DynamicallySetImage: MonoBehaviour
    {
        private AsyncOperationHandle<Sprite> _handle;
        private Coroutine _spriteLoadingCoroutine;
        
        public void SetSprite(string key)
        {
            UnloadCurrentSprite();
            _handle = Addressables.LoadAssetAsync<Sprite>(key);
            _spriteLoadingCoroutine = StartCoroutine(LoadSprite(_handle));
        }

        private IEnumerator LoadSprite(AsyncOperationHandle<Sprite> asyncOperationHandle)
        {
            yield return asyncOperationHandle;
            
            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                var image = GetComponent<Image>();
                image.sprite = asyncOperationHandle.Result;
                image.color = Color.white;
            }
        }

        private void OnDestroy()
        {
            UnloadCurrentSprite();
        }

        private void UnloadCurrentSprite()
        {
            if (_spriteLoadingCoroutine != null)
            {
                StopCoroutine(_spriteLoadingCoroutine);
            }

            if (_handle.IsValid())
            {
                Addressables.Release(_handle);
            }
        }
    }
}