using System;

public static class Enums
{
    public static readonly string[] StopCodes = new string[]
    {
        "CRITICAL_PROCESS_DIED",
        "UNEXPECTED_STORE_EXCEPTION",
        "INACCESSIBLE_BOOT_DEVICE",
        "KERNEL_SECURITY_CHECK_FAILURE",
        "MEMORY_MANAGEMENT",
        "PAGE_FAULT_IN_NONPAGED_AREA",
        "SYSTEM_SERVICE_EXCEPTION",
        "DPC_WATCHDOG_VIOLATION",
        "IRQL_NOT_LESS_OR_EQUAL",
        "VIDEO_TDR_FAILURE",
        "BAD_SYSTEM_CONFIG_INFO",
        "DRIVER_POWER_STATE_FAILURE",
        "KMODE_EXCEPTION_NOT_HANDLED",
        "WATCHDOG_TIMEDOUT"
    };

    private static readonly Random random = new Random();

    public static string GetRandomStopCode()
    {
        return StopCodes[random.Next(StopCodes.Length)];
    }
}
