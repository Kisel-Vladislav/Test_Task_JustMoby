using UnityEngine;

namespace _Project.Infrastructure
{
    public static class RectTransformExtensions
    {
        public static RectTransform Left(this RectTransform rt, float x)
        {
            rt.offsetMin = new Vector2(x, rt.offsetMin.y);
            return rt;
        }

        public static RectTransform Right(this RectTransform rt, float x)
        {
            rt.offsetMax = new Vector2(-x, rt.offsetMax.y);
            return rt;
        }

        public static RectTransform Bottom(this RectTransform rt, float y)
        {
            rt.offsetMin = new Vector2(rt.offsetMin.x, y);
            return rt;
        }

        public static float GetBottom(this RectTransform rt) =>
            rt.offsetMin.y;

        public static RectTransform Top(this RectTransform rt, float y)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x, -y);
            return rt;
        }

        public static RectTransform SetWidth(this RectTransform rt, float y)
        {
            rt.sizeDelta = new Vector2(y, rt.sizeDelta.x);
            return rt;
        }

        public static RectTransform AnchorToParent(this RectTransform rt)
        {
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = new Vector2(1, 1);
            return rt;
        }

        public static RectTransform AnchorToVerticalCenter(this RectTransform rt)
        {
            rt.anchorMin = new Vector2(0.5f, 0);
            rt.anchorMax = new Vector2(0.5f, 1);
            return rt;
        }

        public static RectTransform AnchorToBottomCenter(this RectTransform rt)
        {
            rt.anchorMin = new Vector2(0.5f, 0f);
            rt.anchorMax = new Vector2(0.5f, 0f);
            return rt;
        }

        public static RectTransform AnchorToMiddleCenterWithoutMoving(this RectTransform rt)
        {
            var worldPosition = rt.position;

            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);

            rt.position = worldPosition;

            return rt;
        }

        public static RectTransform ResetOffsets(this RectTransform rt)
        {
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
            return rt;
        }
    }
}