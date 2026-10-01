# MicroNexus — Project Context

Living snapshot dari state project. Beda dari `session-log-*.md` (log harian historis) — ini SATU dokumen yang
selalu diupdate ke kondisi terkini, buat orientasi cepat di sesi baru tanpa perlu baca semua log lama.

Kalau butuh detail historis/alasan kenapa sesuatu dikerjain kayak gitu, baca session log terkait
(`docs/session-log-2026-08-03.md`, `docs/session-log-2026-08-04.md`, dst) atau doc spesifik yang dirujuk di bawah.

---

## 1. Apa itu MicroNexus

Tower-defense game (Unity 6000.0.62f1, URP 2D) yang ngajarin antimicrobial resistance (AMR) — pemain menempatkan
"antibiotik" (defender) buat ngelawan gelombang "bakteri" (enemy) yang berjalan di jalur menuju target. Skripsi
untuk UII. Working directory: `E:\SKRIPSI BACKUPP\MicroNexus` (bukan git repo).

## 2. Roster antibiotik & bakteri

**Target: 14 antibiotik × 14 bakteri**, dipecah jadi Stage 1-13 (1 stage = 1 kasus penyakit = 1 pasangan
bakteri-penyebab + antibiotik-pilihan). Referensi lengkap ada di
**[`antibiotic-bacteria-matrix-full-14x14.md`](antibiotic-bacteria-matrix-full-14x14.md)** — matriks efektivitas
14×14 penuh, deskripsi 13 penyakit, profil lengkap tiap antibiotik & bakteri (format sama kayak `MATERI GAME-1/2.pdf`),
tabel kesimpulan.

| Status | Antibiotik/Bakteri | Stage | Catatan |
|---|---|---|---|
| **Live gameplay** (7 lama) | Amoxicillin, Cefixime, Vancomisin, Azitromisin, Ciprofloxacin, Levofloksasin, Cotrimoxazole × S. pneumoniae, MSSA, MRSA, M. catarrhalis, H. influenzae, E. coli, P. aeruginosa | 1-6 | Implementasi gameplay penuh (`Capsule*.cs`/`BasicShooter*.cs`/`SoldierBasic/*.cs`, character art selesai) |
| **Scaffolded, placeholder** (7 baru) | Meropenem/Klebsiella ESBL, Ceftriaxone/N. gonorrhoeae, Linezolid/VRE, Clarithromycin/H. pylori, Penicillin V/S. pyogenes, Colistin/CRAB, Doxycycline/C. trachomatis | 7-13 | Scene ada (`Level 7-12 Stage 1.unity`, Stage 13 belum), tapi PAKAI character/bacteria LAMA sebagai placeholder — lihat `stage-implementation-status.md` buat kombinasi exact tiap stage. Character art baru: 5/7 selesai (Ceftriaxone, Linezolid, Clarithromycin, Doxycycline, Meropenem); Penicillin V & Colistin belum. Bakteri baru: art belum ada sama sekali (baru rencana warna/shape). |

**Matriks numerik (0-20 scale) di dokumen full-14x14 BELUM disinkron ke kode** — `Projectile*.cs`/`Capsule*.cs`
masih pakai matriks per-warna hardcode manual yang cuma cover 6 pasangan lama. Nyocokin ke matriks referensi adalah
kerjaan terpisah, nunggu character/bacteria final Stage 7-13 kelar.

## 3. Arsitektur gameplay penting

- **3 tier defender independen** (duplikasi per-warna, bukan shared base class): `Capsule<Color>.cs` (melee,
  insta-kill target-color bacteria), `BasicShooter<Color>.cs` (ranged auto-fire), `SoldierBasic/BasicShooter
  <Color>Soldier.cs` (ranged + animasi shoot). Ketujuhnya sekarang share `DefenderHealth.cs` (5 HP, gak lagi
  one-shot mati kena bakteri — lihat §4) tapi logic serang/damage-ke-bakteri tetap independen per file.
- **7 `BacteriaController<Color>.cs`** — pola sama tiap warna: `FixedUpdate()` gerak kiri kecuali `isHit`,
  `Hit()` buat mati, `ProjectileHit(int damage)` buat kena serangan. Sekarang juga bisa "engage" nyerang defender
  lewat `BacteriaMeleeAttacker.cs` (lihat §4).
- **`BacteriaSpawner.cs`** (+ `BacteriaSpawnerEndless.cs` versi Endless) — 3 mode spawn (Single/Multi/Grouped Per
  Line). `GetFreeLine()`/`GetFreeSlot()` udah di-fix supaya milih lane FREE secara random (dulu selalu milih lane
  pertama yang kosong → bias spawn atas-ke-bawah).
- **Level Map** (`Assets/Scenes/Stage Selection/Level Map.unity`, `LevelMapController.cs` + `LevelNodeView.cs`) —
  pengganti Stage Selection lama, nampilin 12 node stage sepanjang jalur horizontal-scroll. Tiap node sekarang
  pakai visual "porthole" (`Assets/Prefab/LevelMap/LevelNode.prefab`): animasi bakteria (`UISpriteFrameAnimator.cs`
  + `BacteriaFrameSet.cs` ScriptableObject, di `Assets/ScriptableObjects/BacteriaFrameSets/`) keliatan lewat kaca
  transparan porthole (`Assets/Art/Button/portHole_1.png`). Warna bakteri per-level di-assign manual di
  `LevelMapController.stage1LevelBacteriaColor[]`, cross-checked ke `BacteriaSpawner.bacteriaPrefabs` tiap scene.
  Status Locked/Unlocked/Completed cuma dibedain checkmark badge, BUKAN warna rim lagi (user request).

## 4. Fitur baru: Antibiotic HP (bukan one-shot lagi)

Defender sekarang punya 5 HP (`DefenderHealth.cs`), bakteri berhenti gerak & nyerang tiap 1.5 detik
(`BacteriaMeleeAttacker.cs`, pakai reflection buat freeze `speed` field bakteri — beda class per warna, gak ada
base class) sampai defender mati atau bakteri kabur dari trigger range. Feedback "-1 HP" pakai popup yang sama
gayanya kayak popup efektivitas serangan lama (`DefenderHitPopup.cs`, reuse `FloatingPopupText.cs`/
`BacteriaLabelFactory.cs`). Musuh NON-bakteri (Mushroom/Protozoa/Helminth/Virus) TETAP one-shot seperti semula —
fitur ini scoped ke bakteri doang sesuai request user.

## 5. Instruksi standing yang HARUS diikuti

- **Main Menu (`New MainMenu.unity`) — SELESAI, JANGAN DISENTUH** tanpa instruksi eksplisit baru dari user (dari
  session 2026-08-04). Kalau audit/investigasi nemu masalah di sana, laporin aja, jangan langsung fix.
- **Stage 7-12 pakai character LAMA sebagai placeholder** — jangan bingung ini "udah selesai", tetap placeholder
  sampai character/bacteria final punya implementasi gameplay (script Capsule/BasicShooter/Projectile + damage
  matrix + sprite final).
- **`Stage Selection.unity` (scene lama, deprecated)** — udah gak dipakai di alur live manapun (semua tombol yang
  dulu nunjuk ke situ udah di-repoint ke `New MainMenu`/`Stage 2 Selection`). Masih ada di Build Settings & disk,
  tapi orphaned. Jangan dihidupkan lagi tanpa alasan jelas.

## 6. Known issues / audit trail

Audit project-wide (Main Menu flow, onClick bindings, dead code) ada di **[`known-issues.md`](known-issues.md)**
— per-tick log, terakhir berhenti di Tick 4 (findings `Stage Selection.unity` 12 tombol no-listener BELUM
ditulis ke file, perlu dilanjutin kalau audit di-resume).

## 7. Dokumen lain yang relevan

| Dokumen | Isi |
|---|---|
| [`antibiotic-bacteria-matrix-full-14x14.md`](antibiotic-bacteria-matrix-full-14x14.md) | Referensi lengkap 14×14 — matriks, profil, kesimpulan (dokumen utama, gantiin v2 buat referensi harian) |
| [`antibiotic-bacteria-matrix-v2.md`](antibiotic-bacteria-matrix-v2.md) | Riset klinis mentah per pasangan baru (kenapa nilai segini, sumber CDC/WHO/jurnal) — rujuk kalau butuh alasan detail di balik satu angka |
| [`stage-implementation-status.md`](stage-implementation-status.md) | Kombinasi placeholder exact tiap Stage 7-13 + status implementasi |
| [`character-design-v2.md`](character-design-v2.md), [`character-design-v3-style-pivot.md`](character-design-v3-style-pivot.md) | Workflow & progress desain karakter 7 antibiotik baru |
| [`known-issues.md`](known-issues.md) | Audit trail bug/dead-code project-wide |
| `session-log-2026-08-03.md`, `session-log-2026-08-04.md` | Log historis harian (detail teknis lebih dalam per perubahan) |

## 8. Graphify

Knowledge graph project ada di `graphify-out/` (scope: `docs/` + `Assets/Scripts`, `Assets/Art` sengaja dikecualiin
— terlalu banyak file gambar). Update pakai `/graphify` (full pipeline) tiap ada perubahan besar ke kode/dokumen,
biar query graph tetap akurat.
