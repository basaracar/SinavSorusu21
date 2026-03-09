using System.ComponentModel.DataAnnotations;

namespace SinavSorusu21.Models;
public class Student
{
    [Key]
    public int Id { get; set; }
    [Display(Name ="Ad")]
    [Required(ErrorMessage ="Bu alanı boş geçmeyin")]
    public string Name { get; set; }
    [Display(Name ="Soyad")]
    [Required(ErrorMessage ="Bu alanı boş geçmeyin")]
    public string Surname { get; set; }
    [Display(Name ="Numara")]
    [Required(ErrorMessage ="Bu alanı boş geçmeyin")]
    public string Number { get; set; }
    [Display(Name ="Sınıfı")]
    [Required(ErrorMessage ="Bu alanı boş geçmeyin")]
    public string _Class { get; set; }
    [Display(Name ="Fotoğraf")]
    public string? Photo { get; set; }
    
}