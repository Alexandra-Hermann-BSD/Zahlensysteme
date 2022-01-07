package ahbsd.numbersystems.library;

import org.jetbrains.annotations.Contract;

/**
 * The class for a numeric base.
 */
public class Base implements IBase {
    /**
     * The numeric base as int.
     */
    private final int numericBase;

    /**
     * The known name of the numeric base.
     */
    private final String knownName;

    /**
     * Constructor without parameters.
     * The numeric base is 10.
     */
    @Contract(pure = true)
    public Base() {
        this(10);
    }

    /**
     * Constructor with a given numeric base.
     * @param base the given numeric base
     */
    @Contract(pure = true)
    public Base(int base) {
        numericBase = base;
        knownName = getKnownNameByNumericBase(base);
    }

    /**
     * Gets a known name by a given numeric base.
     * @param base the given numeric base
     * @return the known name; if there is no known name, the result will be ""
     */
    @Contract(pure = true)
    public static String getKnownNameByNumericBase(int base) {

        String result;

        switch (base) {
            case 2:
                result = "Binär";
                break;

            case 3:
                result = "Ternär";
                break;

            case 4:
                result = "Quaternär";
                break;

            case 5:
                result = "Quinär";
                break;

            case 6:
                result = "Senär";
                break;

            case 7:
                result = "Septenär";
                break;

            case 8:
                result = "Oktal";
                break;

            case 9:
                result = "Nonär";
                break;

            case 10:
                result = "Dezimal";
                break;

            case 11:
                result = "Undezimal";
                break;

            case 12:
                result = "Duodezimal";
                break;

            case 13:
                result = "Tridezimal";
                break;

            case 14:
                result = "Tetradezimal";
                break;

            case 15:
                result = "Pentadezimal";
                break;

            case 16:
                result = "Hexadezimal";
                break;

            case 18:
                result = "Oktodezimal";
                break;

            case 20:
                result = "Vigesimal";
                break;

            case 24:
                result = "Quadrovigesimal";
                break;

            case 28:
                result = "Oktovigesimal";
                break;

            case 30:
                result = "Trigesimal";
                break;

            case 36:
                result = "Hexatridezimal";
                break;

            default:
                result = "";
                break;
        }

        return result;
    }

    /**
     * Gets the numeric base.
     *
     * @return the numeric base
     */
    @Override
    public int getNumericBase() {
        return numericBase;
    }

    /**
     * Gets the known name
     *
     * @return the known name
     */
    @Override
    public String getKnownName() {
        return knownName;
    }

    /**
     * Gets the maximum Sign for the current base.
     * @return the maximum Sign for the current base
     */
    @Override
    public String getMaxSign() {
        char c = '0';

        if (numericBase > 9) {
            c = (char)(numericBase + 65);
        } else if (numericBase >= 2) {
            c = (char)(numericBase + 48);
        }

        return String.valueOf(c);
    }

}
