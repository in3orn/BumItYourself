using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Krk.Bum.View.Animations
{
    public static class AnimationExtensions
    {
        public static void DOAnchorPos(this RectTransform item, MoveAnimationConfig config)
        {
            item.DOAnchorPos(item.anchoredPosition + config.Offset, config.Duration);
        }

        public static void DOPunchScale(this RectTransform item, 
            PunchAnimationConfig config, PunchAnimationData data)
        {
            if (config.Duration > Mathf.Epsilon)
            {
                item.DOComplete();
                item.localScale = data.StartValue;
                item.DOPunchScale(config.Strength, config.Duration, config.Vibrato);
            }
        }

        public static void DOPunchRotation(this RectTransform item, 
            PunchAnimationConfig config, PunchAnimationData data)
        {
            if (config.Duration > Mathf.Epsilon)
            {
                item.DOComplete();
                item.localRotation = Quaternion.Euler(data.StartValue);
                item.DOPunchRotation(config.Strength, config.Duration, config.Vibrato);
            }
        }

        public static Sequence DOFade(this Image item, FadeAnimationConfig config, FadeColorData data)
        {
            var sequence = DOTween.Sequence();

            sequence.AppendCallback(() => item.gameObject.SetActive(true));

            if (config.FadeInDuration > Mathf.Epsilon)
            {
                sequence.Append(item.DOColor(data.TargetColor, config.FadeInDuration));
            }
            else
            {
                item.color = data.TargetColor;
            }

            if (config.FadeHoldDuration > Mathf.Epsilon)
            {
                sequence.AppendInterval(config.FadeHoldDuration);
            }

            sequence.Append(item.DOColor(data.ClearColor, config.FadeOutDuration));

            sequence.AppendCallback(() => item.gameObject.SetActive(false));

            return sequence;
        }

        public static Sequence DOFade(this TextMeshProUGUI item, FadeAnimationConfig config, FadeColorData data)
        {
            var sequence = DOTween.Sequence();

            sequence.AppendCallback(() => item.gameObject.SetActive(true));

            if (config.FadeInDuration > Mathf.Epsilon)
            {
                sequence.Append(item.DOColor(data.TargetColor, config.FadeInDuration));
            }
            else
            {
                item.color = data.TargetColor;
            }

            if (config.FadeHoldDuration > Mathf.Epsilon)
            {
                sequence.AppendInterval(config.FadeHoldDuration);
            }

            sequence.Append(item.DOColor(data.ClearColor, config.FadeOutDuration));

            sequence.AppendCallback(() => item.gameObject.SetActive(false));

            return sequence;
        }
    }
}
