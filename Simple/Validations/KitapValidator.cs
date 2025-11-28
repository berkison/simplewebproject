//using Simple.Validations;
//using System.Data;
using FluentValidation;
using Simple.DTOs;

namespace Simple.Validations
{
    // KitapEklemeDto sınıfını denetleyecek olan polis memuru bu sınıftır
    public class KitapValidator : AbstractValidator<KitapEklemeDTO>
    {
        public KitapValidator()
        {
            // Kural 1: Kitap adı boş olamaz
            RuleFor(x => x.Ad)
                .NotEmpty().WithMessage("Lütfen kitap adını boş geçmeyiniz.")
                .MinimumLength(2).WithMessage("Kitap adı en az 2 karakter olmalıdır.");

            // Kural 2: Yazar adı boş olamaz
            RuleFor(x => x.Yazar)
                .NotEmpty().WithMessage("Yazar adı zorunludur.");

            // Kural 3: Sayfa sayısı 0'dan büyük olmalı
            RuleFor(x => x.SayfaSayisi)
                .GreaterThan(0).WithMessage("Sayfa sayısı 0 veya negatif olamaz.")
                .LessThan(5000).WithMessage("5000 sayfadan fazla kitap olamaz, ansiklopedi mi yazıyorsun? :)");
        }
    }
}