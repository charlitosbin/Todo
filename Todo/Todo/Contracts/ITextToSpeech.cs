using System;

namespace Todo.Contracts
{
	public interface ITextToSpeech
	{
		void Speak(String text);
	}
}