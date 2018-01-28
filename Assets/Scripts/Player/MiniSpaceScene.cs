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
		if(idleAnimation != null)
		{
			StartCoroutine(PlayActionAnimation((float)actionAnimation.duration));
		}
		else
		{
			End();
		}
	}

	private IEnumerator PlayActionAnimation(float duration)
	{
		if(idleAnimation != null)
		{
			idleAnimation.Stop();
			actionAnimation.gameObject.SetActive(true);
		
			yield return new WaitForSeconds(duration);

			idleAnimation.Play();
			actionAnimation.gameObject.SetActive(false);
		}

		End();
	}

	private void End()
	{
		if(OnEndAnimation != null)
		{
			OnEndAnimation.Invoke();
		}
	}
}
