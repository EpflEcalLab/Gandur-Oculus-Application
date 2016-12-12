
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;

	[ExecuteInEditMode]

public class TriggerParticule : MonoBehaviour {
		ParticleSystem ps;

		// these lists are used to contain the particles which match
		// the trigger conditions each frame.
		List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
		List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

		void OnEnable()
		{
		ps = this.gameObject.GetComponent<ParticleSystem>();
		}

		void OnParticleTrigger()
		{
			// get the particles which matched the trigger conditions this frame
			int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
			int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

			// iterate through the particles which entered the trigger and make them red
			for (int i = 0; i < numEnter; i++)
			{
				ParticleSystem.Particle p = enter[i];
			p.startColor = new Color32(255, 255, 255, 255);
				enter[i] = p;
			}

			// iterate through the particles which exited the trigger and make them green
			for (int i = 0; i < numExit; i++)
			{
				ParticleSystem.Particle p = exit[i];
			p.startColor = new Color32(255, 255, 255, 0);
				exit[i] = p;
			}

			// re-assign the modified particles back into the particle system
			ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
			ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
		}
	}
