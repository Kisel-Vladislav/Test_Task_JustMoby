using _Project.Infrastructure.Services.Localization;
using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Game.Logic.Message
{
    public class MessageDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _textMeshProUGUI;

        private ILocalizationService _localizationService;

        private Coroutine _currentCoroutine;
        private WaitForSeconds waitForTwoSeconds = new WaitForSeconds(2);

        [Inject]
        public void Construct(ILocalizationService localizationService) =>
            _localizationService = localizationService;

        public void Send(string messageKey)
        {
            var message = _localizationService.GetLocalizedString(messageKey);
            gameObject.SetActive(true);

            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

            _currentCoroutine = StartCoroutine(Show(message));
        }

        public void Ping(string messageKey)
        {
            Send(messageKey);
            _textMeshProUGUI.transform.DOScale(1.2f, 0.2f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        }

        private IEnumerator Show(string message)
        {
            _textMeshProUGUI.text = message;


            yield return waitForTwoSeconds;

            gameObject.SetActive(false);
        }
    }
}