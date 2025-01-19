using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidSpawnPoint : MonoBehaviour {    
    private bool _onCoolDown;
    public bool Available {get => !_onCoolDown;}

    public void SetSpawnOnCoolDown(float coolDownTime){
        _onCoolDown = true;
        StartCoroutine(CoolDownTimer(coolDownTime));
    }

    private IEnumerator CoolDownTimer(float time){
        yield return new WaitForSeconds(time);
        _onCoolDown = false;
    }
}