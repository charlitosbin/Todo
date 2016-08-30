using System;
using AVFoundation;

using Xamarin.Forms;

using Todo.Contracts;
using Todo.iOS.Contracts;

[assembly: Dependency(typeof(IOSSpeech))]

namespace Todo.iOS.Contracts
{
	public class IOSSpeech : ITextToSpeech
	{
		private float volume = 0.5f;
		private float pitch = 1.0f;

		public IOSSpeech()
		{ 
		}

		public void Speak(string text)
		{
			var speechSynthesizer = new AVSpeechSynthesizer();
			var speechUTterance = new AVSpeechUtterance(text){
				Rate = AVSpeechUtterance.MaximumSpeechRate / 4,
				Voice = AVSpeechSynthesisVoice.FromLanguage("en-US"),
				Volume = volume,
				PitchMultiplier = pitch
			};

			speechSynthesizer.SpeakUtterance(speechUTterance);
		}
	}
}