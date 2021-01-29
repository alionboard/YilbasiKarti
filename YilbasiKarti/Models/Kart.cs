using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YilbasiKarti.Models
{
    [Table("Kartlar")]
    public class Kart
    {
        public int Id { get; set; }
        [Display(Name ="Başlık")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [MaxLength(20, ErrorMessage = "{0} en fazla {1} karakter içerebilir.")]
        public string Baslik { get; set; }
        [Display(Name = "Gönderen Ad")]

        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [MaxLength(25, ErrorMessage = "{0} en fazla {1} karakter içerebilir.")]
        public string GonderenAd { get; set; }
        [Display(Name = "Alıcı Ad")]

        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [MaxLength(25, ErrorMessage = "{0} en fazla {1} karakter içerebilir.")]
        public string AliciAd { get; set; }
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [MaxLength(100, ErrorMessage = "{0} en fazla {1} karakter içerebilir.")]
        public string Mesaj { get; set; }
        public string ResimAd { get; set; }

    }
}