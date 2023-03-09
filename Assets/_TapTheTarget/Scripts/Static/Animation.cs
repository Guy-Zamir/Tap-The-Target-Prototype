using System.Collections;
using UnityEngine;

public class Animation
{
    public static IEnumerator Scale(GameObject gameObject, Vector3 sizeFrom, Vector3 sizeTo, float duration)
    {
        float timePassed = 0f;
        gameObject.transform.localScale = sizeFrom;

        while (timePassed < duration)
        {
            float t = timePassed / duration;
            gameObject.transform.localScale = Vector3.Lerp(sizeFrom, sizeTo, t);
            timePassed += Time.deltaTime;
            yield return null;
        }

        gameObject.transform.localScale = sizeTo;
    }
}
