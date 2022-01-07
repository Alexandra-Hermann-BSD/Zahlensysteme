package ahbsd.numbersystems.library;

public class BaseConverter {

    public static int fromBtoDecimal(int numericBase, String number) {
        int result = 0;
        char charW;
        int intW;
        int length = number.length();
        int counter = 0;
        int step = length - 1;

        while (step > 0) {
            charW = number.charAt(step);

            if (Character.isDigit(charW)) {
                intW = (int)Integer.parseInt(String.valueOf(charW));
            } else {
                intW = calculateIntFromChar(charW);
            }

            step--;

        }

        return result;
    }

    private static int calculateIntFromChar(char c) {
        int result = 0;
        int tempValue = (int)c;


        return result;
    }
}
