using System.ComponentModel.DataAnnotations;

namespace Odev.model {
    public class Models {
        public class Hasta {
            [Key]
            public string tc_no { get; set; }
            public string ad { get; set; }
            public string soyad { get; set; }
            public DateTime dogum_tarihi { get; set; }
            public bool cinsiyet { get; set; }
            public string? telefon { get; set; }
            public string? adres { get; set; }
            public string email { get; set; }
            public bool? aktif { get; set; }
        }

        public class Personel {
            [Key]
            public string tc_no { get; set; }
            public string ad { get; set; }
            public string soyad { get; set; }
            public string pozisyon { get; set; }
            public string? telefon { get; set; }
            public string? email { get; set; }
            public bool? aktif { get; set; }

        }

        public class Doktor : Personel {
            public string uzmanlik { get; set; }
            public bool? aktif { get; set; }

        }

        public class Hizmetci : Personel {
            public string departman { get; set; }
        }

        public class Hemsire : Personel {
            public string brans { get; set; }
        }

        public class Poliklinik {
            [Key]
            public int id { get; set; }
            public string ad { get; set; }
            public bool? aktif { get; set; }
        }

        public class Randevu {
            [Key]
            public int id { get; set; }
            public string hasta_tc_no { get; set; }
            public string doktor_tc_no { get; set; }
            public int poliklinik_id { get; set; }
            public DateTime randevu_tarihi { get; set; }
            public TimeSpan randevu_saati { get; set; }
            public bool? aktif { get; set; }

        }

        public class Hastalik {
            [Key]
            public int id { get; set; }
            public string ad { get; set; }
            public string aciklama { get; set; }
            public bool? aktif { get; set; }

        }

        public class HastaHastalik {
            public int id { get; set; }
            public string hasta_tc_no { get; set; }
            public int hastalik_id { get; set; }
            public DateTime teshis_tarihi { get; set; }
        }

        public class Ilac {
            [Key]
            public int id { get; set; }
            public string ad { get; set; }
            public string aciklama { get; set; }
            public bool? aktif { get; set; }

        }

        public class Recete {
            [Key]
            public int id { get; set; }
            public string hasta_tc_no { get; set; }
            public string doktor_tc_no { get; set; }
            public DateTime tarih { get; set; }
            public bool? aktif { get; set; }

        }

        public class ReceteIlac {
            public int id { get; set; }
            public int recete_id { get; set; }
            public int ilac_id { get; set; }
            public int miktar { get; set; }
        }

        public class Oda {
            [Key]
            public int id { get; set; }
            public int kat_no { get; set; }
            public int oda_no { get; set; }
            public int yatak_sayisi { get; set; }
            public bool? aktif { get; set; }

        }

        public class Yatis {
            [Key]
            public int id { get; set; }
            public string hasta_tc_no { get; set; }
            public int oda_id { get; set; }
            public DateTime baslangic_tarihi { get; set; }
            public DateTime bitis_tarihi { get; set; }
            public bool? aktif { get; set; }

        }

        public class Ameliyat {
            [Key]
            public int id { get; set; }
            public string hasta_tc_no { get; set; }
            public string doktor_tc_no { get; set; }
            public DateTime tarih { get; set; }
            public string aciklama { get; set; }
            public bool? aktif { get; set; }

        }

        public class Tahlil {
            [Key]
            public int id { get; set; }
            public string hasta_tc_no { get; set; }
            public DateTime tarih { get; set; }
            public string tahlil_turu { get; set; }
            public string sonuc { get; set; }
            public bool? aktif { get; set; }

        }

        public class Maliye {
            [Key]
            public int id { get; set; }
            public string hasta_tc_no { get; set; }
            public string odeme_turu { get; set; }
            public decimal miktar { get; set; }
            public DateTime tarih { get; set; }
            public bool? aktif { get; set; }

        }

    }
}