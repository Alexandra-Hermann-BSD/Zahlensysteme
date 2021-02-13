using System;

namespace Zahlensysteme
{
	/// <summary>
	/// Interface einer Zahlenbasis: Grundlagen.
	/// </summary>
	public interface IBase
	{
		/// <summary>
		/// Der Name der Zahlenbasis.
		/// </summary>
		/// <value>Der Name der Zahlenbasis.</value>
		string Name { get; }
		/// <summary>
		/// Die Anzahl der möglichen Zeichen.
		/// </summary>
		/// <value>Das Zahlensystem.</value>
		uint System { get; }
        /// <summary>
        /// Das höchstmögliche Zeichen.
        /// </summary>
        /// <value>Die maximale Anzahl an Zeichen.</value>
        char MaxSign { get; }
        /// <summary>
        /// Gibt das entsprechende Zeichen für die übergebene Zahl zurück.
        /// </summary>
        /// <returns>Das Zeichen für die übergebene Zahl.</returns>
        /// <param name="number">Die zu übersetzende Zahl.</param>
        char GetSign (uint number);
		/// <summary>
		/// Gibt die Zahl des übergebenen Zeichens zurück.
		/// </summary>
		/// <returns>Die Zahl für das übergebene Zeichen.</returns>
		/// <param name="sign">Das zu übersetzende Zeichen.</param>
		uint GetNumber(char sign);
	}
}

