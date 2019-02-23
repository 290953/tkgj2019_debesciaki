using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicOrb : MonoBehaviour
{
    public float CurrentScale = 0.1f;

    public Rigidbody OrbRigidbody;

    private Coroutine scaleBounceCoroutine;
    private Vector3 startPos; 
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        scaleBounceCoroutine = StartCoroutine(ScaleBounce(transform, Mathf.Min(CurrentScale + 0.05f, 0.6f), 1f));

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CurrentScale += 0.07f;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            StopCoroutine(scaleBounceCoroutine);
            OrbRigidbody.constraints = RigidbodyConstraints.None;
            OrbRigidbody.AddForce(new Vector3(Random.Range(-500,500), 40, Random.Range(-500,500)));
        }
    }
    
    public IEnumerator ScaleBounce(Transform target, float targetScale, float time)
    {
        float lTime = 0;
        //transform.position = new Vector3(startPos.x, startPos.y / CurrentScale, startPos.z);
        //float initScale = target.localScale.x;//, cScale = target.localScale.x;
        do
        {
            yield return new WaitForEndOfFrame();
            lTime = Mathf.Min(time / 2, lTime + Time.deltaTime);
            target.localScale = Vector3.one * Mathf.Lerp(CurrentScale, targetScale * 1.1f, lTime / (time / 2));
        } while (lTime < time / 2);

        lTime = 0;
        do
        {
            yield return new WaitForEndOfFrame();
            lTime = Mathf.Min(time / 2, lTime + Time.deltaTime);
            target.localScale = Vector3.one * Mathf.Lerp(transform.localScale.x, CurrentScale, lTime / (time / 2));
        } while (lTime < time / 2);

        scaleBounceCoroutine = StartCoroutine(ScaleBounce(transform, Mathf.Min(CurrentScale + 0.05f, 1.2f), 1f));
    }

    private void Reset()
    {
        transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        transform.position = startPos;
        OrbRigidbody.constraints = RigidbodyConstraints.FreezePosition;
        OrbRigidbody.velocity = Vector3.zero;
        CurrentScale = 0.1f;
        scaleBounceCoroutine = StartCoroutine(ScaleBounce(transform, Mathf.Min(CurrentScale + 0.05f, 1.2f), 1f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            Reset();
        }
    }
}
