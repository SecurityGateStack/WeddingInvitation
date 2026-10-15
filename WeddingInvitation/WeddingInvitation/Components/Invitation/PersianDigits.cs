namespace WeddingInvitation.Components.Invitation;

public static class PersianDigits
{
    public static string Format(int value, int minDigits = 1)
    {
        var text = Math.Abs(value).ToString().PadLeft(minDigits, '0');
        return string.Create(text.Length, text, static (span, source) =>
        {
            for (var i = 0; i < source.Length; i++)
                span[i] = (char)('۰' + (source[i] - '0'));
        });
    }
}
