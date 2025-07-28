using System.ComponentModel.DataAnnotations;

namespace DuyAnh.SaaS.Users.Dto;

public class ChangeUserLanguageDto
{
    [Required]
    public string LanguageName { get; set; }
}