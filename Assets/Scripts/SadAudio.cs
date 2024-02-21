using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class SadAudio : MonoBehaviour
{
	public EventReference PlayerStateEvent;

	private EventInstance instance;


	[Range(0, 1)]
	private float sadLevel;
	[Range(0, 1)]
	private float observingLevel;
	private static bool gameStarted = false;

	PARAMETER_ID sadParameterId, observingParameterId, startParameterId;

	private float lerpSad = 0f;
	private float lerpObs = 0f;

	public void Start()
	{
		instance = RuntimeManager.CreateInstance(PlayerStateEvent);
		instance.start();

		instance.getDescription(out EventDescription sadEventDescription);
		sadEventDescription.getParameterDescriptionByName("sad",
			out PARAMETER_DESCRIPTION sadParameterDescription
		);
		sadParameterId = sadParameterDescription.id;

		instance.getDescription(out EventDescription observingEventDescription);
		observingEventDescription.getParameterDescriptionByName("observing",
			out PARAMETER_DESCRIPTION observingParameterDescription
		);
		observingParameterId = observingParameterDescription.id;

		instance.getDescription(out EventDescription startEventDescription);
		startEventDescription.getParameterDescriptionByName("start",
			out PARAMETER_DESCRIPTION startParameterDescription
		);
		startParameterId = startParameterDescription.id;
	}
	// Update is called once per frame
	void Update()
	{
		lerpSad = Mathf.Lerp(lerpSad, sadLevel, 0.05f);
		lerpObs = Mathf.Lerp(lerpObs, observingLevel, 0.05f);


		instance.setParameterByID(sadParameterId, lerpSad);
		instance.setParameterByID(observingParameterId, lerpObs);
		if (gameStarted)
		{
			instance.setParameterByID(startParameterId, 0.5f);
		}
	}

	public static void StartGame()
	{
		gameStarted = true;
	}

	public void setLevels(float s, float o)
	{
		sadLevel = s;
		observingLevel = o;
	}

	void OnDestroy()
	{
		StopAllPlayerEvents();

		instance.release();
	}

	void StopAllPlayerEvents()
	{
		Bus playerBus = RuntimeManager.GetBus("bus:/player");
		playerBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
	}
}
