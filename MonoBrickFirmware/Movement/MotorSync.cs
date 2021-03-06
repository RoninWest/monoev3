using System;

namespace MonoBrickFirmware.Movement
{
	/// <summary>
	/// Class for synchronizing two motors
	/// </summary>
	public class MotorSync : MotorBase{
		
		private const int waitInitialSleep = 300;
		private const int waitPollTime = 50;

		/// <summary>
		/// Initializes a new instance of the <see cref="MonoBrickFirmware.IO.MotorSync"/> class.
		/// </summary>
		/// <param name="bitfield">Bitfield.</param>
		public MotorSync (OutputBitfield bitfield)
		{
			this.BitField = bitfield;
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="MonoBrickFirmware.IO.MotorSync"/> class.
		/// </summary>
		/// <param name="port1">Port1.</param>
		/// <param name="port2">Port2.</param>
		public MotorSync (MotorPort port1, MotorPort port2)
		{
			this.BitField = base.MotorPortToBitfield(port1) | base.MotorPortToBitfield(port2);
		}
		
		/// <summary>
		/// Gets or sets the motor bit field.
		/// </summary>
		/// <value>The bit field.</value>
		public new OutputBitfield BitField {
			get{return base.BitField;}
			set{base.BitField = value;}
		}
		
		/// <summary>
		/// Syncronise steps between two motors
		/// </summary>
		/// <param name="speed">Speed of the motors.</param>
		/// <param name="turnRatio">Turn ratio (-200 to 200).</param>
		/// <param name="steps">Steps to move.</param>
		/// <param name="brake">If set to <c>true</c> motors will brake when done otherwise off.</param>
		/// <param name='waitForCompletion'>
		/// Set to <c>true</c> to wait for movement to be completed before returning
		/// </param>
		public void StepSync(sbyte speed, Int16 turnRatio, UInt32 steps, bool brake, bool waitForCompletion = true){
			output.SetStepSync(speed, turnRatio, steps, brake);
			if(waitForCompletion)
				WaitForMotorToStop(waitPollTime);
		}
		
		/// <summary>
		/// Syncronise time between two motors
		/// </summary>
		/// <param name="speed">Speed of the motors.</param>
		/// <param name="turnRatio">Turn ratio (-200 to 200).</param>
		/// <param name="timeInMs">Time in ms to move.</param>
		/// <param name="brake">If set to <c>true</c> motors will brake when done otherwise off.</param>
		/// <param name='waitForCompletion'>
		/// Set to <c>true</c> to wait for movement to be completed before returning
		/// </param>
		public void TimeSync(sbyte speed, Int16 turnRatio, UInt32 timeInMs, bool brake, bool waitForCompletion = true){
			output.SetTimeSync(speed, turnRatio, timeInMs, brake);
			if(waitForCompletion)
				WaitForMotorToStop(waitPollTime);
		}
		
		/// <summary>
		/// Move both motors with the same speed
		/// </summary>
		/// <param name="speed">Speed of the motors.</param>
		/// <param name="turnRatio">Turn ratio (-200 to 200).</param>
		public void On(sbyte speed, Int16 turnRatio){
			On(speed, turnRatio, 0, false);
		}
		
		/// <summary>
		/// Move both motors with the same speed a given number of steps
		/// </summary>
		/// <param name="speed">Speed of the motors.</param>
		/// <param name="turnRatio">Turn ratio (-200 to 200).</param>
		/// <param name="degrees">Degrees to move.</param>
		/// <param name="brake">If set to <c>true</c> motors will brake when done otherwise off.</param>
		/// <param name='waitForCompletion'>
		/// Set to <c>true</c> to wait for movement to be completed before returning
		/// </param>
		public void On (sbyte speed, Int16 turnRatio, uint degrees, bool brake, bool waitForCompletion = true){
			StepSync(speed, turnRatio , degrees, brake, waitForCompletion);
			if(waitForCompletion)
				WaitForMotorToStop(waitPollTime);
		}
	}
}

