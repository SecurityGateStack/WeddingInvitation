using System.Globalization;

namespace WeddingInvitation.Components.Invitation;

public static class InvitationContent
{
    public const string GroomName = "فرهاد";
    public const string BrideName = "ایلین";
    public const string VenueName = "تالار رامونا";
    public const string VenueAddress = "";
    public const string MapUrl = "https://www.google.com/maps/search/?api=1&query=%D8%AA%D8%A7%D9%84%D8%A7%D8%B1+%D8%B1%D8%A7%D9%85%D9%88%D9%86%D8%A7";
    public const string OpeningLine = "با خوشحالی از شما دعوت می‌کنیم در جشن ازدواجمان کنار ما باشید.";
    public const string ClosingLine = "حضور گرم شما این شب را کامل می‌کند.";

    public static readonly DateTimeOffset CeremonyAt =
        new(2026, 12, 18, 19, 30, 0, new TimeSpan(3, 30, 0));

    private static readonly PersianCalendar Calendar = new();

    private static readonly string[] Weekdays = ["یکشنبه", "دوشنبه", "سه‌شنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه"];

    private static readonly string[] Months =
    [
        "", "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد",
        "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"
    ];

    public static string GroomInitial => FirstLetter(GroomName);

    public static string BrideInitial => FirstLetter(BrideName);

    public static string CeremonyDateText
    {
        get
        {
            var local = CeremonyAt.DateTime;
            var weekday = Weekdays[(int)Calendar.GetDayOfWeek(local)];
            var day = PersianDigits.Format(Calendar.GetDayOfMonth(local));
            var month = Months[Calendar.GetMonth(local)];
            var year = PersianDigits.Format(Calendar.GetYear(local));
            return $"{weekday} {day} {month} {year}";
        }
    }

    public static string CeremonyTimeText
    {
        get
        {
            var local = CeremonyAt.ToOffset(new TimeSpan(3, 30, 0));
            return $"{PersianDigits.Format(local.Hour)}:{PersianDigits.Format(local.Minute, 2)}";
        }
    }

    private static string FirstLetter(string name)
    {
        var trimmed = name.Trim();
        return trimmed.Length == 0 ? string.Empty : trimmed[0].ToString();
    }
}
