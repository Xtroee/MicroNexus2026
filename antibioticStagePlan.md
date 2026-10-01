# Antibiotic Stage Plan — Stage 7-13 (roster final, bukan placeholder)

Planning buat urutan kerja: tiap stage cuma butuh **1 antibiotik "hero"** (yang jadi jawaban benar) beneran baru
digambar — 3 slot card sisanya diisi antibiotik yang **udah pernah ada** (dari roster lama ATAU stage baru
sebelumnya), jadi gak nambah beban gambar. Bakteri sekunder (kalau ada) juga dipilih dari yang udah pernah
digambar, buat alasan yang sama.

Semua nilai efektivitas dari `docs/antibiotic-bacteria-matrix-full-14x14.md`. Legenda: 20 = full effective,
16 = strong, 12 = moderate, 8 = partial-low, 4 = minimal, 0 = no effect.

**Status karakter saat ini**: 7 antibiotik baru (Meropenem, Ceftriaxone, Linezolid, Clarithromycin, Penicillin V,
Colistin, Doxycycline) ✅ udah digambar (gaya slime-scout GPT). 7 bakteri baru ❌ belum ada art sama sekali —
itu kerjaan berikutnya setelah ini.

---

## Stage 7 — Klebsiella pneumoniae (ESBL)

| Slot | Isi | Nilai vs Klebsiella ESBL | Kenapa |
|---|---|---|---|
| **Hero** | **Meropenem** 🆕 | 20 | Lini pertama beneran buat ESBL |
| Card 2 | Cotrimoxazole (lama) | 8 | Paling tinggi di antara obat lama, "Klebsiella spp." eksplisit di target-nya |
| Card 3 | Cefixime (lama) | 4 | Profilnya sendiri nyebut Klebsiella pneumoniae sebagai target — tapi cuma buat strain BIASA, gak ESBL. Bagus buat lesson "dulu kerja, sekarang enggak" |
| Card 4 | Ciprofloxacin (lama) | 4 | Fluorokuinolon biasanya kuat ke Gram-negatif, tapi ESBL sering ikut resisten juga |
| Bakteri utama | Klebsiella pneumoniae (ESBL) 🆕 | — | — |
| Bakteri sekunder (opsional) | E. coli (lama, warna Pink) | Meropenem = 20 juga | ESBL itu masalah Enterobacterales secara umum, bukan cuma Klebsiella — E. coli cocok jadi pasangan (udah dipakai jadi placeholder Stage 7 juga, jadi tinggal lanjut) |

---

## Stage 8 — Neisseria gonorrhoeae

| Slot | Isi | Nilai vs N. gonorrhoeae | Kenapa |
|---|---|---|---|
| **Hero** | **Ceftriaxone** 🆕 | 20 | Satu-satunya regimen lini pertama CDC (dosis tunggal) |
| Card 2 | Cefixime (lama) | 12 | Alternatif oral kalau suntikan gak memungkinkan — beda rute, sama keluarga sefalosporin, lesson "satu keluarga, beda kekuatan" |
| Card 3 | Azitromisin (lama) | 8 | Dulu dipasangin sama Ceftriaxone (dual therapy), sekarang udah dihentiin CDC — lesson "rekomendasi berubah" |
| Card 4 | Meropenem 🆕 (dari Stage 7) | 8 | Teknis kerja tapi gak pernah dipakai klinis buat ini — "senjata kuat gak selalu yang tepat" |
| Bakteri utama | Neisseria gonorrhoeae 🆕 | — | — |
| Bakteri sekunder (opsional) | Moraxella catarrhalis (lama) | Ceftriaxone = 16 | Sama-sama diplokokus Gram-negatif (morfologi mirip), udah dipakai placeholder-nya juga |

---

## Stage 9 — Enterococcus faecium (VRE)

| Slot | Isi | Nilai vs VRE | Kenapa |
|---|---|---|---|
| **Hero** | **Linezolid** 🆕 | 20 | Lini pertama FDA-approved buat VRE |
| Card 2 | Vancomisin (lama) | **0** | **WAJIB ada** — ini poin edukasi utama stage-nya: obat andalan G+ yang udah dipercaya dari Stage 1-6, di sini GAGAL TOTAL (definisi "resistant") |
| Card 3 | Ciprofloxacin (lama) | 4 | Minimal, gak diandalkan |
| Card 4 | Cotrimoxazole (lama) | 4 | Ada aktivitas in-vitro tapi gak reliable klinis |
| Bakteri utama | Enterococcus faecium VRE 🆕 | — | — |
| Bakteri sekunder (opsional) | Staphylococcus aureus / MSSA (lama) | Vancomisin = 20 di MSSA | Kontras kuat: Vancomisin di MSSA (sebelah) = full effective, di VRE = 0. Bikin lesson-nya keliatan jelas side-by-side |

---

## Stage 10 — Helicobacter pylori

| Slot | Isi | Nilai vs H. pylori | Kenapa |
|---|---|---|---|
| **Hero** | **Clarithromycin** 🆕 | 20 | Komponen utama triple therapy standar |
| Card 2 | Amoxicillin (lama) | 12 | Partner kombinasi asli (triple therapy = PPI + Amoxicillin + Clarithromycin) |
| Card 3 | Levofloksasin (lama) | 16 | Regimen salvage/rescue kalau triple therapy gagal — nilainya malah lebih tinggi dari Amoxicillin, menarik buat didiskusiin |
| Card 4 | Azitromisin (lama) | 4 | Satu keluarga makrolid tapi jauh lebih lemah — kontras sama Clarithromycin |
| Bakteri utama | Helicobacter pylori 🆕 | — | — |
| Bakteri sekunder | — (gak ada yang cocok secara klinis, niche lambung unik) | — | Stage ini disaranin 1-bakteri aja, jangan dipaksain nambah |

---

## Stage 11 — Streptococcus pyogenes (GABHS / Strep Throat)

| Slot | Isi | Nilai vs S. pyogenes | Kenapa |
|---|---|---|---|
| **Hero** | **Penicillin V** 🆕 | 20 | Lini pertama ACP/AAP/IDSA, gold standard historis |
| Card 2 | Amoxicillin (lama) | 20 | Sengaja SAMA nilainya — lesson "kadang ada lebih dari 1 jawaban benar", bukan Penicillin V doang yang valid |
| Card 3 | Cotrimoxazole (lama) | **0** | Gap klinis terkenal — TMP-SMX TIDAK direkomendasikan buat strep throat sama sekali |
| Card 4 | Ciprofloxacin (lama) | 4 | Fluorokuinolon (terutama generasi cipro) lemah ke Streptococcus |
| Bakteri utama | Streptococcus pyogenes 🆕 | — | — |
| Bakteri sekunder (opsional) | Streptococcus pneumoniae (lama) | Penicillin V = 16 di S. pneumoniae | Satu genus, tapi S. pneumoniae lebih "gampang" (banyak obat lama masih kerja) vs S. pyogenes lebih spesifik — lesson "beda spesies, beda karakter" |

---

## Stage 12 — Acinetobacter baumannii (CRAB) — boss stage

| Slot | Isi | Nilai vs CRAB | Kenapa |
|---|---|---|---|
| **Hero** | **Colistin** 🆕 | 20 | Satu-satunya obat pamungkas di roster buat CRAB |
| Card 2 | Meropenem 🆕 (dari Stage 7) | **0** | **WAJIB ada** — poin klimaks narasi: senjata terkuat dari Stage 7 (karbapenem) GAGAL TOTAL di sini (definisi "Carbapenem-Resistant") |
| Card 3 | Levofloksasin (lama) | 4 | Sisa aktivitas minim |
| Card 4 | Ciprofloxacin (lama) | 4 | Sama, minim |
| Bakteri utama | Acinetobacter baumannii CRAB 🆕 | — | — |
| Bakteri sekunder (opsional) | Pseudomonas aeruginosa (lama) | Colistin = 8 di Pseudomonas | Dua-duanya "ICU MDR Gram-negatif", udah dipakai placeholder-nya juga, Colistin masih ada aktivitas parsial di sini (beda dari CRAB yang full) |

---

## Stage 13 — Chlamydia trachomatis

> **Catatan**: scene `Level 13 Stage 1.unity` **belum dibuat** (beda dari Stage 7-12 yang scene-nya udah ada di
> Level Map/Build Settings). Planning di bawah buat karakter/bakteri-nya aja, implementasi scene nyusul.

| Slot | Isi | Nilai vs C. trachomatis | Kenapa |
|---|---|---|---|
| **Hero** | **Doxycycline** 🆕 | 20 | Rekomendasi CDC terbaru (2021+), cure rate 100% vs Azitromisin 74% (infeksi rektal) |
| Card 2 | Azitromisin (lama) | 16 | **WAJIB ada** — poin edukasi utama: dulu first-line, MASIH kuat (16, bukan 0), tapi Doxycycline sekarang lebih disarankan. Lesson "rekomendasi berubah karena data baru, bukan karena obat lama jadi gak berguna" |
| Card 3 | Levofloksasin (lama) | 12 | Alternatif dengan bukti efikasi di beberapa guideline |
| Card 4 | Amoxicillin (lama) | 8 | Alternatif khusus populasi tertentu (mis. kehamilan) |
| Bakteri utama | Chlamydia trachomatis 🆕 | — | — |
| Bakteri sekunder (opsional) | Neisseria gonorrhoeae 🆕 (dari Stage 8) | — | Co-infeksi gonore+klamidia itu skenario klinis nyata & umum — juga nutup "circle" tema Infeksi Menular Seksual bareng Stage 8 |

---

## Ringkasan urutan kerja karakter (udah beres semua)

Meropenem → Ceftriaxone → Linezolid → Clarithromycin → Penicillin V → Colistin → Doxycycline — **7/7 selesai**,
lihat `docs/character-design-v4-gpt-slime-pivot.md`.

## Ringkasan urutan kerja bakteri (belum mulai — rekomendasi urutan)

Ngikutin urutan stage biar bisa langsung dites bareng antibiotik hero-nya yang udah jadi:

1. Klebsiella pneumoniae (ESBL) — Stage 7
2. Neisseria gonorrhoeae — Stage 8
3. Enterococcus faecium (VRE) — Stage 9
4. Helicobacter pylori — Stage 10
5. Streptococcus pyogenes (GABHS) — Stage 11
6. Acinetobacter baumannii (CRAB) — Stage 12
7. Chlamydia trachomatis — Stage 13

Belum ada style DNA buat bakteri baru (beda dari antibiotik yang udah punya template slime-scout) — warna/bentuk
masih sebatas rencana di `docs/antibiotic-bacteria-matrix-v2.md`, belum ada contoh gambar referensi kayak yang
antibiotik udah punya. Kemungkinan perlu sesi desain terpisah buat nentuin "style DNA" bakteri dulu sebelum mulai
generate 7 ini.
