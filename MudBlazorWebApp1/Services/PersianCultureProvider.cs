using System;
using System.Globalization;
using System.Reflection;

public class PersianCultureProvider
{
    /// <summary>
    /// مقدار تاریخ اولیه برای نمونه‌سازی (می‌توانید با متد یا پراپرتی تغییر دهید)
    /// </summary>
    public DateTime? DefaultDate { get; set; } = new DateTime(2026, 02, 15); // معادل 1399-11-26 در تقویم فارسی

    /// <summary>
    /// ایجاد CultureInfo فارسی با تنظیمات تقویم شمسی و نام‌های سفارشی
    /// </summary>
    public CultureInfo GetPersianCulture()
    {
        var culture = new CultureInfo("fa-IR");
        DateTimeFormatInfo formatInfo = culture.DateTimeFormat;

        formatInfo.AbbreviatedDayNames = new[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
        formatInfo.DayNames = new[] { "یکشنبه", "دوشنبه", "سه شنبه", "چهار شنبه", "پنجشنبه", "جمعه", "شنبه" };
        var monthNames = new[]
        {
            "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور",
            "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", ""
        };
        formatInfo.AbbreviatedMonthNames =
            formatInfo.MonthNames =
            formatInfo.MonthGenitiveNames =
            formatInfo.AbbreviatedMonthGenitiveNames = monthNames;

        formatInfo.AMDesignator = "ق.ظ";
        formatInfo.PMDesignator = "ب.ظ";
        formatInfo.ShortDatePattern = "yyyy/MM/dd";
        formatInfo.LongDatePattern = "dddd, dd MMMM, yyyy";
        formatInfo.FirstDayOfWeek = DayOfWeek.Saturday;

        // تقویم شمسی
        Calendar persianCal = new PersianCalendar();

        // ست کردن calendar با reflection (تنها راه عملی در برخی نسخه‌ها)
        var cultureCalField = culture.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
        if (cultureCalField != null)
            cultureCalField.SetValue(culture, persianCal);

        var dateFormatCalField = formatInfo.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
        if (dateFormatCalField != null)
            dateFormatCalField.SetValue(formatInfo, persianCal);

        culture.NumberFormat.NumberDecimalSeparator = "/";
        culture.NumberFormat.DigitSubstitution = DigitShapes.NativeNational;
        culture.NumberFormat.NumberNegativePattern = 0;

        return culture;
    }
}