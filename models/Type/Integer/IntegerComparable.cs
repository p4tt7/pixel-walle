public interface IntegerComparable
{

    bool GreaterThan(int a, int b) { return a > b; }
    bool LessThan(int a, int b) { return a < b; }
    bool GreaterEqualThan(int a, int b) { return a >= b; }
    bool LessEqualThan(int a, int b) { return a <= b; }
    bool Equal(int a, int b) { if (a == b) return true; return false; }

}