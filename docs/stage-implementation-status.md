# Status Implementasi Stage 7-13 (roster baru)

Semua stage di bawah ini **placeholder** — karakter & bakteri yang kepake sekarang adalah PINJAMAN dari roster
lama (7 antibiotik/bakteri yang udah ada implementasi gameplay-nya), bukan karakter final. Karakter/bakteri
final BELUM bisa dipakai karena belum punya implementasi gameplay (`Capsule`/`BasicShooter`/`Projectile` script
+ damage matrix + sprite final) — yang ada baru concept art dari sesi desain karakter (`character-design-v3-style-pivot.md`).

**Kapan placeholder ini diganti**: begitu karakter/bakteri final versi masing-masing stage punya implementasi
gameplay lengkap, card & bacteria spawner di scene terkait tinggal di-swap ke yang final.

---

## Tabel status

| Stage | Scene | Final antibiotik (belum diimplementasi) | Final bakteri (belum diimplementasi) | Placeholder karakter (4 card) | Placeholder bakteri | Status |
|---|---|---|---|---|---|---|
| 7 | `Level 7 Stage 1.unity` | Meropenem | Klebsiella pneumoniae (ESBL) | **Meropenem (`Merope.prefab`, FINAL)**, Cefixime, Ciprofloxacin, Cotrimoxazole | Esherchia Coli, Pseudomonas aeruginosa (placeholder, bakteri final belum ada art) | ✅ Scene jadi, di Level Map, di Build Settings. Card Levofloksasin diganti Merope (prefab `Assets/Prefab/Character 2/Merope.prefab`, projectile `MeropeProjectile.prefab` = ampule, damage per matriks 14×14) |
| 8 | `Level 8 Stage 1.unity` | Ceftriaxone | Neisseria gonorrhoeae | Cefixime, Azitromisin, Ciprofloxacin, Cotrimoxazole | Moracella xataralis, Haemophilus influenzae | belum dikerjain |
| 9 | `Level 9 Stage 1.unity` | Linezolid | Enterococcus faecium (VRE) | Vancomisin, Cotrimoxazole, Azitromisin, Levofloksasin | Streptococcus pneumonia, Staphylloccocus aureus | belum dikerjain |
| 10 | `Level 10 Stage 1.unity` | Clarithromycin | Helicobacter pylori | Amoxicillin, Azitromisin, Cefixime, Cotrimoxazole | Haemophilus influenzae, Moracella xataralis | belum dikerjain |
| 11 | `Level 11 Stage 1.unity` | Penicillin V | Streptococcus pyogenes (GABHS) | Amoxicillin, Azitromisin, Vancomisin, Cefixime | Streptococcus pneumonia, Staphylloccocus aureus | belum dikerjain |
| 12 | `Level 12 Stage 1.unity` | Colistin | Acinetobacter baumannii (CRAB) | Levofloksasin, Ciprofloxacin, Cotrimoxazole, Amoxicillin | Pseudomonas aeruginosa, MRSA | belum dikerjain (boss stage — placeholder-nya sengaja "kurang kuat" ngelawan CRAB, biar konsisten sama narasi "obat lama gak cukup") |
| 13 | `Level 13 Stage 1.unity` | Doxycycline | Chlamydia trachomatis | *(belum ditentuin, nyusul)* | *(belum ditentuin, nyusul)* | belum dikerjain |

Alasan pemilihan placeholder per stage (kenapa kombinasi ini yang "masuk"):
- **8 (Gonore)**: Moracella catarrhalis dipilih karena morfologinya emang mirip N. gonorrhoeae (sama-sama
  diplokokus Gram-negatif) — placeholder paling nyambung yang ada di roster lama.
- **9 (VRE)**: Vancomisin sengaja dimasukin walau ironis (obat yang seharusnya kalah lawan VRE) — bisa jadi bahan
  ngobrol soal "kenapa nanti pas Linezolid diimplementasi, Vancomisin harus 0 damage di sini".
- **10 (H. pylori)**: Amoxicillin dimasukin karena di dunia nyata itu bagian dari terapi kombinasi asli H. pylori.
- **11 (Strep throat)**: Streptococcus pneumonia jadi placeholder bakteri paling pas (satu genus sama
  S. pyogenes yang jadi bakteri final), Amoxicillin jadi placeholder antibiotik paling pas (di dunia nyata
  emang sama efektifnya sama Penicillin V).
- **12 (CRAB, boss)**: kombinasi placeholder di sini emang gak didesain buat "menang mudah" — refleksi dari tema
  boss stage (CRAB itu WHO critical priority #1, hampir semua obat lama gagal ngelawannya di dunia nyata juga).

---

## Background

Semua stage 7-13 pakai 1 background yang sama: `Assets/Art/Background/newStageGrid.png` (tema biru-ungu, beda
dari background oranye yang dipake Level 1-6). UI/HUD tetep pakai prefab & layout lama (belum di-redesign).
