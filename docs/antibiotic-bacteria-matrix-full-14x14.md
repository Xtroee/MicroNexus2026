# Antibiotic × Bacteria — Referensi Lengkap 14×14

Sumber: `MATERI GAME-1.pdf` (matriks 7×7 asli, profil dasar) + `MATERI GAME-2.pdf` (profil antibiotik/bakteri format
lengkap: Class/Indications/Target Bacteria/ADR/Link untuk antibiotik; Brief characteristics/Habitat/Major
diseases/Effective antibiotics untuk bakteri) + roadmap 7 pasangan baru dari `antibiotic-bacteria-matrix-v2.md`
(hasil riset klinis sesi sebelumnya, CDC/WHO/ACG). Dokumen ini menyatukan semuanya jadi referensi tunggal untuk
14 antibiotik × 14 bakteri, ditulis dengan format sama persis kayak `MATERI GAME-2.pdf`.

**Skala nilai** (konvensi kode, `0–20`): `0` = immune/no effect · `4` = minimal · `8` = partial-low ·
`12` = partial-moderate · `16` = strong · `20` = full effective. Setara skala `+` PDF: `-`→0, `+`→4, `++`→8,
`+++`→12, `++++`→16, `+++++`→20.

7 pasangan lama (Stage 1-6) dan 7 pasangan baru (Stage 7-13) — urutan sama kayak `stage-implementation-status.md`.

---

## 1. Matriks lengkap 14×14

| Antibiotik \ Bakteri | S. pneu (G+) | MSSA (G+) | MRSA (G+) | M. cat (G−) | H. inf (G−) | E. coli (G−) | P. aer (G−) | K. pneu ESBL (G−) | N. gono (G−) | VRE (G+) | H. pylori (G−) | S. pyogenes (G+) | CRAB (G−) | C. trachomatis |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| Amoxicillin | 20 | 16 | 0 | 4 | 8 | 4 | 0 | 0 | 4 | 0 | 12 | 20 | 0 | 8 |
| Cefixime | 4 | 4 | 0 | 12 | 16 | 20 | 0 | 4 | 12 | 0 | 0 | 12 | 0 | 0 |
| Vancomisin | 20 | 20 | 20 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 20 | 0 | 0 |
| Azitromisin | 16 | 12 | 0 | 12 | 8 | 0 | 0 | 4 | 8 | 0 | 4 | 12 | 0 | 16 |
| Ciprofloxacin | 4 | 4 | 0 | 4 | 12 | 20 | 20 | 4 | 4 | 4 | 8 | 4 | 4 | 4 |
| Levofloksasin | 20 | 8 | 8 | 8 | 8 | 12 | 12 | 4 | 4 | 4 | 16 | 12 | 4 | 12 |
| Cotrimoxazole | 8 | 12 | 12 | 8 | 8 | 16 | 0 | 8 | 0 | 4 | 0 | 0 | 4 | 0 |
| **Meropenem** | 12 | 16 | 0 | 16 | 20 | 20 | 20 | 20 | 8 | 0 | 0 | 20 | 0 | 0 |
| **Ceftriaxone** | 20 | 12 | 0 | 16 | 20 | 20 | 0 | 4 | 20 | 0 | 0 | 20 | 0 | 0 |
| **Linezolid** | 16 | 16 | 20 | 0 | 0 | 0 | 0 | 0 | 0 | 20 | 0 | 16 | 0 | 0 |
| **Clarithromycin** | 16 | 8 | 0 | 12 | 8 | 0 | 0 | 0 | 4 | 0 | 20 | 12 | 0 | 8 |
| **Penicillin V** | 16 | 0 | 0 | 0 | 4 | 0 | 0 | 0 | 0 | 0 | 0 | 20 | 0 | 0 |
| **Colistin** | 0 | 0 | 0 | 4 | 4 | 4 | 8 | 8 | 0 | 0 | 0 | 0 | 20 | 0 |
| **Doxycycline** | 12 | 8 | 12 | 12 | 8 | 4 | 0 | 4 | 4 | 4 | 8 | 4 | 4 | 20 |

Baris **bold** = 7 antibiotik baru (ditambahkan sesi ini beserta 6 sebelumnya). Kolom baru: K. pneumoniae ESBL,
N. gonorrhoeae, VRE, H. pylori, S. pyogenes, CRAB, C. trachomatis.

### Catatan cepat per antibiotik baru (kenapa nilainya segini)

- **Meropenem**: broad-spectrum karbapenem, tapi tetap 0 ke MRSA (resistensi kelas beta-laktam, bukan cuma
  methicillin) dan 0 ke CRAB (definisinya "Carbapenem-Resistant") dan 0 ke VRE (karbapenem gak cover enterococcus).
- **Ceftriaxone**: satu keluarga sefalosporin gen-3 sama Cefixime, tapi first-line buat gonore (20, CDC single-agent
  recommendation) sementara Cefixime cuma alternatif (12). Sama sekali gak cover Pseudomonas (beda dari sefalosporin
  anti-pseudomonas kayak ceftazidime/cefepime yang gak ada di roster).
- **Linezolid**: satu-satunya obat Gram-positif murni di roster baru — 0 total ke semua bakteri Gram-negatif.
  First-line VRE (20) dan tetap kuat ke MRSA (20, konsisten sama PDF).
- **Clarithromycin**: obat utama H. pylori (20, bareng Amoxicillin). Spektrum makrolide mirip Azitromisin tapi
  gak sekuat itu ke C. trachomatis (Azitromisin masih obat lama buat itu, Clarithromycin bukan pilihan utama).
- **Penicillin V**: paling sempit spektrumnya dari semua — 0 ke hampir semua kecuali S. pneumoniae (16, penisilin
  masih rentan sebagian) dan S. pyogenes (20, gold standard). Bahkan MSSA = 0 karena penisilin biasa gak tahan
  beta-laktamase stafilokokus.
- **Colistin**: sengaja rendah ke hampir semua bakteri (termasuk Gram-negatif biasa kayak E. coli/H. influenzae —
  4, teknis bisa tapi gak pernah dipakai klinis buat itu) kecuali CRAB (20, satu-satunya alasan obat ini ada di
  roster). 0 total ke semua Gram-positif (polymyxin gak nembus dinding sel Gram-positif).
- **Doxycycline**: kelas obat baru (tetrasiklin) — spektrum medium-luas tapi gak ada yang benar-benar dominan
  kecuali C. trachomatis (20, first-line CDC 2021+, cure rate 100% vs Azitromisin 74% infeksi rektal).

---

## 2. Deskripsi berdasarkan penyakit

Format sama kayak `MATERI GAME-2.pdf` halaman 2-6. 6 kasus lama dari PDF, 7 kasus baru (Stage 7-13) dari riset
sesi sebelumnya (`antibiotic-bacteria-matrix-v2.md` bagian 7).

### ACUTE OTITIS MEDIA
Inflamasi telinga tengah. Ada 3 subtipe otitis media: akut, dengan efusi, dan kronis — akut yang paling relevan
untuk terapi antibiotik. Patogen bakteri umum: *Streptococcus pneumoniae*, *Haemophilus influenzae* nontypeable,
dan *Moraxella catarrhalis*.
**Antibiotik pilihan**: Amoxicillin, Azitromisin, Cefixime.

### ACUTE PHARYNGITIS
Infeksi akut orofaring/nasofaring, 6% kunjungan anak ke dokter primer per tahun. Mayoritas kasus disebabkan virus,
tapi *group A β-hemolytic Streptococcus* (GABHS/*S. pyogenes*) adalah penyebab bakteri utama — dikenal sebagai
"strep throat" — dan satu-satunya penyebab faringitis akut yang butuh terapi antibiotik secara rutin.
**Antibiotik pilihan**: Penicillin V (lini pertama), Amoxicillin (sama efektif).

### CHRONIC BRONCHITIS
Disebabkan berbagai faktor (asap rokok, paparan debu/polutan, faktor genetik, infeksi bakteri/virus). Ciri khasnya
batuk kronis dengan produksi sputum berlebih. Patogen umum: *Haemophilus influenzae*, *Moraxella catarrhalis*,
*Streptococcus pneumoniae*.
**Antibiotik pilihan**: Azitromisin, Levofloksasin, Ciprofloxacin.

### PNEUMONIA
**1. Community-Acquired Pneumonia (CAP)** — pneumonia di luar rumah sakit/<48 jam setelah masuk RS. Penyebab
tersering biasanya virus, tapi patogen bakteri paling menonjol adalah *S. pneumoniae* (~35% kasus akut), diikuti
*H. influenzae* (2.5-45%).
**Antibiotik pilihan**: Amoxicillin, Azitromisin, Levofloksasin.

**2. Hospital-Acquired Pneumonia (HAP)** — pneumonia >48 jam setelah masuk RS, biasanya pada pasien kritis, sering
disebabkan bakteri gram-negatif aerobik atau *S. aureus*, dan lebih mungkin MDR. *P. aeruginosa* & *Acinetobacter*
spp. penyebab paling umum (~25-45%), diikuti *K. pneumoniae* & *E. coli* (13-20%), *S. aureus* (12-21%, separuhnya
MRSA).
**Antibiotik pilihan**: Vancomisin (MRSA), Levofloksasin, Ciprofloxacin, Meropenem (ESBL), Colistin (CRAB — kasus
paling ekstrem).

### URINARY TRACT INFECTIONS
Keberadaan mikroorganisme di saluran kemih yang gak bisa dijelaskan oleh kontaminasi. *Escherichia coli* penyebab
tersering infeksi tanpa komplikasi (80-90% kasus komunitas). Penyebab lain: *S. saprophyticus*, *K. pneumoniae*,
*Proteus* spp., *P. aeruginosa*, *Enterococcus* spp.
**Antibiotik pilihan**: Ciprofloxacin, Levofloksasin, Cotrimoxazole, Cefixime, Meropenem (kalau ESBL).

### INFEKSI ESBL (Stage 7 — mis. HAP/UTI berat)
*Klebsiella pneumoniae* adalah bakteri Gram-negatif penyebab umum pneumonia nosokomial & ISK, terutama pasien
rawat inap. Sebagian strain memproduksi enzim ESBL (*Extended-Spectrum Beta-Lactamase*) yang menonaktifkan hampir
semua antibiotik beta-laktam — masuk kategori *critical priority* WHO Bacterial Priority Pathogens List 2024.
**Antibiotik pilihan**: Meropenem (karbapenem, lini pertama strain ESBL).

### GONORE (Stage 8 — Infeksi Menular Seksual)
*Neisseria gonorrhoeae* adalah diplokokus Gram-negatif penyebab gonore, salah satu IMS paling umum di dunia. Bisa
menginfeksi uretra/serviks, rektum, dan tenggorokan. Resistensi antibiotik terus meningkat, masuk daftar prioritas
tinggi WHO.
**Antibiotik pilihan**: Ceftriaxone dosis tunggal (satu-satunya regimen lini pertama CDC sejak 2020; Cefixime oral
cuma alternatif kalau suntikan gak memungkinkan).

### INFEKSI VRE (Stage 9 — Vancomycin-Resistant Enterococcus)
*Enterococcus faecium* biasanya hidup di usus, tapi bisa menyebabkan infeksi serius (bakteremia, endokarditis,
ISK terkait kateter) pada pasien rawat inap lama. Strain VRE resisten total terhadap Vancomycin — obat andalan
terakhir untuk infeksi Gram-positif berat (termasuk MRSA).
**Antibiotik pilihan**: Linezolid (oxazolidinone, lini pertama VRE, FDA-approved).

### GASTRITIS KRONIS / TUKAK LAMBUNG (Stage 10)
*Helicobacter pylori* adalah bakteri spiral Gram-negatif di mukosa lambung, penyebab utama gastritis kronis, tukak
lambung/duodenum, bahkan faktor risiko kanker lambung. Butuh terapi kombinasi (bukan 1 obat) karena lingkungan
asam lambung sulit ditembus.
**Antibiotik pilihan**: Clarithromycin + Amoxicillin (terapi kombinasi standar 14 hari bersama PPI).

### FARINGITIS STREPTOKOKUS / "STREP THROAT" (Stage 11)
*Streptococcus pyogenes* (GABHS) adalah penyebab bakteri utama radang tenggorokan akut, terutama anak-anak. Kalau
gak diobati bisa berkomplikasi jadi demam rematik/glomerulonefritis. Beda dari *S. pneumoniae* yang udah ada di
roster (organ target & penyakit beda, walau satu genus).
**Antibiotik pilihan**: Penicillin V (lini pertama ACP/AAP/IDSA; Amoxicillin sama efektif).

### INFEKSI CRAB (Stage 12 — boss stage, ICU)
*Acinetobacter baumannii* adalah bakteri Gram-negatif oportunistik yang bertahan di lingkungan RS. Strain CRAB
(*Carbapenem-Resistant A. baumannii*) adalah patogen prioritas **tertinggi** WHO 2024 — resisten bahkan ke
Meropenem. Colistin, obat lama yang sempat ditinggalkan (toksik ginjal), terpaksa dipakai lagi.
**Antibiotik pilihan**: Colistin (polymyxin, obat pamungkas terakhir).

### KLAMIDIA (Stage 13 — Infeksi Menular Seksual)
*Chlamydia trachomatis* adalah bakteri intraseluler obligat penyebab IMS paling umum secara global, sering tanpa
gejala. Kalau gak diobati bisa menyebabkan infertilitas, radang panggul.
**Antibiotik pilihan**: Doxycycline (rekomendasi CDC terbaru sejak 2021, cure rate 100% vs Azitromisin 74% infeksi
rektal).

---

## 3. Profil Antibiotik (14)

### Amoxicillin
**Class**: β-lactam antibiotic; aminopenicillin
**Indications**: Otitis media akut, faringitis & tonsilitis, infeksi saluran napas bawah, infeksi kulit, ISK,
gonore tanpa komplikasi, tifoid, infeksi *Helicobacter pylori* & tukak duodenum (terapi kombinasi)
**Target Bacteria**: *Streptococcus pneumoniae*, *Streptococcus pyogenes*, *Enterococcus faecalis*, *Haemophilus
influenzae*, *Escherichia coli*, *Helicobacter pylori*
**ADR**: Mual, muntah, diare, ruam kulit, reaksi hipersensitivitas termasuk anafilaksis (jarang)
**LINK**: https://www.rxlist.com/amoxicillin/generic-drug.htm

### Cefixime
**Class**: β-lactam antibiotic; third-generation cephalosporin
**Indications**: ISK, otitis media akut, faringitis & tonsilitis, bronkitis, pneumonia komunitas ringan, tifoid,
gonore tanpa komplikasi
**Target Bacteria**: *Escherichia coli*, *Klebsiella pneumoniae*, *Proteus mirabilis*, *Haemophilus influenzae*,
*Neisseria gonorrhoeae*, *Salmonella typhi*
**ADR**: Diare, mual, muntah, nyeri perut, ruam kulit, reaksi hipersensitivitas
**LINK**: https://www.rxlist.com/suprax-drug.htm

### Vancomisin (Vancomycin)
**Class**: Glycopeptide antibiotic
**Indications**: Infeksi Gram-positif serius termasuk MRSA, endokarditis, pneumonia, infeksi tulang & sendi, kolitis
terkait *Clostridioides difficile*
**Target Bacteria**: *Staphylococcus aureus* (termasuk MRSA), *Streptococcus* spp., *Enterococcus* spp. (susceptible),
*Clostridioides difficile*
**ADR**: Nefrotoksisitas, ototoksisitas, reaksi terkait infus (red man syndrome), flebitis, ruam kulit
**LINK**: https://www.rxlist.com/vancocin-drug.htm

### Azitromisin (Azithromycin)
**Class**: Macrolide antibiotic
**Indications**: ISPA atas & bawah, otitis media akut, infeksi kulit & jaringan lunak, IMS (klamidia), tifoid
**Target Bacteria**: *Streptococcus pneumoniae*, *Haemophilus influenzae*, *Mycoplasma pneumoniae*, *Chlamydia
trachomatis*, *Legionella pneumophila*
**ADR**: Gangguan gastrointestinal, sakit kepala, pusing, perpanjangan interval QT
**LINK**: https://www.rxlist.com/zithromax-drug.htm

### Ciprofloxacin
**Class**: Fluoroquinolone antibiotic (generasi kedua)
**Indications**: ISK, infeksi gastrointestinal, infeksi tulang & sendi, infeksi kulit & jaringan lunak, pneumonia
Gram-negatif, antraks
**Target Bacteria**: *Escherichia coli*, *Pseudomonas aeruginosa*, *Klebsiella* spp., *Salmonella* spp., *Shigella*
spp.
**ADR**: Mual, diare, pusing, fotosensitivitas, peningkatan risiko tendinitis & ruptur tendon
**LINK**: https://www.rxlist.com/cipro-drug.htm

### Levofloksasin (Levofloxacin)
**Class**: Fluoroquinolone antibiotic (generasi ketiga, "respiratory")
**Indications**: Pneumonia komunitas, sinusitis bakterial akut, eksaserbasi akut bronkitis kronis, ISK, infeksi
kulit & jaringan lunak
**Target Bacteria**: *Streptococcus pneumoniae*, *Haemophilus influenzae*, *Escherichia coli*, *Klebsiella
pneumoniae*, *Legionella pneumophila*
**ADR**: Gangguan gastrointestinal, pusing, insomnia, perpanjangan interval QT, peningkatan risiko ruptur tendon
**LINK**: https://www.rxlist.com/levaquin-drug.htm

### Cotrimoxazole (Trimethoprim–Sulfamethoxazole)
**Class**: Combination antibiotic; folate synthesis inhibitor
**Indications**: ISK, diare bakterial, pneumonia *Pneumocystis jirovecii*, infeksi kulit & jaringan lunak,
toksoplasmosis
**Target Bacteria**: *Escherichia coli*, *Klebsiella* spp., *Staphylococcus aureus*, *Pneumocystis jirovecii*,
*Toxoplasma gondii*
**ADR**: Mual, muntah, ruam kulit, sindrom Stevens–Johnson, supresi sumsum tulang
**LINK**: https://www.rxlist.com/bactrim-drug.htm

### Meropenem
**Class**: β-lactam antibiotic; carbapenem (broad-spectrum)
**Indications**: Infeksi berat/kompleks termasuk pneumonia nosokomial, infeksi intra-abdomen kompleks, ISK
kompleks, meningitis bakterial, infeksi akibat organisme Gram-negatif ESBL/MDR
**Target Bacteria**: *Klebsiella pneumoniae* (ESBL), *Escherichia coli* (ESBL), *Pseudomonas aeruginosa*,
*Haemophilus influenzae*, *Streptococcus pneumoniae*, Enterobacterales
**ADR**: Mual, muntah, diare, sakit kepala, reaksi di tempat injeksi, kejang (jarang, terkait dosis), reaksi
hipersensitivitas
**LINK**: https://www.rxlist.com/merrem-drug.htm

### Ceftriaxone
**Class**: β-lactam antibiotic; third-generation cephalosporin (injeksi)
**Indications**: Gonore, meningitis bakterial, pneumonia komunitas & nosokomial, ISK kompleks, septikemia, infeksi
kulit/jaringan lunak
**Target Bacteria**: *Neisseria gonorrhoeae*, *Streptococcus pneumoniae*, *Haemophilus influenzae*, *Moraxella
catarrhalis*, *Escherichia coli*, *Streptococcus pyogenes*
**ADR**: Diare, mual, nyeri di tempat injeksi, sludge bilier (pseudolitiasis), reaksi hipersensitivitas
**LINK**: https://www.rxlist.com/rocephin-drug.htm

### Linezolid
**Class**: Oxazolidinone antibiotic
**Indications**: Infeksi *Enterococcus faecium* vancomycin-resistant (VRE), pneumonia komunitas & nosokomial,
infeksi kulit/jaringan lunak kompleks termasuk MRSA
**Target Bacteria**: *Enterococcus faecium* (VRE), *Staphylococcus aureus* (termasuk MRSA), *Streptococcus
pneumoniae*, *Streptococcus pyogenes*
**ADR**: Trombositopenia, neuropati perifer/optik (pemakaian lama), sindrom serotonin (dengan obat serotonergik),
gangguan gastrointestinal
**LINK**: https://www.rxlist.com/zyvox-drug.htm

### Clarithromycin
**Class**: Macrolide antibiotic
**Indications**: Infeksi saluran napas, infeksi kulit, terapi kombinasi (dengan PPI + Amoxicillin) untuk eradikasi
*Helicobacter pylori*
**Target Bacteria**: *Helicobacter pylori*, *Streptococcus pneumoniae*, *Haemophilus influenzae*, *Moraxella
catarrhalis*, *Mycobacterium avium* complex
**ADR**: Gangguan gastrointestinal (rasa logam umum), perpanjangan interval QT, interaksi obat (inhibitor CYP3A4),
hepatotoksisitas (jarang)
**LINK**: https://www.rxlist.com/biaxin-drug.htm

### Penicillin V
**Class**: β-lactam antibiotic; penisilin alami (spektrum sempit)
**Indications**: Faringitis streptokokus ("strep throat"), profilaksis rekurensi demam rematik, infeksi kulit
ringan akibat streptokokus rentan
**Target Bacteria**: *Streptococcus pyogenes* (Group A Streptococcus), *Streptococcus pneumoniae* (strain rentan)
**ADR**: Mual, diare, ruam kulit, reaksi hipersensitivitas termasuk anafilaksis (jarang tapi serius)
**LINK**: https://www.rxlist.com/penicillin-vk-drug.htm

### Colistin (Polymyxin E)
**Class**: Polymyxin antibiotic (obat lini terakhir)
**Indications**: Infeksi serius akibat bakteri Gram-negatif multidrug-resistant, termasuk *Acinetobacter baumannii*
carbapenem-resistant (CRAB), saat opsi lain sudah gagal
**Target Bacteria**: *Acinetobacter baumannii* (CRAB), *Pseudomonas aeruginosa* (MDR), *Klebsiella pneumoniae*
(carbapenem-resistant)
**ADR**: Nefrotoksisitas (umum, membatasi dosis), neurotoksisitas (parestesia, pusing), depresi napas (jarang),
reaksi hipersensitivitas
**LINK**: https://www.rxlist.com/coly-mycin-m-drug.htm

### Doxycycline
**Class**: Tetracycline antibiotic
**Indications**: Infeksi *Chlamydia trachomatis*, jerawat, infeksi saluran napas, penyakit Lyme, profilaksis
malaria
**Target Bacteria**: *Chlamydia trachomatis*, *Mycoplasma pneumoniae*, *Rickettsia* spp., *Borrelia burgdorferi*
**ADR**: Fotosensitivitas, gangguan gastrointestinal, iritasi esofagus, diskolorasi gigi (anak-anak/kehamilan —
kontraindikasi), pusing (efek vestibular)
**LINK**: https://www.rxlist.com/vibramycin-drug.htm

---

## 4. Profil Bakteri (14)

### 1. Streptococcus pneumoniae (Gram-positive)
**Brief characteristics**: Diplokokus, berkapsul, alfa-hemolitik
**Habitat**: Flora normal nasofaring
**Major diseases**: Pneumonia komunitas (CAP), otitis media, sinusitis, meningitis, bakteremia
**Effective antibiotics**: Amoxicillin/Amoxicillin-clavulanate, Penicillin (jika rentan), Ceftriaxone/Cefotaxime,
Levofloxacin, Azitromisin (resistensi meningkat), Vancomycin (kasus berat/resisten)

### 2. MSSA — Methicillin-Sensitive Staphylococcus aureus (Gram-positive)
**Brief characteristics**: Kokus berkelompok (seperti anggur), coagulase-positive
**Habitat**: Kulit, mukosa hidung (nares anterior)
**Major diseases**: Infeksi kulit & jaringan lunak (abses, selulitis), pneumonia, osteomielitis, endokarditis,
sepsis
**Effective antibiotics**: Cefazolin, Oxacillin/Nafcillin, Amoxicillin-clavulanate, Clindamycin, Levofloxacin

### 3. MRSA — Methicillin-Resistant Staphylococcus aureus (Gram-positive)
**Brief characteristics**: Morfologi mirip MSSA, resisten terhadap antibiotik beta-laktam
**Habitat**: Kulit, rumah sakit (HA-MRSA), lingkungan komunitas (CA-MRSA)
**Major diseases**: Abses kulit, pneumonia berat, endokarditis, sepsis
**Effective antibiotics**: Vancomycin (standar emas), Linezolid, Daptomycin (bukan untuk pneumonia), Clindamycin
(jika rentan), Trimethoprim-sulfamethoxazole (infeksi ringan)

### 4. Moraxella catarrhalis (Gram-negative)
**Brief characteristics**: Diplokokus Gram-negatif, memproduksi beta-laktamase
**Habitat**: Saluran napas atas
**Major diseases**: Otitis media, sinusitis, eksaserbasi akut PPOK, bronkitis
**Effective antibiotics**: Amoxicillin-clavulanate, Cefixime/Ceftriaxone, Azitromisin, Levofloxacin, Ciprofloxacin

### 5. Haemophilus influenzae (Gram-negative)
**Brief characteristics**: Kokobasilus, butuh faktor X & V, tipe b paling virulen
**Habitat**: Nasofaring
**Major diseases**: Pneumonia, epiglotitis, otitis media, sinusitis, meningitis (tipe b)
**Effective antibiotics**: Amoxicillin-clavulanate, Ceftriaxone/Cefotaxime, Azitromisin, Levofloxacin

### 6. Escherichia coli (Gram-negative)
**Brief characteristics**: Basilus Gram-negatif, anaerob fakultatif
**Habitat**: Flora normal usus
**Major diseases**: ISK, sepsis, diare, meningitis neonatal, infeksi intra-abdomen
**Effective antibiotics**: Ceftriaxone, Cefixime, Ciprofloxacin, Levofloxacin, Trimethoprim-sulfamethoxazole,
Karbapenem (strain ESBL)

### 7. Pseudomonas aeruginosa (Gram-negative)
**Brief characteristics**: Basilus Gram-negatif, patogen oportunistik, pembentukan biofilm kuat, bau khas
**Habitat**: Air, lingkungan rumah sakit, alat medis
**Major diseases**: Pneumonia nosokomial, infeksi luka bakar, ISK terkait kateter, sepsis, otitis eksterna maligna
**Effective antibiotics**: Ciprofloxacin, Levofloxacin, Ceftazidime, Cefepime, Piperacillin-tazobactam, Meropenem

### 8. Klebsiella pneumoniae (ESBL-producing) (Gram-negative)
**Brief characteristics**: Basilus berkapsul, non-motil, memproduksi enzim ESBL (*Extended-Spectrum
Beta-Lactamase*) yang menonaktifkan hampir semua penisilin & sefalosporin
**Habitat**: Flora normal usus, lingkungan rumah sakit (ventilator, kateter)
**Major diseases**: Pneumonia nosokomial, ISK, bakteremia, abses hati
**Effective antibiotics**: Meropenem (karbapenem, lini pertama strain ESBL), Colistin (jika resisten karbapenem),
Amikacin (kasus tertentu)

### 9. Neisseria gonorrhoeae (Gram-negative)
**Brief characteristics**: Diplokokus Gram-negatif (bentuk "biji kopi"), intraseluler dalam neutrofil, tanpa
kapsul
**Habitat**: Permukaan mukosa saluran urogenital, rektum, faring
**Major diseases**: Gonore (uretritis, servisitis), gonore faringeal/rektal, infeksi gonokokal diseminata, radang
panggul
**Effective antibiotics**: Ceftriaxone (dosis tunggal IM, lini pertama CDC), Cefixime (alternatif oral)

### 10. Enterococcus faecium — VRE (Gram-positive)
**Brief characteristics**: Kokus berpasangan/rantai pendek, sangat tahan lingkungan ekstrem, strain
vancomycin-resistant membawa gen vanA/vanB
**Habitat**: Flora normal usus, bertahan di permukaan & alat rumah sakit
**Major diseases**: Bakteremia, ISK terkait kateter, endokarditis, infeksi intra-abdomen (biasanya pasien rawat
inap/immunocompromised)
**Effective antibiotics**: Linezolid (lini pertama, FDA-approved untuk VRE), Daptomycin (bukan untuk pneumonia)

### 11. Helicobacter pylori (Gram-negative)
**Brief characteristics**: Basilus spiral (heliks), motil dengan flagela, memproduksi urease untuk bertahan di
keasaman lambung
**Habitat**: Mukosa lambung
**Major diseases**: Gastritis kronis, tukak lambung/duodenum, limfoma MALT lambung, faktor risiko kanker lambung
**Effective antibiotics**: Clarithromycin + Amoxicillin (triple therapy standar dengan PPI), Levofloxacin (regimen
salvage/rescue)

### 12. Streptococcus pyogenes — Group A Strep/GABHS (Gram-positive)
**Brief characteristics**: Kokus berantai, beta-hemolitik, memproduksi banyak faktor virulensi (protein M,
streptolisin)
**Habitat**: Nasofaring, kulit
**Major diseases**: Faringitis streptokokus ("strep throat"), demam skarlet, impetigo, selulitis, fasciitis
nekrotikan, komplikasi pasca-infeksi (demam rematik, glomerulonefritis)
**Effective antibiotics**: Penicillin V (lini pertama, GABHS tetap universal rentan), Amoxicillin (alternatif sama
efektif)

### 13. Acinetobacter baumannii — CRAB (Gram-negative)
**Brief characteristics**: Kokobasilus, sangat tahan lingkungan (bertahan berminggu-minggu di permukaan kering),
strain carbapenem-resistant adalah patogen prioritas kritis #1 WHO (2024)
**Habitat**: Lingkungan rumah sakit (ventilator, alat medis, permukaan), air
**Major diseases**: Pneumonia terkait ventilator, bakteremia, infeksi luka (khususnya luka perang/trauma),
meningitis terkait alat medis
**Effective antibiotics**: Colistin (obat lini terakhir), Ampicillin-sulbactam (strain rentan tertentu)

### 14. Chlamydia trachomatis (intraseluler obligat)
**Brief characteristics**: Bakteri intraseluler obligat (gak bisa bereplikasi di luar sel inang), tanpa dinding
peptidoglikan klasik, punya 2 bentuk (elementary body/reticulate body)
**Habitat**: Sel epitel kolumnar saluran urogenital, konjungtiva, rektum
**Major diseases**: Klamidia (uretritis, servisitis — sering tanpa gejala), radang panggul, infertilitas, trakoma
(okular, daerah endemik), konjungtivitis/pneumonia neonatal
**Effective antibiotics**: Doxycycline (lini pertama CDC sejak 2021), Azitromisin (alternatif, mis. kehamilan)

---

## 5. Kesimpulan (14 kasus)

| Penyakit | Jenis Penyakit | Bakteri Penyebab Tersering | Antibiotik Pilihan |
|---|---|---|---|
| Upper Respiration Infection | Otitis media | *S. pneumoniae* (G+), *H. influenzae* (G−), *M. catarrhalis* (G−) | Amoxicillin, Azitromisin, Cefixime |
| Upper Respiration Infection | Acute Faringitis | *S. pyogenes*/GABHS (G+) | Penicillin V, Amoxicillin |
| Lower Respiration Infection | Chronic Bronchitis | *H. influenzae* (G−), *M. catarrhalis* (G−) | Azitromisin, Levofloksasin, Ciprofloxacin |
| Lower Respiration Infection | Community-acquired pneumonia (CAP) | *S. pneumoniae* (G+), *H. influenzae* (G−) | Amoxicillin, Azitromisin, Levofloksasin |
| Lower Respiration Infection | Hospital-acquired pneumonia (HAP) | MSSA/MRSA (G+), *P. aeruginosa* (G−) | Vancomisin (MRSA), Levofloksasin, Ciprofloxacin |
| UTI | Urinary Tract Infection (UTI) | *E. coli* (G−), *P. aeruginosa* (G−) | Ciprofloxacin, Levofloksasin, Cotrimoxazole, Cefixime |
| Infeksi Nosokomial (ESBL) | Infeksi ESBL (mis. HAP/UTI berat) | *K. pneumoniae* ESBL (G−) | Meropenem |
| Infeksi Menular Seksual | Gonore | *N. gonorrhoeae* (G−) | Ceftriaxone (Cefixime alternatif) |
| Infeksi Nosokomial | Infeksi VRE | *E. faecium* VRE (G+) | Linezolid |
| Gastrointestinal | Tukak Lambung / Gastritis Kronis | *H. pylori* (G−) | Clarithromycin + Amoxicillin |
| Upper Respiration Infection | Strep Throat | *S. pyogenes*/GABHS (G+) | Penicillin V (Amoxicillin alternatif) |
| Infeksi ICU / MDR | Infeksi CRAB (boss stage) | *A. baumannii* CRAB (G−) | Colistin |
| Infeksi Menular Seksual | Klamidia | *C. trachomatis* | Doxycycline (Azitromisin alternatif) |

---

## 6. Referensi

- `MATERI GAME-1.pdf`, `MATERI GAME-2.pdf` (materi skripsi resmi — matriks 7×7 asli, profil antibiotik/bakteri
  format lengkap, deskripsi penyakit, tabel kesimpulan)
- `docs/antibiotic-bacteria-matrix-v2.md` (riset klinis 7 pasangan baru — reasoning detail per pasangan, sumber
  CDC/WHO/ACG/jurnal, ada di bagian 5 dokumen tsb)
- `docs/stage-implementation-status.md` (status implementasi gameplay Stage 7-13, placeholder karakter saat ini)

**Belum diimplementasikan ke kode**: matriks 14×14 di atas masih dokumen desain — `Projectile*.cs`/`Capsule*.cs`
saat ini pakai matriks per-warna yang di-hardcode manual dan cuma cover 6 pasangan lama (bukan hasil konversi
langsung dari sini). Nyocokin kode ke matriks ini adalah kerjaan terpisah, nunggu karakter/bakteri final Stage
7-13 selesai gameplay-nya (lihat `stage-implementation-status.md`).
