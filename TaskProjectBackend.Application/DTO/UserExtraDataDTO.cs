﻿namespace TaskProjectBackend.Application.DTO;

public class UserExtraDataDTO
{
    public string Username { get; set; }
    public string? ProfilePicturePath { get; set; }
    public string? ProfilePictureBase64 { get; set; }
}