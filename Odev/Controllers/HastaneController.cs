using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Odev.model;
using static Odev.model.Models;

namespace Odev.Controllers {
    [ApiController]
    [Route("hastane")]
    public class HastaneController : ControllerBase {

        private readonly DatabaseContext _context;

        public HastaneController(DatabaseContext context) {
            _context = context;
        }

        #region Hasta
        [HttpGet("hasta")]
        public async Task<IEnumerable<Hasta>> GetHasta() {
            return await _context.hastalar
                .Select(hasta => new Hasta {
                    tc_no = hasta.tc_no,
                    ad = hasta.ad,
                    soyad = hasta.soyad,
                    dogum_tarihi = hasta.dogum_tarihi,
                    cinsiyet = hasta.cinsiyet,
                    telefon = hasta.telefon,
                    email = hasta.email,
                    aktif = hasta.aktif,
                    adres = hasta.adres
                })
                .ToListAsync();
        }

        [HttpPost("hasta")]
        public async Task<IActionResult> PostHasta(Hasta hasta) {
            if (hasta == null) {
                return BadRequest("TC No ve ID boş olamaz.");
            }
            hasta.aktif = true;
            _context.hastalar.Add(hasta);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHasta), new { id = hasta.tc_no }, hasta);
        }

        [HttpPut("hasta/{id}")]
        public async Task<IActionResult> PutHasta(string id, Hasta hasta) {
            if (id != hasta.tc_no) {
                return BadRequest("Bu TC numarasina sahip hasta bulunmamaktadir");
            }

            var mevcutHasta = await _context.hastalar.FindAsync(id);
            if (mevcutHasta == null)
                return NotFound();

            mevcutHasta.ad = hasta.ad;
            mevcutHasta.soyad = hasta.soyad;
            mevcutHasta.dogum_tarihi = hasta.dogum_tarihi;
            mevcutHasta.cinsiyet = hasta.cinsiyet;
            mevcutHasta.telefon = hasta.telefon;
            mevcutHasta.adres = hasta.adres;

            _context.Entry(mevcutHasta).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("hasta/{id}")]
        public async Task<IActionResult> DeleteHasta(string id) {
            var hasta = await _context.hastalar.FindAsync(id);
            if (hasta == null)
                return NotFound();

            // Hasta aktif durumu false olarak işaretleniyor.
            hasta.aktif = false;

            // Eğer doğum tarihi veya diğer datetime alanlar 'Unspecified' ise UTC'ye çevriliyor.
            if (hasta.dogum_tarihi.Kind == DateTimeKind.Unspecified) {
                hasta.dogum_tarihi = DateTime.SpecifyKind(hasta.dogum_tarihi, DateTimeKind.Utc);
            }

            _context.Entry(hasta).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region Doktor
        [HttpGet("doktor")]
        public async Task<IEnumerable<Doktor>> GetDoktor() {
            return await _context.doktorlar
                .Select(doktor => new Doktor {
                    tc_no = doktor.tc_no,
                    ad = doktor.ad,
                    soyad = doktor.soyad,
                    pozisyon = doktor.pozisyon,
                    telefon = doktor.telefon,
                    email = doktor.email,
                    uzmanlik = doktor.uzmanlik,
                    aktif = doktor.aktif
                })
                .ToListAsync();
        }

        [HttpPost("doktor")]
        public async Task<IActionResult> PostDoktor(Doktor doktor) {
            if (doktor == null) {
                return BadRequest("NULL bir json gonderdiniz");
            }
            doktor.aktif = true;
            _context.doktorlar.Add(doktor);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDoktor), new { id = doktor.tc_no }, doktor);
        }

        [HttpPut("doktor/{id}")]
        public async Task<IActionResult> PutDoktor(string id, Doktor doktor) {
            if (id != doktor.tc_no) {
                return BadRequest("Bu TC numarasina sahip doktor bulunmamaktadir");
            }

            var mevcutDoktor = await _context.doktorlar.FindAsync(id);
            if (mevcutDoktor == null)
                return NotFound();

            mevcutDoktor.ad = doktor.ad;
            mevcutDoktor.soyad = doktor.soyad;
            mevcutDoktor.pozisyon = doktor.pozisyon;
            mevcutDoktor.telefon = doktor.telefon;
            mevcutDoktor.email = doktor.email;
            mevcutDoktor.uzmanlik = doktor.uzmanlik;

            _context.Entry(mevcutDoktor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("doktor/{id}")]
        public async Task<IActionResult> DeleteDoktor(string id) {
            var doktor = await _context.doktorlar.FindAsync(id);
            if (doktor == null) return NotFound();

            doktor.aktif = false;
            _context.Entry(doktor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        #endregion

        #region Personel

        [HttpGet("personel")]
        public async Task<IEnumerable<Personel>> GetPersonel() {
            return await _context.personel
                .Select(personel => new Personel {
                    tc_no = personel.tc_no,
                    ad = personel.ad,
                    soyad = personel.soyad,
                    pozisyon = personel.pozisyon,
                    telefon = personel.telefon,
                    email = personel.email,
                })
                .ToListAsync();
        }

        [HttpPost("personel")]
        public async Task<IActionResult> PostPersonel(Personel personel) {
            if (personel == null) {
                return BadRequest("NULL bir json gonderdiniz");
            }
            personel.aktif = true;
            _context.personel.Add(personel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPersonel), new { id = personel.tc_no }, personel);
        }

        [HttpPut("personel/{id}")]
        public async Task<IActionResult> PutPersonel(string id, Personel personel) {
            if (id != personel.tc_no) {
                return BadRequest("Bu TC numarasina sahip personel bulunmamaktadir");
            }

            var mevcutPersonel = await _context.personel.FindAsync(id);
            if (mevcutPersonel == null)
                return NotFound();

            mevcutPersonel.ad = personel.ad;
            mevcutPersonel.soyad = personel.soyad;
            mevcutPersonel.pozisyon = personel.pozisyon;
            mevcutPersonel.telefon = personel.telefon;
            mevcutPersonel.email = personel.email;

            _context.Entry(mevcutPersonel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("personel/{id}")]
        public async Task<IActionResult> DeletePersonel(int id) {
            var personel = await _context.personel.FindAsync(id);
            if (personel == null) return NotFound();

            personel.aktif = false;
            _context.Entry(personel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        #endregion

        #region Poliklinik

        [HttpGet("poliklinik")]
        public async Task<IEnumerable<Poliklinik>> GetPoliklinik() {
            return await _context.poliklinikler
                .Select(poliklinik => new Poliklinik {
                    id = poliklinik.id,
                    ad = poliklinik.ad
                })
                .ToListAsync();
        }

        [HttpPost("poliklinik")]
        public async Task<IActionResult> PostPoliklinik(Poliklinik poliklinik) {
            if (poliklinik == null) {
                return BadRequest("NULL bir json gonderdiniz");
            }
            poliklinik.aktif = true;
            _context.poliklinikler.Add(poliklinik);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPoliklinik), new { id = poliklinik.id }, poliklinik);
        }

        // PUT - Poliklinik
        [HttpPut("poliklinik/{id}")]
        public async Task<IActionResult> PutPoliklinik(int id, Poliklinik poliklinik) {
            if (id != poliklinik.id) {
                return BadRequest("Bu ID numarasina sahip poliklinik bulunmamaktadir");
            }

            var mevcutPoliklinik = await _context.poliklinikler.FindAsync(id);
            if (mevcutPoliklinik == null)
                return NotFound();

            mevcutPoliklinik.ad = poliklinik.ad;

            _context.Entry(mevcutPoliklinik).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("poliklinik/{id}")]
        public async Task<IActionResult> DeletePoliklinik(int id) {
            var poliklinik = await _context.poliklinikler.FindAsync(id);
            if (poliklinik == null) return NotFound();

            poliklinik.aktif = false;
            _context.Entry(poliklinik).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region Randevu

        [HttpGet("randevu")]
        public async Task<IEnumerable<Randevu>> GetRandevu() {
            return await _context.randevular
                .Select(randevu => new Randevu {
                    id = randevu.id,
                    hasta_tc_no = randevu.hasta_tc_no,
                    doktor_tc_no = randevu.doktor_tc_no,
                    poliklinik_id = randevu.poliklinik_id,
                    randevu_tarihi = randevu.randevu_tarihi,
                    randevu_saati = randevu.randevu_saati
                })
                .ToListAsync();
        }

        [HttpPost("randevu")]
        public async Task<IActionResult> PostRandevu(Randevu randevu) {
            if (randevu.hasta_tc_no == null || randevu.doktor_tc_no == null) {
                return BadRequest("ID ve TC No'lar boş olamaz.");
            }
            randevu.aktif = true;
            _context.randevular.Add(randevu);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRandevu), new { id = randevu.id }, randevu);
        }

        [HttpPut("randevu/{id}")]
        public async Task<IActionResult> PutRandevu(int id, Randevu randevu) {
            if (id != randevu.id)
                return BadRequest("ID uyuşmazlığı.");

            if (randevu.randevu_tarihi.Kind == DateTimeKind.Unspecified) {
                randevu.randevu_tarihi = DateTime.SpecifyKind(randevu.randevu_tarihi, DateTimeKind.Utc);
            }

            _context.Entry(randevu).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("randevu/{id}")]
        public async Task<IActionResult> DeleteRandevu(int id) {
            var randevu = await _context.randevular.FindAsync(id);
            if (randevu == null) return NotFound();

            if (randevu.randevu_tarihi.Kind == DateTimeKind.Unspecified) {
                randevu.randevu_tarihi = DateTime.SpecifyKind(randevu.randevu_tarihi, DateTimeKind.Utc);
            }

            randevu.aktif = false;
            _context.Entry(randevu).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        #endregion

        #region Hastalik

        [HttpGet("hastalik")]
        public async Task<IEnumerable<Hastalik>> GetHastalik() {
            return await _context.hastaliklar
                .Select(hastalik => new Hastalik {
                    id = hastalik.id,
                    ad = hastalik.ad,
                    aciklama = hastalik.aciklama
                })
                .ToListAsync();
        }

        [HttpPost("hastalik")]
        public async Task<IActionResult> PostHastalik(Hastalik hastalik) {
            if (hastalik == null) {
                return BadRequest("NULL json gönderdiniz");
            }
            hastalik.aktif = true;
            _context.hastaliklar.Add(hastalik);
            await _context.SaveChangesAsync();
            return CreatedAtAction("PostHastalik", new { id = hastalik.id }, hastalik);
        }

        [HttpPut("hastalik/{id}")]
        public async Task<IActionResult> PutHastalik(int id, Hastalik hastalik) {
            if (id != hastalik.id) {
                return BadRequest("Bu ID'ye sahip bir hastalik bulunmamaktadir");
            }

            var mevcutHastalik = await _context.hastaliklar.FindAsync(id);
            if (mevcutHastalik == null)
                return NotFound();

            mevcutHastalik.ad = hastalik.ad;
            mevcutHastalik.aciklama = hastalik.aciklama;

            _context.Entry(mevcutHastalik).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("hastalik/{id}")]
        public async Task<IActionResult> DeleteHastalik(int id) {
            var hastalik = await _context.hastaliklar.FindAsync(id);
            if (hastalik == null)
                return NotFound();

            hastalik.aktif = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        #endregion

        #region Ilac

        [HttpGet("ilac")]
        public async Task<IEnumerable<Ilac>> GetIlac() {
            return await _context.ilaclar
                .Select(ilac => new Ilac {
                    id = ilac.id,
                    ad = ilac.ad,
                    aciklama = ilac.aciklama
                })
                .ToListAsync();
        }

        [HttpPost("ilac")]
        public async Task<IActionResult> PostIlac(Ilac ilac) {
            if (ilac == null) {
                return BadRequest("NULL bir json gönderdiniz");
            }
            ilac.aktif = true;
            _context.ilaclar.Add(ilac);
            await _context.SaveChangesAsync();
            return CreatedAtAction("PostIlac", new { id = ilac.id }, ilac);
        }

        [HttpPut("ilac/{id}")]
        public async Task<IActionResult> PutIlac(int id, Ilac ilac) {
            if (id != ilac.id) {
                return BadRequest("Böyle bir ilac bulunmamaktadır");
            }

            var mevcutIlac = await _context.ilaclar.FindAsync(id);
            if (mevcutIlac == null)
                return NotFound();

            mevcutIlac.ad = ilac.ad;
            mevcutIlac.aciklama = ilac.aciklama;

            _context.Entry(mevcutIlac).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("ilac/{id}")]
        public async Task<IActionResult> DeleteIlac(int id) {
            var ilac = await _context.ilaclar.FindAsync(id);
            if (ilac == null)
                return NotFound();

            ilac.aktif = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        #endregion

        #region Recete

        [HttpGet("recete")]
        public async Task<IEnumerable<Recete>> GetRecete() {
            return await _context.receteler
                .Select(recete => new Recete {
                    id = recete.id,
                    hasta_tc_no = recete.hasta_tc_no,
                    doktor_tc_no = recete.doktor_tc_no,
                    tarih = recete.tarih
                })
                .ToListAsync();
        }

        [HttpPost("recete")]
        public async Task<IActionResult> PostRecete(Recete recete) {
            //if (string.IsNullOrEmpty(recete.hasta_tc_no) || string.IsNullOrEmpty(recete.doktor_tc_no)) {
            //    return BadRequest("Hasta TC veya Doktor TC boş olamaz.");
            //}
            //recete.aktif = true;
            //_context.receteler.Add(recete);
            //await _context.SaveChangesAsync();
            //return CreatedAtAction("PostRecete", new { id = recete.id }, recete);

            // Prosedür çağrısı
            var sql = "CALL recete_olusturma_kontrol({0}, {1}, {2})";
            await _context.Database.ExecuteSqlRawAsync(sql,
                recete.hasta_tc_no,
                recete.doktor_tc_no,
                recete.tarih);

            return CreatedAtAction("PostRecete", new { id = recete.id }, recete);
        }

        [HttpPut("recete/{id}")]
        public async Task<IActionResult> PutRecete(int id, Recete recete) {
            if (id != recete.id) {
                return BadRequest("ID uyuşmazlığı.");
            }

            var mevcutRecete = await _context.receteler.FindAsync(id);
            if (mevcutRecete == null)
                return NotFound();

            mevcutRecete.hasta_tc_no = recete.hasta_tc_no;
            mevcutRecete.doktor_tc_no = recete.doktor_tc_no;
            mevcutRecete.tarih = recete.tarih;

            _context.Entry(mevcutRecete).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("recete/{id}")]
        public async Task<IActionResult> DeleteRecete(int id) {
            var recete = await _context.receteler.FindAsync(id);
            if (recete == null)
                return NotFound();

            recete.aktif = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        #endregion

        #region Oda

        [HttpGet("oda")]
        public async Task<IEnumerable<Oda>> GetOda() {
            return await _context.odalar
                .Select(oda => new Oda {
                    id = oda.id,
                    kat_no = oda.kat_no,
                    oda_no = oda.oda_no,
                    yatak_sayisi = oda.yatak_sayisi
                })
                .ToListAsync();
        }

        [HttpPost("oda")]
        public async Task<IActionResult> PostOda(Oda oda) {
            if (oda == null) {
                return BadRequest("NULL bir json gönderdiniz");
            }
            oda.aktif = true;
            _context.odalar.Add(oda);
            await _context.SaveChangesAsync();
            return CreatedAtAction("PostOda", new { id = oda.id }, oda);
        }

        [HttpPut("oda/{id}")]
        public async Task<IActionResult> PutOda(int id, Oda oda) {
            if (id != oda.id) {
                return BadRequest("ID uyuşmazlığı.");
            }

            var mevcutOda = await _context.odalar.FindAsync(id);
            if (mevcutOda == null)
                return NotFound();

            mevcutOda.kat_no = oda.kat_no;
            mevcutOda.oda_no = oda.oda_no;
            mevcutOda.yatak_sayisi = oda.yatak_sayisi;

            _context.Entry(mevcutOda).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("oda/{id}")]
        public async Task<IActionResult> DeleteOda(int id) {
            var oda = await _context.odalar.FindAsync(id);
            if (oda == null)
                return NotFound();

            oda.aktif = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region Yatis

        [HttpGet("yatis")]
        public async Task<IEnumerable<Yatis>> GetYatis() {
            return await _context.yatislar
                .Select(yatis => new Yatis {
                    id = yatis.id,
                    hasta_tc_no = yatis.hasta_tc_no,
                    oda_id = yatis.oda_id,
                    baslangic_tarihi = yatis.baslangic_tarihi,
                    bitis_tarihi = yatis.bitis_tarihi
                })
                .ToListAsync();
        }

        [HttpPost("yatis")]
        public async Task<IActionResult> PostYatis([FromBody] Yatis yatis) {
            if (yatis == null) {
                return BadRequest();
            }
            yatis.aktif = true;
            _context.yatislar.Add(yatis);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetYatis), new { id = yatis.id }, yatis);
        }

        [HttpPut("yatis/{id}")]
        public async Task<IActionResult> PutYatis(int id, Yatis yatis) {
            if (id != yatis.id)
                return BadRequest("ID uyuşmazlığı.");

            var mevcutYatis = await _context.yatislar.FindAsync(id);
            if (mevcutYatis == null)
                return NotFound();

            mevcutYatis.hasta_tc_no = yatis.hasta_tc_no;
            mevcutYatis.oda_id = yatis.oda_id;
            mevcutYatis.baslangic_tarihi = yatis.baslangic_tarihi;
            mevcutYatis.bitis_tarihi = yatis.bitis_tarihi;

            _context.Entry(mevcutYatis).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("yatis/{id}")]
        public async Task<IActionResult> DeleteYatis(int id) {
            var yatis = await _context.yatislar.FindAsync(id);
            if (yatis == null)
                return NotFound();

            yatis.aktif = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        #endregion

        #region Ameliyat

        [HttpGet("ameliyat")]
        public async Task<IEnumerable<Ameliyat>> GetAmeliyat() {
            return await _context.ameliyatlar
                .Select(ameliyat => new Ameliyat {
                    id = ameliyat.id,
                    hasta_tc_no = ameliyat.hasta_tc_no,
                    doktor_tc_no = ameliyat.doktor_tc_no,
                    tarih = ameliyat.tarih,
                    aciklama = ameliyat.aciklama
                })
                .ToListAsync();
        }

        [HttpPost("ameliyat")]
        public async Task<IActionResult> PostAmeliyat(Ameliyat ameliyat) {
            if (string.IsNullOrEmpty(ameliyat.hasta_tc_no) || string.IsNullOrEmpty(ameliyat.doktor_tc_no)) {
                return BadRequest("Hasta TC veya Doktor TC boş olamaz.");
            }
            ameliyat.aktif = true;
            _context.ameliyatlar.Add(ameliyat);
            await _context.SaveChangesAsync();
            return CreatedAtAction("PostRecete", new { id = ameliyat.id }, ameliyat);
        }

        [HttpPut("ameliyat/{id}")]
        public async Task<IActionResult> PutAmeliyat(int id, Ameliyat ameliyat) {
            if (id != ameliyat.id) {
                return BadRequest("ID uyuşmazlığı.");
            }

            var mevcutAmeliyat = await _context.ameliyatlar.FindAsync(id);
            if (mevcutAmeliyat == null)
                return NotFound();

            mevcutAmeliyat.hasta_tc_no = ameliyat.hasta_tc_no;
            mevcutAmeliyat.doktor_tc_no = ameliyat.doktor_tc_no;
            mevcutAmeliyat.tarih = ameliyat.tarih;
            mevcutAmeliyat.aciklama = ameliyat.aciklama;

            _context.Entry(mevcutAmeliyat).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("ameliyat/{id}")]
        public async Task<IActionResult> DeleteAmeliyat(int id) {
            var ameliyat = await _context.ameliyatlar.FindAsync(id);
            if (ameliyat == null)
                return NotFound();

            ameliyat.aktif = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        #endregion

        #region Tahlil

        [HttpGet("tahlil")]
        public async Task<IEnumerable<Tahlil>> GetTahlil() {
            return await _context.tahliller
                .Select(tahlil => new Tahlil {
                    id = tahlil.id,
                    hasta_tc_no = tahlil.hasta_tc_no,
                    tarih = tahlil.tarih,
                    tahlil_turu = tahlil.tahlil_turu,
                    sonuc = tahlil.sonuc
                })
                .ToListAsync();
        }

        [HttpPost("tahlil")]
        public async Task<IActionResult> PostTahlil([FromBody] Tahlil tahlil) {
            if (tahlil == null) {
                return BadRequest();
            }
            tahlil.aktif = true;
            _context.tahliller.Add(tahlil);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTahlil), new { id = tahlil.id }, tahlil);
        }

        [HttpPut("tahlil/{id}")]
        public async Task<IActionResult> PutTahlil(int id, Tahlil tahlil) {
            if (id != tahlil.id)
                return BadRequest("Boyle bir tahlil bulunmamaktadir");

            var mevcutTahlil = await _context.tahliller.FindAsync(id);
            if (mevcutTahlil == null)
                return NotFound();

            mevcutTahlil.tahlil_turu = tahlil.tahlil_turu;
            mevcutTahlil.sonuc = tahlil.sonuc;
            mevcutTahlil.tarih = tahlil.tarih;

            _context.Entry(mevcutTahlil).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("tahlil/{id}")]
        public async Task<IActionResult> DeleteTahlil(int id) {
            var tahlil = await _context.tahliller.FindAsync(id);
            if (tahlil == null) {
                return NotFound();
            }

            tahlil.aktif = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        #endregion

        #region Maliye

        [HttpGet("maliye")]
        public async Task<IEnumerable<Maliye>> GetMaliye() {
            return await _context.maliye
                .Select(maliye => new Maliye {
                    id = maliye.id,
                    hasta_tc_no = maliye.hasta_tc_no,
                    odeme_turu = maliye.odeme_turu,
                    miktar = maliye.miktar,
                    tarih = maliye.tarih
                })
                .ToListAsync();
        }

        [HttpPost("maliye")]
        public async Task<IActionResult> PostMaliye(Maliye maliye) {
            if (maliye == null) {
                return BadRequest("NULL bir json gönderdiniz");
            }
            maliye.aktif = true;
            _context.maliye.Add(maliye);
            await _context.SaveChangesAsync();
            return CreatedAtAction("PostMaliye", new { id = maliye.id }, maliye);
        }

        [HttpPut("maliye/{id}")]
        public async Task<IActionResult> PutMaliye(int id, Maliye maliye) {
            if (id != maliye.id)
                return BadRequest("Boyle bir odeme bulunmamaktadir");

            var mevcutMaliye = await _context.maliye.FindAsync(id);
            if (mevcutMaliye == null)
                return NotFound();

            mevcutMaliye.odeme_turu = maliye.odeme_turu;
            mevcutMaliye.miktar = maliye.miktar;
            mevcutMaliye.tarih = maliye.tarih;

            _context.Entry(mevcutMaliye).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("maliye/{id}")]
        public async Task<IActionResult> DeleteMaliye(int id) {
            var maliye = await _context.maliye.FindAsync(id);
            if (maliye == null)
                return NotFound();

            maliye.aktif = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion
    }
}