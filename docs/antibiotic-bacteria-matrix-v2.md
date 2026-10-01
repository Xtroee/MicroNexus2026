# Antibiotic × Bacteria Effectiveness Matrix — v2 (+ Klebsiella pneumoniae / Meropenem)

Sumber: `MATERI GAME-1.pdf` (referensi resmi skripsi, 18 halaman — matriks `+/-`, profil antibiotik, profil bakteri,
deskripsi penyakit, tabel kesimpulan). Dokumen ini menerjemahkan skala `+` PDF ke skala numerik 0–20 yang dipakai
di kode (`Assets/Scripts/Projectile/*.cs`, konvensi `0 = immune, 1-19 = partial, 20 = full effective`), lalu
menambahkan baris/kolom untuk bakteri & antibiotik ke-8 yang direkomendasikan sesi lalu.

**Konversi skala**: `-` = 0 · `+` = 4 · `++` = 8 · `+++` = 12 · `++++` = 16 · `+++++` = 20

---

## 1. Matriks lengkap (7 → 8 antibiotik × 7 → 8 bakteri)

| Antibiotik \ Bakteri | S. pneumoniae (G+) | MSSA (G+) | MRSA (G+) | M. catarrhalis (G−) | H. influenzae (G−) | E. coli (G−) | P. aeruginosa (G−) | **K. pneumoniae ESBL (G−)** |
|---|---|---|---|---|---|---|---|---|
| Amoxicillin | 20 | 16 | 0 | 4 | 8 | 4 | 0 | **0** |
| Cefixime | 4 | 4 | 0 | 12 | 16 | 20 | 0 | **4** |
| Vancomisin | 20 | 20 | 20 | 0 | 0 | 0 | 0 | **0** |
| Azitromisin | 16 | 12 | 0 | 12 | 8 | 0 | 0 | **4** |
| Ciprofloxacin | 4 | 4 | 0 | 4 | 12 | 20 | 20 | **4** |
| Levofloksasin | 20 | 8 | 8 | 8 | 8 | 12 | 12 | **4** |
| Cotrimoxazole | 8 | 12 | 12 | 8 | 8 | 16 | 0 | **8** |
| **Meropenem** | **12** | **16** | **0** | **16** | **20** | **20** | **20** | **20** |

Baris/kolom **bold** = tambahan baru. Angka non-bold di baris/kolom lama itu hasil konversi langsung dari matriks
`+/-` di halaman 1 PDF — belum tentu sama persis dengan angka yang saat ini ada di `Projectile.cs` (itu di-hardcode
manual sebelumnya, per warna, bukan dari referensi ini). Kalau matriks ini mau jadi acuan resmi, perlu ada pass
buat nyocokin ulang 7×7 lama di kode — bisa dikerjain kapan pun kamu siap.

---

## 2. Kenapa nilai Klebsiella pneumoniae & Meropenem segini

**Klebsiella pneumoniae (ESBL-producing strain)** — dipilih varian ESBL (bukan strain biasa) karena itu yang punya
nilai edukasi paling kuat soal AMR (masuk kategori *critical priority* di WHO Bacterial Priority Pathogens List
2024, bareng Enterobacterales lain yang resisten sefalosporin gen-3/karbapenem).

- **Amoxicillin = 0**: Klebsiella secara intrinsik (bawaan genetik, bukan cuma ESBL) punya beta-laktamase
  kromosomal yang bikin dia dari sononya udah kebal ke aminopenicillin — beda dari E. coli yang baru resisten kalau
  dapet ESBL.
- **Cefixime = 4**: PDF sendiri nyebut Klebsiella pneumoniae sebagai target bacteria Cefixime (hlm. 7) — tapi itu
  buat strain biasa. Strain ESBL spesifiknya justru resisten ke hampir semua sefalosporin (termasuk gen-3 kayak
  Cefixime), makanya nilainya rendah, bukan tinggi.
- **Vancomisin = 0**: Klebsiella itu Gram-negatif, vancomycin gak bisa nembus membran luarnya sama sekali — ini
  berlaku ke semua bakteri Gram-negatif di matriks (lihat kolom Vancomisin, semua G− = 0).
- **Ciprofloxacin & Levofloksasin = 4 masing-masing**: keduanya satu kelas (fluorokuinolon), studi nunjukin strain
  ESBL K. pneumoniae resisten ke ciprofloxacin di ~60-65% kasus, dan karena mekanisme resistensinya di level
  target-enzim (gyrA/parC) yang dipakai bareng, biasanya ikut resisten juga ke levofloxacin.
- **Cotrimoxazole = 8**: PDF nyebut "Klebsiella spp." sebagai target Cotrimoxazole (hlm. 9) — masih ada aktivitas
  parsial, jadi nilainya menengah, bukan nol.
- **Meropenem = 20**: karbapenem adalah "reference treatment" standar buat infeksi Enterobacterales penghasil ESBL
  (termasuk K. pneumoniae) — ini pasangan yang paling nyambung karena jadi jawaban langsung ke resistensi di atas.

**Meropenem vs 7 bakteri lain**:
- **MRSA = 0**: karbapenem tetap beta-laktam — MRSA secara definisi resisten ke *semua* beta-laktam (termasuk
  karbapenem), bukan cuma methicillin. Ini poin edukasi penting: "obat terkuat" pun ada batasnya kalau
  mekanisme resistensinya di level kelas obat, bukan cuma satu senyawa.
- **E. coli & P. aeruginosa = 20**: PDF sendiri nyebut "Carbapenems (for ESBL-producing strains)" di profil E. coli
  (hlm. 14) dan "Meropenem" eksplisit di daftar antibiotik efektif P. aeruginosa (hlm. 15).
- Sisanya (S. pneumoniae, MSSA, M. catarrhalis, H. influenzae) — karbapenem broad-spectrum, aktif ke hampir semua
  kecuali yang secara definisi resisten beta-laktam (MRSA).

---

## 3. Contoh kasus penyakit — Klebsiella pneumoniae

Ditulis dengan format sama kayak halaman "Deskripsi berdasarkan penyakit" di PDF, plus 2 baris tambahan
untuk tabel kesimpulan (hlm. 17).

### HOSPITAL-ACQUIRED PNEUMONIA (HAP) — kasus tambahan
PDF sendiri udah nyinggung ini di halaman 4: *"Enteric gram-negative bacilli such as K. pneumoniae and E. coli are
also common (13%–20%)"* sebagai penyebab HAP, di samping P. aeruginosa dan S. aureus yang udah lebih dulu ada di
matriks. Klebsiella jadi penyebab HAP yang realistis untuk dipasangkan ke bakteri Gram-negatif lain yang udah ada.

**Treatment**: Meropenem (kalau dicurigai/confirmed ESBL) — sejalan dengan kolom Cipro/Levo yang udah rendah di
matriks P. aeruginosa & sekarang Klebsiella, jadi HAP severity tinggi = kasus paling pas buat ngenalin karbapenem.

### URINARY TRACT INFECTION (UTI) — kasus tambahan
PDF halaman 5 juga udah nyebut *"Klebsiella pneumoniae"* sebagai salah satu penyebab UTI tidak-terkomplikasi,
di samping E. coli (80-90% kasus) yang udah jadi bakteri utama UTI di matriks lama.

**Treatment**: kalau non-ESBL bisa Cotrimoxazole/Cefixime (sama kayak E. coli); kalau ESBL → Meropenem.

### Tabel kesimpulan (extension dari hlm. 17 PDF)

| Penyakit | Jenis Penyakit | Bakteri Penyebab Tersering | Antibiotik Pilihan |
|---|---|---|---|
| Lower Respiration Infection | Hospital-acquired pneumonia (HAP) | ...MSSA/MRSA (G+), P. aeruginosa (G−), **Klebsiella pneumoniae ESBL (G−)** | Vancomisin (MRSA), Levofloksasin, Ciprofloxacin, **Meropenem (ESBL)** |
| UTI | Urinary Tract Infection (UTI) | E. coli (G−), P. aeruginosa (G−), **Klebsiella pneumoniae (G−)** | Ciprofloxacin, Levofloksasin, Cotrimoxazole, Cefixime, **Meropenem (ESBL)** |

---

## 4. Belum diputuskan / perlu keputusan kamu sebelum implementasi

1. **Matriks 7×7 lama** di `Projectile.cs` (per-warna) belum tentu cocok sama angka hasil konversi PDF ini — perlu
   diputusin apakah mau disinkronkan ulang.
2. **Klebsiella ESBL vs non-ESBL** — dokumen ini pakai varian ESBL (buat nilai edukasi AMR-nya), tapi kalau kamu
   maunya versi lebih "gampang" (non-resisten) buat level awal, nilainya bakal beda total (Cefixime/Cipro/Levo
   bakal tinggi, bukan rendah).
3. **Bio-File encyclopedia vs gameplay matrix** — dari analisis sesi lalu, sistem ensiklopedia (`RangerBioManager`)
   dan sistem gameplay (`Capsule*`/`Projectile*`) itu terpisah secara kode. Matriks ini valid buat KEDUANYA secara
   konten, tapi implementasinya butuh disentuh di 2 tempat beda.

---

## 5. Roadmap ke 14 antibiotik + 14 bakteri (target: 7 lama + 7 baru)

> **Update arah**: gak digabung/dipaksain ke 6 stage lama. Tiap pasangan baru jadi **stage sendiri** (Stage 7-13),
> pola sama kayak yang lama (1 stage = 1 kasus penyakit = bakteri penyebab + pilihan antibiotik) — cuma berdiri
> independen, gak perlu nyambung ke kategori Upper/Lower Respiration Infection atau UTI yang lama. Detail tiap
> stage ada di bagian 7 di bawah.

Target disepakati: total 14 antibiotik, 14 bakteri, dipecah ke progresi 6 stage/level yang udah ada. Meropenem +
Klebsiella (bagian 1-3 di atas) itu pasangan baru **#1 dari 7**. 6 sisanya di bawah — semua dipilih karena punya
kaitan nyata (bukan berdiri sendiri) ke antibiotik/bakteri yang UDAH ada di roster, dan divalidasi ke guideline
klinis terkini (CDC/WHO/ACG), bukan cuma dari PDF referensi doang.

### #2 — Ceftriaxone ↔ Neisseria gonorrhoeae (gonore resisten obat)

- **Kaitan ke roster lama**: satu keluarga sefalosporin sama Cefixime (beda cuma rute — Ceftriaxone suntik,
  Cefixime oral), dan PDF sendiri udah nyebut "acute uncomplicated gonorrhea" di indikasi Amoxicillin (hlm. 7) DAN
  "uncomplicated gonorrhea" di indikasi Cefixime (hlm. 7) — jadi bakteri ini sebenernya udah "disinggung" 2x di
  materi lama tapi belum pernah jadi karakter sendiri.
- **Fakta klinis terkini**: CDC per 2020 (masih berlaku) cuma merekomendasikan **satu** regimen first-line buat
  gonore — monoterapi Ceftriaxone 500mg IM dosis tunggal. Cefixime 800mg oral cuma jadi alternatif KALAU suntikan
  gak memungkinkan — artinya di dunia nyata, Ceftriaxone > Cefixime buat kasus ini, padahal keduanya sekeluarga.
  Bagus buat ngajarin "generasi/rute obat beda, efektivitas bisa beda juga."
- **Damage value usulan**: Ceftriaxone ke N. gonorrhoeae = 20 (full, first-line). Cefixime ke N. gonorrhoeae = 12
  (masih kerja, tapi cuma alternatif — dan resistensi terhadap cefixime spesifik udah mulai kelihatan di beberapa
  wilayah).
- **Stage**: cocok buat kategori baru "Sexually Transmitted Infection" — atau digabung longgar ke UTI stage
  (sama-sama genitourinari) kalau gak mau nambah kategori baru.

### #3 — Linezolid ↔ Enterococcus faecium (VRE — Vancomycin-Resistant Enterococcus)

- **Kaitan ke roster lama**: paling kuat di antara semua kandidat — VRE itu **literally bakteri yang kebal ke
  Vancomisin**, obat yang udah ada di roster dan biasanya jadi "jawaban terakhir" buat infeksi Gram-positif serius
  (termasuk MRSA). Linezolid sendiri udah disebut di PDF sebagai salah satu "Effective antibiotics" MRSA (hlm. 12)
  — jadi obat ini juga udah "nongol" di materi lama tanpa jadi karakter sendiri.
- **Fakta klinis terkini**: Linezolid itu FDA-approved & first-line buat VRE, walau ada catatan (bakteriostatik,
  bukan bakterisidal — jadi kurang ideal buat infeksi berat kayak bakteremia dibanding Daptomycin).
- **Damage value usulan**: Vancomisin ke VRE = 0 (definisi "resistant"). Linezolid ke VRE = 20. Linezolid ke MRSA
  (bakteri lama) = 16-20 juga, sesuai PDF.
- **Poin edukasi**: "obat andalan (Vancomycin) buat lawan bakteri Gram-positif resisten pun bisa dilawan balik" —
  pasangan yang paling pas buat nutup narasi soal MRSA/Vancomisin yang udah ada dari awal.
- **Stage**: HAP (Enterococcus juga penyebab infeksi nosokomial) atau UTI (VRE juga penyebab catheter-associated
  UTI di dunia nyata).

### #4 — Clarithromycin ↔ Helicobacter pylori

- **Kaitan ke roster lama**: PDF eksplisit nyebut "Helicobacter pylori infection, and duodenal ulcer disease (as
  part of combination therapy)" di indikasi Amoxicillin (hlm. 7), plus "Helicobacter pylori" ada di daftar Target
  Bacteria Amoxicillin. Clarithromycin sendiri satu keluarga makrolid sama Azitromisin yang udah ada.
- **Fakta klinis terkini**: standar terapi H. pylori itu **triple therapy** — PPI + Amoxicillin + Clarithromycin,
  14 hari. Artinya kasus ini secara natural butuh 2 antibiotik SEKALIGUS (Amoxicillin yang udah ada + Clarithromycin
  yang baru) — bisa jadi kasus edukasi soal "kenapa kadang butuh kombinasi obat, bukan cuma 1."
  Catatan: Clarithromycin gak boleh dipakai kalau resistensi lokal >15% — poin bagus lain soal AMR bikin pilihan
  obat makin terbatas dari waktu ke waktu.
- **Damage value usulan**: Amoxicillin ke H. pylori = 12 (perlu dikombo, gak cukup sendirian). Clarithromycin ke
  H. pylori = 16 (juga perlu dikombo). Kalau mekanik game belum support "harus 2 obat sekaligus", bisa disederhanain
  jadi Clarithromycin = 20 standalone dulu.
- **Stage**: kategori baru "Gastrointestinal Infection" (H. pylori nyebabin gastritis/ulkus, beda dari 6 kategori
  lama yang semuanya respirasi/UTI).

### #5 — Penicillin V ↔ Streptococcus pyogenes (Group A Strep / GABHS)

- **Kaitan ke roster lama**: ini sebenernya **nutup gap** di PDF sendiri — halaman 2 (deskripsi Acute Pharyngitis)
  udah bilang penyebab utamanya itu "group A β-hemolytic Streptococcus (GABHS)", TAPI tabel kesimpulan (hlm. 17)
  malah nulis "Streptococcus pneumoniae" buat baris Acute Faringitis — beda bakteri (S. pyogenes ≠ S. pneumoniae).
  Nambahin S. pyogenes sebagai karakter sendiri sekalian betulin ketidaksesuaian kecil ini.
- **Fakta klinis terkini**: Penicillin V (bukan Amoxicillin) yang jadi "preferred" first-line per ACP/AAP/IDSA buat
  GABHS pharyngitis, walau Amoxicillin (yang udah ada di roster) juga tetep first-line alternatif yang setara.
- **Damage value usulan**: Penicillin V ke S. pyogenes = 20. Amoxicillin ke S. pyogenes = 20 juga (sama-sama
  first-line, beda cuma sejarah/preferensi resep, bukan efektivitas).
- **Catatan**: kalau mau hemat 1 slot antibiotik, bakteri ini sebenernya BISA jalan cuma pakai Amoxicillin yang
  udah ada (gak wajib nambah Penicillin V) — tapi Penicillin V punya nilai naratif tambahan (antibiotik pertama
  yang ditemukan Fleming, bagus buat konteks sejarah AMR di skripsi).
- **Stage**: Acute Pharyngitis (udah ada slot-nya, tinggal ganti/tambah bakterinya).

### #6 — Colistin ↔ Acinetobacter baumannii (CRAB)

- **Kaitan ke roster lama**: A. baumannii sering **JUGA resisten ke Meropenem** (karbapenem yang baru ditambahin
  di #1) — CRAB (Carbapenem-Resistant Acinetobacter baumannii) itu WHO critical priority **#1** di 2024 (paling atas
  dari 24 patogen, di atas Klebsiella/E.coli 3GCRE yang udah masuk lewat #1). Colistin jadi "obat pamungkas"
  beneran yang cuma dipake kalau semua yang lain (termasuk Meropenem) udah gak mempan.
- **Fakta klinis terkini**: Colistin itu obat lama (toksik ke ginjal) yang "dibangkitkan lagi" justru KARENA AMR —
  gak ada pilihan lain buat sebagian strain CRAB. Riset terbaru nunjukin kombinasi Ampicillin-Sulbactam kadang
  lebih baik dari Colistin buat CRAB pneumonia, tapi Colistin tetep standar historis/pedagogis paling dikenal.
- **Damage value usulan**: Meropenem ke A. baumannii = 4 (resisten, kebalikan dari efektivitasnya ke bakteri lain).
  Colistin ke A. baumannii = 20. Colistin ke bakteri lain di roster = rendah semua (bukan obat lini pertama untuk
  apapun selain kasus ekstrem kayak gini — obat ini "sengaja jelek" di mana-mana kecuali di sini).
- **Poin edukasi**: ini "boss fight" paling pas buat nutup narasi AMR — nunjukin kalau bahkan obat generasi baru
  (Meropenem) pun ada batasnya, dan solusi terakhirnya adalah balik ke obat lama yang toksik. Cocok jadi level
  akhir/tersulit.
- **Stage**: HAP (sama kayak Klebsiella — HAP jadi "stage ICU" isinya bakteri paling resisten).

### #7 — Doxycycline ↔ Chlamydia trachomatis

- **Kaitan ke roster lama**: PDF nyebut "sexually transmitted infections (chlamydia)" di indikasi Azitromisin
  (hlm. 8) — jadi bakteri ini juga udah "disinggung" di materi lama.
- **Fakta klinis terkini**: CDC per update terbaru (2021+) udah **ganti** rekomendasi first-line dari Azithromycin
  ke **Doxycycline 100mg 2x/hari selama 7 hari** — soalnya studi nunjukin cure rate Doxycycline 100% vs Azithromycin
  74% buat infeksi rektal. Azithromycin sekarang cuma alternatif (buat yang alergi Doxycycline atau ibu hamil).
  Ini bagus banget buat poin edukasi "rekomendasi pengobatan berubah seiring waktu berdasarkan data resistensi baru."
- **Damage value usulan**: Doxycycline ke C. trachomatis = 20. Azitromisin (obat lama) ke C. trachomatis = 12-16
  (masih kerja, tapi udah bukan pilihan utama lagi).
- **Bonus**: Doxycycline itu kelas tetrasiklin — **kelas obat baru** yang belum ada sama sekali di roster (yang
  udah ada: beta-laktam, glikopeptida, makrolid, fluorokuinolon, folat-inhibitor, karbapenem). Nambah variasi
  mekanisme kerja yang bisa dijelasin di skripsi.
- **Stage**: sama kayak N. gonorrhoeae (#2) — kategori STI baru, atau digabung ke UTI/genitourinari.

---

## 6. Ringkasan roadmap 7 pasangan baru

| # | Bakteri baru | Antibiotik baru | Nyambung ke (roster lama) | Stage usulan |
|---|---|---|---|---|
| 1 | Klebsiella pneumoniae (ESBL) | Meropenem | Cefixime, Cipro, Levo, Amoxicillin | HAP / UTI |
| 2 | Neisseria gonorrhoeae | Ceftriaxone | Cefixime (1 keluarga), Amoxicillin | STI (baru) / UTI |
| 3 | Enterococcus faecium (VRE) | Linezolid | Vancomisin (langsung kebal ke ini) | HAP / UTI |
| 4 | Helicobacter pylori | Clarithromycin | Azitromisin (1 keluarga), Amoxicillin (kombo) | GI (baru) |
| 5 | Streptococcus pyogenes (GABHS) | Penicillin V | Amoxicillin (sama efektif) | Acute Pharyngitis (betulin gap PDF) |
| 6 | Acinetobacter baumannii (CRAB) | Colistin | Meropenem (langsung kebal ke ini) | HAP (boss fight) |
| 7 | Chlamydia trachomatis | Doxycycline | Azitromisin (obat lama, kini alternatif) | STI (baru) |

**Belum dikerjain** (sesuai instruksi — fokus planning dulu): nilai numerik full 14×14 (196 sel) buat SEMUA
kombinasi bakteri×antibiotik baru vs lama. Yang di atas cuma pasangan "utama" per karakter baru (yang punya dasar
klinis jelas) — sel-sel lain yang gak punya relevansi klinis nyata (misal: Colistin vs Streptococcus pneumoniae)
butuh diputusin pake angka default yang wajar aja pas mulai implementasi, bukan riset satu-satu.

---

## 7. Stage 7–13 (mandiri, format sama kayak yang lama)

Tiap stage = 1 kasus penyakit dari 7 pasangan di atas, ditulis dengan format sama kayak halaman "Deskripsi
berdasarkan penyakit" di PDF (deskripsi singkat penyakitnya, bakteri penyebab, antibiotik yang efektif). Gak ada
lagi usaha nyambungin ke kategori Upper/Lower Respiration Infection atau UTI yang lama — masing-masing stage
punya kasusnya sendiri, walaupun secara pharmacology tetep ada kaitan ke antibiotik lama (dijelasin di bagian
5 di atas).

### Stage 7 — Klebsiella pneumoniae (ESBL) × Meropenem

**Kasus: Infeksi Enterobacterales Penghasil ESBL (mis. Hospital-Acquired Pneumonia / UTI berat)**
Klebsiella pneumoniae adalah bakteri Gram-negatif penyebab umum pneumonia nosokomial dan infeksi saluran kemih,
terutama pada pasien rawat inap. Sebagian strain memproduksi enzim ESBL (*Extended-Spectrum Beta-Lactamase*) yang
menonaktifkan hampir semua antibiotik golongan beta-laktam (penisilin & sefalosporin), sehingga masuk kategori
*critical priority* di WHO Bacterial Priority Pathogens List 2024.
**Antibiotik pilihan**: Meropenem (karbapenem — lini pertama untuk strain ESBL).

### Stage 8 — Neisseria gonorrhoeae × Ceftriaxone

**Kasus: Gonore (Infeksi Menular Seksual)**
Neisseria gonorrhoeae adalah diplokokus Gram-negatif penyebab gonore, salah satu infeksi menular seksual paling
umum di dunia. Bisa menginfeksi uretra/serviks, rektum, dan tenggorokan (gonore faringeal, lewat oral seks).
Resistensi terhadap berbagai antibiotik terus meningkat, membuat WHO memasukkannya ke daftar prioritas tinggi.
**Antibiotik pilihan**: Ceftriaxone dosis tunggal (satu-satunya rejimen lini pertama yang direkomendasikan CDC
sejak 2020 — Cefixime oral cuma jadi alternatif kalau suntikan gak memungkinkan).

### Stage 9 — Enterococcus faecium (VRE) × Linezolid

**Kasus: Infeksi VRE (Vancomycin-Resistant Enterococcus)**
Enterococcus faecium adalah bakteri Gram-positif yang biasanya hidup di usus, tapi bisa menyebabkan infeksi
serius (bakteremia, endokarditis, ISK terkait kateter) terutama pada pasien rawat inap lama. Strain VRE resisten
total terhadap Vancomycin — obat yang biasanya jadi andalan terakhir untuk infeksi Gram-positif berat (termasuk
MRSA) — sehingga jadi salah satu ancaman resistensi yang paling serius di rumah sakit.
**Antibiotik pilihan**: Linezolid (oxazolidinone — lini pertama untuk VRE, FDA-approved).

### Stage 10 — Helicobacter pylori × Clarithromycin

**Kasus: Gastritis Kronis / Tukak Lambung (Peptic Ulcer Disease)**
Helicobacter pylori adalah bakteri spiral Gram-negatif yang hidup di lapisan mukosa lambung dan merupakan
penyebab utama gastritis kronis, tukak lambung/duodenum, bahkan faktor risiko kanker lambung. Berbeda dari
bakteri lain di roster, H. pylori butuh terapi kombinasi (bukan 1 obat tunggal) karena lingkungan asam lambung
sulit ditembus.
**Antibiotik pilihan**: Clarithromycin + Amoxicillin (terapi kombinasi standar 14 hari bersama obat penekan asam
lambung/PPI). Catatan: Clarithromycin gak dianjurkan kalau resistensi lokal >15% — makin sulit dari waktu ke
waktu, poin edukasi AMR yang relevan.

### Stage 11 — Streptococcus pyogenes (GABHS) × Penicillin V

**Kasus: Faringitis Streptokokus ("Strep Throat")**
Streptococcus pyogenes (Group A Beta-Hemolytic Streptococcus/GABHS) adalah penyebab bakteri utama radang
tenggorokan akut, terutama pada anak-anak. Kalau gak diobati, bisa berkomplikasi jadi demam rematik atau
glomerulonefritis. Ini beda dari Streptococcus pneumoniae yang udah ada di roster (organ target & penyakit beda,
walau masih satu genus).
**Antibiotik pilihan**: Penicillin V (lini pertama menurut ACP/AAP/IDSA — Amoxicillin yang udah ada di roster
sebenarnya sama efektifnya, tapi Penicillin V "aslinya").

### Stage 12 — Acinetobacter baumannii (CRAB) × Colistin

**Kasus: Infeksi Multidrug-Resistant di ICU (boss stage)**
Acinetobacter baumannii adalah bakteri Gram-negatif oportunistik yang bertahan hidup di lingkungan rumah sakit
(alat medis, permukaan) dan menginfeksi pasien kritis. Strain CRAB (Carbapenem-Resistant A. baumannii) adalah
patogen dengan prioritas **tertinggi** di WHO Bacterial Priority Pathogens List 2024 — bahkan resisten terhadap
Meropenem yang baru ditambahkan di Stage 7. Colistin, antibiotik lama yang sempat ditinggalkan karena toksik ke
ginjal, terpaksa dipakai lagi karena hampir gak ada pilihan lain.
**Antibiotik pilihan**: Colistin (polymyxin — obat pamungkas terakhir).

### Stage 13 — Chlamydia trachomatis × Doxycycline

**Kasus: Klamidia (Infeksi Menular Seksual)**
Chlamydia trachomatis adalah bakteri intraseluler obligat penyebab infeksi menular seksual paling umum secara
global, sering tanpa gejala sehingga gampang menyebar tanpa disadari. Kalau gak diobati bisa menyebabkan
komplikasi serius (infertilitas, radang panggul).
**Antibiotik pilihan**: Doxycycline (rekomendasi CDC terbaru sejak 2021, menggantikan Azithromycin yang udah ada
di roster — cure rate 100% vs 74% khususnya untuk infeksi rektal). Poin edukasi: rekomendasi pengobatan bisa
berubah seiring waktu berdasarkan data resistensi terbaru.

---

**Ringkasan urutan stage baru**: Stage 7 (Klebsiella/Meropenem) → 8 (Gonore/Ceftriaxone) → 9 (VRE/Linezolid) →
10 (H. pylori/Clarithromycin) → 11 (Strep Throat/Penicillin V) → 12 (CRAB/Colistin, "boss") → 13
(Klamidia/Doxycycline). Urutan ini bisa diubah bebas — gak ada dependency teknis antar stage, cuma saran
supaya makin ke belakang makin susah/makin resisten bakterinya (Stage 12 CRAB sengaja diposisikan sebagai
puncak kesulitan karena bahkan Meropenem dari Stage 7 gak mempan ke situ).

Sources: [CDC drug-resistant gonorrhea](https://www.cdc.gov/gonorrhea/hcp/drug-resistant/index.html), [CDC gonorrhea clinical treatment](https://www.cdc.gov/gonorrhea/hcp/clinical-care/index.html), [CDC chlamydia STI treatment guidelines](https://www.cdc.gov/std/treatment-guidelines/chlamydia.htm), [Doxycycline vs azithromycin rectal chlamydia RCT](https://www.sciencedirect.com/science/article/abs/pii/S1473309922001487), [VRE treatment guidance (UNC)](https://www.med.unc.edu/casp/wp-content/uploads/sites/563/2023/06/CASP-VRE-Guideline-Jan-2023.pdf), [Daptomycin vs linezolid for VRE bacteremia](https://www.ncbi.nlm.nih.gov/pmc/articles/PMC4269951/), [H. pylori triple therapy PPI+clarithromycin](https://pmc.ncbi.nlm.nih.gov/articles/PMC8406940/), [ACG H. pylori treatment update](https://www.aafp.org/pubs/afp/issues/2018/0115/p135.html), [CRAB WHO critical priority + colistin](https://pmc.ncbi.nlm.nih.gov/articles/PMC11625361/), [Ampicillin-sulbactam vs colistin for CRAB pneumonia](https://journals.asm.org/doi/10.1128/aac.01880-24), [Streptococcal pharyngitis rapid evidence review](https://www.aafp.org/afp/2024/0400/streptococcal-pharyngitis), [GABHS penicillin V first-line](https://www.ncbi.nlm.nih.gov/pmc/articles/PMC5020538/)
