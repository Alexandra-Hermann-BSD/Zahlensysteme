namespace Zahlensysteme
{
	/// <summary>
	/// Interface für ZahlensystemException.
	/// </summary>
	public interface IZahlensystemException
	{
		/// <summary>
		/// Gibt die auszuprobierende Basis zurück.
		/// </summary>
		/// <value>Die auszuprobierende Basis.</value>
		IBase TryBase { get; }
		/// <summary>
		/// Gibt das Zahlensystem als <see cref="uint"/> zurück.
		/// </summary>
		/// <value>Das Zahlensystem als uint.</value>
		/// <remarks>Gleichzusetzen mit <see cref="TryBase.System"/>.</remarks>
		uint System { get; }

		bool Equals (object obj);
		bool Equals (IZahlensystemException other);
		int GetHashCode ();
	}
}