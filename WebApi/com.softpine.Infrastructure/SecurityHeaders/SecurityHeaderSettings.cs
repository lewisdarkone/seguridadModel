﻿namespace com.softpine.muvany.infrastructure.SecurityHeaders;

/// <summary>
/// 
/// </summary>
public class SecurityHeaderSettings
{
    /// <summary>
    /// 
    /// </summary>
    public bool Enable { get; set; }

    /// <summary>
    /// X-Frame-Options.
    /// </summary>
    public string? XFrameOptions { get; set; }

    /// <summary>
    /// X-Content-Type-Options.
    /// </summary>
    public string? XContentTypeOptions { get; set; }

    /// <summary>
    /// Referrer-Policy.
    /// </summary>
    public string? ReferrerPolicy { get; set; }

    /// <summary>
    /// Permissions-Policy.
    /// </summary>
    public string? PermissionsPolicy { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? SameSite { get; set; }

    /// <summary>
    /// X-XSS-Protection.
    /// </summary>
    public string? XXSSProtection { get; set; }

    ///// <summary>
    ///// </summary>
    ////public List<string> ContentPolicy { get; set; }.
}
