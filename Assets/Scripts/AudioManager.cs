﻿using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public Sound[] sounds;

	// Use this for initialization
	void Awake ()
	{
		foreach (Sound s in sounds) {
			s.source = gameObject.AddComponent<AudioSource> ();
			s.source.clip = s.clip;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;

			if (PlayerPrefs.HasKey ("Volume")) {
				s.source.volume = PlayerPrefs.GetFloat ("Volume");

			} else {
				s.source.volume = s.volume;
			}
		}
	}

	void Start ()
	{
		Play ("Background");
	}

	public void SetVolume (float value)
	{
		foreach (Sound s in sounds) {
			s.source.volume = value;
		}
	}

	public void Play (string name)
	{
		Sound s = Array.Find (sounds, sound => sound.name == name);
		if (s == null) {
			print ("Sound " + name + " not found");
			return;
		}
		s.source.Play ();
	}

	public void Pause (string name)
	{
		Sound s = Array.Find (sounds, sound => sound.name == name);

		s.source.Pause ();
	}

	public void Stop (string name)
	{
		Sound s = Array.Find (sounds, sound => sound.name == name);

		s.source.Stop ();
	}

	public void PlayOnAwake (string name)
	{
		Sound s = Array.Find (sounds, sound => sound.name == name);
		if (s == null) {
			print ("Sound " + name + " not found");
			return;
		}
		if (!s.source.playOnAwake) {
			s.source.Play ();
		}
		;
	}
}