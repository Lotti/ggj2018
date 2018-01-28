using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

public class MiniSpaceScene : MonoBehaviour {

	public PlayableDirector idleAnimation, actionAnimation;
	public UnityAction OnEndAnimation;

	public void PlayActionAnimation()
	{
		StartCoroutine(PlayActionAnimation((float)actionAnimation.duration));
	}

	private IEnumerator PlayActionAnimation(float duration)
	{
		idleAnimation.Stop();
		actionAnimation.Play();

		yield return new WaitForSeconds(duration);

		idleAnimation.Play();

		if(OnEndAnimation != null)
		{
			OnEndAnimation.Invoke();
		}
	}
}
