using System;
using Android.Speech.Tts;
using Xamarin.Forms;

using Todo.Contracts;
using Todo.Droid.Contracts;
using System.Collections.Generic;

[assembly: Xamarin.Forms.Dependency(typeof(DroidSpeech))]
namespace Todo.Droid.Contracts
{
	public class DroidSpeech : Java.Lang.Object, ITextToSpeech, TextToSpeech.IOnInitListener
	{
		private TextToSpeech speaker;
		private string toSpeak;

		public DroidSpeech()
		{
		}

		public void Speak(string text)
		{
			var c = Forms.Context;
			toSpeak = text;
			if (speaker == null)
				speaker = new TextToSpeech(c, this);
			else
			{
				var p = new Dictionary<string, string>();
				speaker.Speak(toSpeak, QueueMode.Flush, p);
			}
		}

		public void OnInit(OperationResult status)
		{
			if (status.Equals(OperationResult.Success))
			{
				System.Diagnostics.Debug.WriteLine("spoke");
				var p = new Dictionary<string, string>();
				speaker.Speak(toSpeak, QueueMode.Flush, p);
			}
			else {
				System.Diagnostics.Debug.WriteLine("was quiet");
			}
		}
	}
}

