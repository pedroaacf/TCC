using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBinLift : MonoBehaviour
{
    public GameObject[] trashBins;
    public float targetHeight;
    public float speed = 2f; 
    public float delayBetweenTrashBins = 1f; 
    public AudioSource elevationCompleteSound;
    void Update()
    {
		StartElevatingTrashBins();
	}
    
    public void StartElevatingTrashBins()
    {
        StartCoroutine(ElevateTrashBinsInOrder());
    }

    private IEnumerator ElevateTrashBinsInOrder()
    {
        foreach (var trashBin in trashBins)
        {
            yield return new WaitForSeconds(delayBetweenTrashBins);
            yield return StartCoroutine(ElevateTrashBin(trashBin));
            //yield return new WaitForSeconds(delayBetweenTrashBins);
        }
    }

    private IEnumerator ElevateTrashBin(GameObject trashBin)
    {
        Vector3 startPos = trashBin.transform.position;
        Vector3 endPos = new Vector3(startPos.x, targetHeight, startPos.z);
        
        while (trashBin.transform.position.y < targetHeight)
        {
            trashBin.transform.position = Vector3.MoveTowards(trashBin.transform.position, endPos, speed * Time.deltaTime);
            
            elevationCompleteSound.Play();
            
            yield return null;
        }
    }
}
