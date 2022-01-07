package ahbsd.numbersystems.library;

/**
 * Interface for a numeric base.
 */
public interface IBase {
    /**
     * Gets the numeric base.
     *
     * @return the numeric base
     */
    int getNumericBase();

    /**
     * Gets the known name
     *
     * @return the known name
     */
    String getKnownName();

    /**
     * Gets the maximum Sign for the current base.
     * @return the maximum Sign for the current base
     */
    String getMaxSign();
}
