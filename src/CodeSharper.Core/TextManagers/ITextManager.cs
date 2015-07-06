using System;

namespace CodeSharper.Core.TextManagers
{
    public interface ITextManager
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        String GetValue(TextMarkerBase marker);

        /// <summary>
        /// Sets the value.
        /// </summary>
        void SetValue(TextMarkerBase marker, String value);

        /// <summary>
        /// Creates the marker.
        /// </summary>
        TextMarkerBase CreateMarker(Int32 start, Int32 exclusiveStop);

        /// <summary>
        /// Removes the marker.
        /// </summary>
        Boolean RemoveMarker(TextMarkerBase marker);
    }
}