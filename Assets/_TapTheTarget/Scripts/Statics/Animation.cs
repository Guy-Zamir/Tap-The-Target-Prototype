using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class Animation
{
    public async static void Scale(GameObject gameObject, Vector3 sizeFrom, Vector3 sizeTo, float duration)
    {
        float timePassed = 0f;
        gameObject.transform.localScale = sizeFrom;

        while (timePassed < duration)
        {
            float t = timePassed / duration;
            gameObject.transform.localScale = Vector3.Lerp(sizeFrom, sizeTo, t);
            timePassed += Time.deltaTime;
            await Task.Yield();
        }

        gameObject.transform.localScale = sizeTo;
    }
}
