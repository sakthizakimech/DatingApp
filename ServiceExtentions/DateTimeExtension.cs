using System;

namespace DatingApp.ServiceExtentions;

public static class DateTimeExtension
{
    public static int CalculateAge(this DateOnly date)
    {
        var CurrentDate = DateOnly.FromDateTime(DateTime.Today);
        var age = CurrentDate.Year - date.Year;
        if (date > CurrentDate.AddYears(-age)) age--;
        return age;
    }
}
