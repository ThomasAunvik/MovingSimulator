using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class SFX : MonoBehaviour
{
	EventInstance instance;
	public EventReference sfxEvent;

	FMOD.Studio.PARAMETER_ID keepParameterId, trashParameterId;



	void Start()
	{
		instance = FMODUnity.RuntimeManager.CreateInstance(sfxEvent);

		instance.getDescription(out EventDescription keepEventDescription);
		keepEventDescription.getParameterDescriptionByName(
			"keep",
			out PARAMETER_DESCRIPTION keepParameterDescription
		);
		keepParameterId = keepParameterDescription.id;

		instance.getDescription(out EventDescription trashEventDescription);
		trashEventDescription.getParameterDescriptionByName(
			"trash",
			out PARAMETER_DESCRIPTION trashParameterDescription
		);
		trashParameterId = trashParameterDescription.id;
	}


	public void SFXhit(float keepVal, float trashVal)
	{
		EventInstance sfx = RuntimeManager.CreateInstance(sfxEvent);
		sfx.setParameterByID(trashParameterId, trashVal);
		sfx.setParameterByID(keepParameterId, keepVal);
		instance.start();
		sfx.start();
		sfx.release();
	}

	void OnDestroy()
	{
		StopAllPlayerEvents();

		//--------------------------------------------------------------------
		// 6: This shows how to release resources when the unity object is 
		//    disabled.
		//--------------------------------------------------------------------
		instance.release();
	}

	void StopAllPlayerEvents()
	{
		Bus playerBus = RuntimeManager.GetBus("bus:/player");
		playerBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
	}
}
