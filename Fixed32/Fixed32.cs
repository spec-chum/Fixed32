namespace Types;

/// <summary>
/// Represents a fixed-point decimal number with a 16-bit fraction part and variable scale.
/// </summary>
public struct Fixed32 : IEquatable<Fixed32>
{
    private const int FractionMask = 0xffff;
    private const int DefaultScale = 16;

    /// <summary>
    /// Gets the raw value of the fixed-point number.
    /// </summary>
    public long RawValue { get; private set; }

    /// <summary>
    /// Gets the scale of the fixed-point number.
    /// </summary>
    public int Scale { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Fixed32"/> struct with the specified scale and a raw value of zero.
    /// </summary>
    /// <param name="scale">The scale of the fixed-point number.</param>
    public Fixed32(int scale) : this(scale, 0) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Fixed32"/> struct with the specified scale and whole number part.
    /// </summary>
    /// <param name="scale">The scale of the fixed-point number.</param>
    /// <param name="wholeNumber">The whole number part of the fixed-point number.</param>
    public Fixed32(int scale, int wholeNumber)
    {
        Scale = scale;
        RawValue = wholeNumber << scale;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Fixed32"/> struct with the specified scale and real number value.
    /// </summary>
    /// <param name="scale">The scale of the fixed-point number.</param>
    /// <param name="realNumber">The real number value of the fixed-point number.</param>
    public Fixed32(int scale, float realNumber)
    {
        Scale = scale;
        RawValue = (long)(realNumber * (1 << scale));
    }

    /// <summary>
    /// Gets the whole number part of the fixed-point number.
    /// </summary>
    public int WholeNumber => (int)(RawValue >> Scale) + (RawValue < 0 && Fraction != 0 ? 1 : 0);

    /// <summary>
    /// Gets the fraction part of the fixed-point number.
    /// </summary>
    public int Fraction => (int)(RawValue & FractionMask);

    /// <summary>
    /// Adds two fixed-point numbers together.
    /// </summary>
    /// <param name="leftHandSide">The left-hand side operand of the addition.</param>
    /// <param name="rightHandSide">The right-hand side operand of the addition.</param>
    /// <returns>The sum of the two fixed-point numbers.</returns>
    public static Fixed32 operator +(Fixed32 leftHandSide, Fixed32 rightHandSide)
    {
        leftHandSide.RawValue += rightHandSide.RawValue;
        return leftHandSide;
    }

    /// <summary>
    /// Subtracts one fixed-point number from another.
    /// </summary>
    /// <param name="leftHandSide">The left-hand side operand of the subtraction.</param>
    /// <param name="rightHandSide">The right-hand side operand of the subtraction.</param>
    /// <returns>The difference of the two fixed-point numbers.</returns>
    public static Fixed32 operator -(Fixed32 leftHandSide, Fixed32 rightHandSide)
    {
        leftHandSide.RawValue -= rightHandSide.RawValue;
        return leftHandSide;
    }

    /// <summary>
    /// Multiplies two Fixed32 values and returns the result.
    /// </summary>
    /// <param name="leftHandSide">The first value to multiply.</param>
    /// <param name="rightHandSide">The second value to multiply.</param>
    /// <returns>The product of the two specified values.</returns>
    public static Fixed32 operator *(Fixed32 leftHandSide, Fixed32 rightHandSide)
    {
        leftHandSide.RawValue = (leftHandSide.RawValue * rightHandSide.RawValue) >> leftHandSide.Scale;
        return leftHandSide;
    }

    /// <summary>
    /// Divides one Fixed32 value by another and returns the result.
    /// </summary>
    /// <param name="leftHandSide">The dividend.</param>
    /// <param name="rightHandSide">The divisor.</param>
    /// <returns>The quotient of the specified values.</returns>
    public static Fixed32 operator /(Fixed32 leftHandSide, Fixed32 rightHandSide)
    {
        leftHandSide.RawValue = (leftHandSide.RawValue << leftHandSide.Scale) / rightHandSide.RawValue;
        return leftHandSide;
    }

    /// <summary>
    /// Converts the given Fixed32 value to a double-precision floating-point number.
    /// </summary>
    /// <param name="number">The value to convert.</param>
    /// <returns>The double-precision floating-point number equivalent of the given Fixed32 value.</returns>
    public static explicit operator double(Fixed32 number) => (double)number.RawValue / (1 << number.Scale);

    /// <summary>
    /// Implicitly converts the given Fixed32 value to an integer.
    /// </summary>
    /// <param name="number">The value to convert.</param>
    /// <returns>The integer equivalent of the given Fixed32 value.</returns>
    public static implicit operator int(Fixed32 number) => number.WholeNumber;

    /// <summary>
    /// Implicitly converts the given integer to a Fixed32 value with the default scale of 16 decimal places.
    /// </summary>
    /// <param name="number">The integer to convert.</param>
    /// <returns>A new Fixed32 value with the default scale of 16 decimal places.</returns>
    public static implicit operator Fixed32(int number) => new(DefaultScale, number);

    /// <summary>
    /// Determines whether this instance and the specified Fixed32 value are equal.
    /// </summary>
    /// <param name="other">The Fixed32 value to compare with this instance.</param>
    /// <returns><c>true</c> if the specified Fixed32 value is equal to this instance; otherwise, <c>false</c>.</returns>
    public bool Equals(Fixed32 other) => RawValue == other.RawValue && Scale == other.Scale;

    /// <summary>
    /// Determines whether the specified object is equal to the current instance of <see cref="Fixed32"/>.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>true if the specified object is a Fixed32 value and is equal to the current instance; otherwise, false.</returns>
    public override bool Equals(object? obj) => obj is Fixed32 fixed32 && Equals(fixed32);

    /// <summary>
    /// Returns a string that represents the current Fixed32 value.
    /// </summary>
    /// <returns>A string that represents the current Fixed32 value.</returns>
    public override string ToString() => ((double)this).ToString();

}